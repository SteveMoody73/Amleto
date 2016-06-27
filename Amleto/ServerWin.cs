using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;
using NLog;
using RemoteExecution;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.IO;
using System.Runtime.Remoting.Activation;
using System.Collections;
using RemoteExecution.Jobs;
using Manina.Windows.Forms;

namespace Amleto
{
    public partial class ServerWin : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private MasterServer _masterServer;
        private bool _isMaster;
        private TcpChannel _channel;

        private List<ClientConnection> _clients;
        private List<RenderProject> _projects;
        private List<RenderProject> _finishedProjects;
        private readonly object _clientsLock = new object();
        private readonly object _projectsLock = new object();
        private readonly object _previewLock = new object();

        private EventBridge _eventBridge;

        private readonly Stopwatch _clientListRefresh = new Stopwatch();
        private readonly Stopwatch _projectListRefresh = new Stopwatch();
        private readonly Stopwatch _finishedListRefresh = new Stopwatch();
        private readonly Stopwatch _imageRefresh = new Stopwatch();

        private int _lastActiveTime = -1;

        private bool _needToUpdateClient = true;

        private readonly Queue<FinishedFrame> _framesToAdd = new Queue<FinishedFrame>();

        private ServerSettings _settings;

        private int _thumbSize = 150;


        public ServerWin()
        {
            InitializeComponent();

            // Try to recover old setting...
            _settings = ServerSettings.LoadSettings();

            ClientStatusGrid.DataError += dataGrid_DataError;
            ActiveProjectGrid.DataError += dataGrid_DataError;
            FinishedProjectGrid.DataError += dataGrid_DataError;
            RenderedImages.DataError += dataGrid_DataError;

            RenderedImages.Rows.Clear();

            ImagePreviews.ThumbnailSize = new Size(_thumbSize, _thumbSize);

            Size = _settings.WinSize;
        }

        private void dataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void ShowImage(FinishedFrame frame)
        {
            _framesToAdd.Enqueue(frame);
            if (!_imageRefresh.IsRunning)
            {
                _imageRefresh.Reset();
                _imageRefresh.Start();
            }
        }

        private void DoShowImages()
        {
            lock (_previewLock)
            {
                while (_framesToAdd.Count > 0)
                {
                    FinishedFrame frame = _framesToAdd.Dequeue();

                    DataGridViewRow currentRow = null;
                    bool exists = false;

                    foreach (DataGridViewRow row in RenderedImages.Rows)
                    {
                        if (row.Cells[0].Value != null)
                        {
                            if (row.Cells[0].Value.ToString() == frame.Nodename)
                            {
                                exists = true;
                            }
                            if (row.Selected == true)
                            {
                                currentRow = row;
                            }
                        }
                    }

                    if (exists == false)
                    {
                        try
                        {
                            RenderedImages.SelectionChanged -= RenderedImagesSelectionChanged;

                            currentRow = new DataGridViewRow();
                            DataGridViewTextBoxCell project = new DataGridViewTextBoxCell();
                            List<String> images = new List<String>();

                            project.Value = frame.Nodename;
                            currentRow.Cells.Add(project);
                            currentRow.Tag = images;
                            RenderedImages.Rows.Add(currentRow);
                            currentRow.Selected = true;
                            ImagePreviews.Items.Clear();

                            RenderedImages.SelectionChanged += RenderedImagesSelectionChanged;
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex, "Error Adding rendered image");
                        }
                    }

                    if (currentRow != null)
                    {
                        // Add image to view
                        try
                        {
                            List<String> fileList = null;
                            if (currentRow.Tag != null && currentRow.Tag.GetType() == typeof(List<String>))
                            {
                                fileList = (List<String>)currentRow.Tag;
                                fileList.Add(frame.Filename);
                            }

                            if (currentRow.Selected || exists == false)
                            {
                                bool force = File.Exists(frame.Filename);
                                Image thumb = Thumbnail.CreateImageThumbnail(frame.Nodename, frame.Filename, _thumbSize, force);

                                if (thumb != null)
                                    ImagePreviews.Items.Add(frame.Filename, thumb);
                                else
                                    ImagePreviews.Items.Add(frame.Filename);
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex, "Error adding image to preview");
                        }
                    }
                }
            }
        }

        private TreeNode AddSortSceneBranch(TreeNode branch, string toAdd)
        {
            for (int i = 0; i < branch.Nodes.Count; i++)
            {
                if (branch.Nodes[i].Text.CompareTo(toAdd) > 0)
                    return branch.Nodes.Insert(i, toAdd);
            }
            return branch.Nodes.Add(toAdd);
        }

        private TreeNode SortSceneBranch(TreeNode branch, string toAdd)
        {
            List<string> elements = new List<string>();

            for (int i = 0; i < branch.Nodes.Count; i++)
                elements.Add(branch.Nodes[i].Text);

            if (toAdd != null)
                elements.Add(toAdd);

            elements.Sort();
            branch.Nodes.Clear();
            TreeNode res = null;

            foreach (string t in elements)
            {
                TreeNode child = branch.Nodes.Add(t);
                if (t == toAdd)
                    res = child;
            }
            return res;
        }

        private void DoAddFrames(IEnumerable<FinishedFrame> frames)
        {
            TreeNode baseNode = null;
            string prevNodeName = "-----------------------";

            foreach (FinishedFrame frame in frames)
            {
                if (prevNodeName != frame.Nodename)
                {
                    prevNodeName = frame.Nodename;
                }
                TreeNode child = new TreeNode(frame.Filename);
                if (baseNode != null) baseNode.Nodes.Add(child);
            }
        }

        private void ConsumeMessage(string msg)
        {
            BeginInvoke((MethodInvoker) delegate { DoConsumeMessage(msg); });
        }

        private void DoConsumeMessage(string msg)
        {
            try
            {
                string[] p = msg.Split('|');
                DataGridViewRow row = new DataGridViewRow();

                if (p.Length == 1)
                {
                    DataGridViewCell cell = new DataGridViewImageCell();
                    cell.Value = iconList.Images[0];
                    row.Cells.Add(cell);
                    cell = new DataGridViewTextBoxCell();
                    cell.Value = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                    row.Cells.Add(cell);
                    cell = new DataGridViewTextBoxCell();
                    cell.Value = p[0];
                    row.Cells.Add(cell);
                }
                else
                {
                    DataGridViewCell cell = new DataGridViewImageCell();
                    cell.Value = iconList.Images[Convert.ToInt32(p[0])];
                    row.Cells.Add(cell);
                    cell = new DataGridViewTextBoxCell();
                    cell.Value = p[1];
                    row.Cells.Add(cell);
                    cell = new DataGridViewTextBoxCell();
                    cell.Value = p[2];
                    row.Cells.Add(cell);
                }

                while (messageList.Rows.Count > 3000)
                    messageList.Rows.RemoveAt(0);

                if (messageList.SelectedRows.Count > 0)
                    messageList.SelectedRows[0].Selected = false;

                row.Height = 17;
                messageList.Rows.Add(row);
                row.Selected = true;
                messageList.FirstDisplayedScrollingRowIndex = messageList.Rows.Count - 1;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error invalid message: " + msg);
                MessageBox.Show("Invalid message: " + msg);                
            }
        }

        private void RestoreClientList()
        {
            List<ClientConnection> list = _masterServer.GetConnectedHosts();
            RefreshClientList(list);
        }

        private void RefreshClientList(List<ClientConnection> clients)
        {
            lock (_clientsLock)
            {
                _clients = clients;
            }
            if (!_clientListRefresh.IsRunning)
            {
                _clientListRefresh.Reset();
                _clientListRefresh.Start();
            }
        }

        private void RepaintClientList()
        {
            List<string> oldSelected = new List<string>();
            foreach (DataGridViewRow row in ClientStatusGrid.SelectedRows)
                oldSelected.Add(row.Cells[0].Value.ToString());

            int pos = ClientStatusGrid.FirstDisplayedScrollingRowIndex;

            lock (_clientsLock)
            {
                List<ClientConnection> hosts = _clients;
                try
                {
                    ClientStatusGrid.Rows.Clear();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Error clearing client status list");
                }

                List<string> configsName = _masterServer.ConfigNames;

                foreach (ClientConnection client in hosts)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    DataGridViewCell cell = new DataGridViewTextBoxCell();

                    try
                    {
                        // Client ID
                        cell.Value = client.Id;
                        row.Cells.Add(cell);

                        // Hostname
                        cell = new DataGridViewTextBoxCell {Value = client.HostName + " (" + client.IPAddress + ")"};
                        row.Cells.Add(cell);

                        // Instance
                        cell = new DataGridViewTextBoxCell {Value = client.Instance};
                        row.Cells.Add(cell);
    
                        // Configs
                        DataGridViewComboBoxCell configList = new DataGridViewComboBoxCell();
                        foreach (string s in configsName)
                        {
                            try
                            {
                                configList.Items.Add(s);
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex, "Error adding config item");
                            }
                        }
                        configList.Value = configsName[client.Config];
                        row.Cells.Add(configList);
                    
                        // Status
                        cell = new DataGridViewImageCell();
                        if (client.CurrentJob.StartsWith("Downloading"))
                            cell.Value = iconList.Images[2];
                        else if (client.CurrentJob.StartsWith("Project"))
                            cell.Value = iconList.Images[5];
                        else if (client.CurrentJob.StartsWith("Render"))
                            cell.Value = iconList.Images[4];
                        else if (client.CurrentJob == "" && client.CanBeUsed == false)
                            cell.Value = iconList.Images[6];
                        else if (client.CurrentJob == "")
                            cell.Value = iconList.Images[3];
                        else
                            cell.Value = iconList.Images[2];

                        row.Cells.Add(cell);

                        // Last render time
                        cell = new DataGridViewTextBoxCell();
                        if (client.LastFrameTimeSet)
                        {
                            cell.Value = String.Format("{0:00}:{1:00}:{2:00}",
                                                       (client.LastFrameTime.Days*24) + client.LastFrameTime.Hours,
                                                       client.LastFrameTime.Minutes, client.LastFrameTime.Seconds);
                        }
                        row.Cells.Add(cell);

                        // Current Job
                        cell = new DataGridViewTextBoxCell {Value = client.CurrentJob};
                        row.Cells.Add(cell);

                        ClientStatusGrid.Rows.Add(row);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "Error adding cells");
                    }

                    try
                    {
                        foreach (string s in oldSelected)
                        {
                            if (s == row.Cells[0].Value.ToString())
                                row.Selected = true;
                            else
                                row.Selected = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "Error selecting cell");
                    }
                }
                try
                {
                    if (pos != -1)
                        ClientStatusGrid.FirstDisplayedScrollingRowIndex = pos;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Error repainting client list");
                }
            }
        }

        private void RefreshProjectList(List<RenderProject> projects)
        {
            lock (_projectsLock)
            {
                _projects = projects;
            }
            if (!_projectListRefresh.IsRunning)
            {
                _projectListRefresh.Reset();
                _projectListRefresh.Start();
            }
        }

        private void RefreshFinishedList(List<RenderProject> finishedProjects)
        {
            lock (_projectsLock)
            {
                _finishedProjects = finishedProjects;
            }
            if (!_finishedListRefresh.IsRunning)
            {
                _finishedListRefresh.Reset();
                _finishedListRefresh.Start();
            }
        }

        private void RepaintProjectList()
        {
            string oldSelected = "";
            if (ActiveProjectGrid.SelectedRows.Count > 0)
                oldSelected = ActiveProjectGrid.SelectedRows[0].Cells[0].Value.ToString();

            int pos = ActiveProjectGrid.FirstDisplayedScrollingRowIndex;

            lock (_projectsLock)
            {
                try
                {
                    ActiveProjectGrid.Rows.Clear();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Error clearing project grid");
                }

                foreach (RenderProject project in _projects)
                {
                    DataGridViewRow row = new DataGridViewRow();

                    // Project ID
                    DataGridViewCell cell = new DataGridViewTextBoxCell();
                    cell.Value = project.ProjectId;
                    row.Cells.Add(cell);
                    
                    // Project Name
                    cell = new DataGridViewTextBoxCell
                               {Value = project.SceneFile.Substring(project.SceneFile.LastIndexOf('\\') + 1)};
                    row.Cells.Add(cell);
                    
                    // Project Owner
                    cell = new DataGridViewTextBoxCell {Value = project.Owner};
                    row.Cells.Add(cell);
                    
                    // Start Frame
                    cell = new DataGridViewTextBoxCell {Value = project.StartFrame};
                    row.Cells.Add(cell);
                    
                    // End Frame
                    cell = new DataGridViewTextBoxCell {Value = project.EndFrame};
                    row.Cells.Add(cell);
                    
                    // Number of frames to render
                    cell = new DataGridViewTextBoxCell {Value = project.StartJobs};
                    row.Cells.Add(cell);

                    // Status 
                    cell = new DataGridViewTextBoxCell();
                    if (project.StartJobs == 0)
                        cell.Value = "Waiting";
                    else
                    {
                        if (project.StartJobs > 0)
                        {
                            if (project.Paused)
                                cell.Value = "Paused";
                            else
                                cell.Value = "Active";
                        }
                    }
                    row.Cells.Add(cell);

                    // Percent completed
                    cell = new DataGridViewProgressCell();
                    if (project.StartJobs == 0)
                        cell.Value = 0;
                    else
                    {
                        if (project.StartJobs > 0)
                        {
                            int progress = (project.StartJobs - project.RemainingJobs()) * 100 / project.StartJobs;
                            cell.Value = progress;
                        }
                    }
                    row.Cells.Add(cell);

                    // Elapsed Time
                    cell = new DataGridViewTextBoxCell();
                    if (project.StartTimeSet)
                    {
                        TimeSpan elapsed = DateTime.Now - project.StartTime;
                        cell.Value = String.Format("{0:00}:{1:00}:{2:00}", (elapsed.Days * 24) + elapsed.Hours, elapsed.Minutes, elapsed.Seconds);
                    }
                    else
                        cell.Value = "";
                    row.Cells.Add(cell);
                    
                    // Estimated time
                    cell = new DataGridViewTextBoxCell();
                    if (project.StartTimeSet && project.UpdateTimeSet && project.RenderedFrames.Count > 0)
                    {
                        TimeSpan elapsed = project.UpdateTime - project.StartTime;
                        long frameTime = elapsed.Ticks/ project.RenderedFrames.Count;
                        long totalframes = project.StartJobs;
                        if (project.SaveAlpha)
                            totalframes *= 2;
                        long remainFrames = totalframes - project.RenderedFrames.Count;
                        long estimated = remainFrames*frameTime;
                        TimeSpan remain = new TimeSpan(estimated);
                        cell.Value = String.Format("{0:00}:{1:00}:{2:00}",
                                                   (remain.Days*24) + remain.Hours, remain.Minutes, remain.Seconds);                        
                    }
                    else
                        cell.Value = "";
                    row.Cells.Add(cell);

                    try
                    {
                        ActiveProjectGrid.Rows.Add(row);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "Error adding project row");
                    }

                    if ((string) row.Cells[0].Value.ToString() == oldSelected)
                        row.Selected = true;
                }

                try
                {
                    if (pos != -1)
                        ActiveProjectGrid.FirstDisplayedScrollingRowIndex = pos;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Error repainting project list");
                }
            }
        }

        private void RepaintFinishedList()
        {
            string oldSelected = "";
            if (FinishedProjectGrid.SelectedRows.Count > 0)
                oldSelected = (string)FinishedProjectGrid.SelectedRows[0].Cells[0].Value.ToString();

            int pos = FinishedProjectGrid.FirstDisplayedScrollingRowIndex;

            lock (_projectsLock)
            {
                try
                {
                    FinishedProjectGrid.Rows.Clear();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Error clearing finished list");
                }

                foreach (RenderProject project in _finishedProjects)
                {
                    DataGridViewRow row = new DataGridViewRow();

                    // Project ID
                    DataGridViewCell cell = new DataGridViewTextBoxCell();
                    cell.Value = project.ProjectId;
                    row.Cells.Add(cell);

                    // Project Name
                    cell = new DataGridViewTextBoxCell
                               {Value = project.SceneFile.Substring(project.SceneFile.LastIndexOf('\\') + 1)};
                    row.Cells.Add(cell);

                    // Frame count
                    cell = new DataGridViewTextBoxCell {Value = project.RenderedFrameCount.ToString()};
                    row.Cells.Add(cell);

                    // Status
                    cell = new DataGridViewTextBoxCell {Value = project.FinalStatus};
                    row.Cells.Add(cell);

                    // Start render time
                    cell = new DataGridViewTextBoxCell();
                    if (project.StartTimeSet)
                        cell.Value = project.StartTime.ToString();
                    else
                        cell.Value = "";
                    row.Cells.Add(cell);

                    // Final Render Time
                    cell = new DataGridViewTextBoxCell
                               {
                                   Value = String.Format("{0:00}:{1:00}:{2:00}",
                                                         (project.RenderTime.Days*24) + project.RenderTime.Hours,
                                                         project.RenderTime.Minutes, project.RenderTime.Seconds)
                               };
                    row.Cells.Add(cell);

                    try
                    {
                        FinishedProjectGrid.Rows.Add(row);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "Error error adding finished row");
                    }

                    if ((string)row.Cells[0].Value.ToString() == oldSelected)
                        row.Selected = true;
                }

                try
                {
                    if (pos != -1)
                        FinishedProjectGrid.FirstDisplayedScrollingRowIndex = pos;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Error repainting finished list");
                }
            }
        }

        private void MenuExitClick(object sender, EventArgs e)
        {
            Close();
        }

        private void ServerWinFormClosing(object sender, FormClosingEventArgs e)
        {
            _settings.WinSize = Size;
            ServerSettings.SaveSettings(_settings);

            if (_isMaster &&
                MessageBox.Show("Are you sure you want to exit?", "Closing Amleto", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
            else
            {
                try
                {
                    // Save currently rendering jobs
                    string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Amleto");
                    savePath = Path.Combine(savePath, "RenderJobs");
                    if (Directory.Exists(savePath))
                    {
                        // Delete any existing saved files
                        Directory.Delete(savePath, true);
                    }
                    Directory.CreateDirectory(savePath);

                    foreach (RenderProject p in _masterServer.GetProjects())
                    {
                        string filename = Path.Combine(savePath, p.ProjectId + ".xml");
                        p.Save(filename);
                    }

                    if (_isMaster)
                        _masterServer.Shutdown();
                    _masterServer.Disconnect();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Error saving render jobs");
                }
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddProject dlg = new AddProject(_masterServer);
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void ServerWin_DragEnter(object sender, DragEventArgs e)
        {
            if (!_isMaster)
                e.Effect = DragDropEffects.None;
            else if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
                e.Effect = DragDropEffects.All;
        }

        private void ServerWin_DragDrop(object sender, DragEventArgs e)
        {
            if (!_isMaster)
                return;
            string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop);
            AddProject dlg = new AddProject(_masterServer, files[0]);
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void MenuOptionsClick(object sender, EventArgs e)
        {
            bool needToReset = false;

            SetupWin dlg = new SetupWin(_masterServer, true);
            dlg.Port = _masterServer.Port;
            dlg.AutoPort = _masterServer.AutoOfferPort;
            dlg.Configs = _masterServer.GetConfigs();
            dlg.LogFile = _masterServer.LogFile;
            dlg.LogEnabled = _masterServer.LogEnabled;
            dlg.EmailFrom = _masterServer.EmailFrom;
            dlg.SmtpServer = _masterServer.SmtpServer;
            dlg.SmtpUsername = _masterServer.SmtpUsername;
            dlg.SmtpPassword = _masterServer.SmtpPassword;
            dlg.OfferWeb = _masterServer.OfferWeb;
            dlg.OfferWebPort = _masterServer.OfferWebPort;
            dlg.RenderBlocks = _masterServer.RenderBlocks;
            dlg.MappedDrives = _masterServer.GetMappedDrives();       
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (_masterServer.Port != dlg.Port && dlg.AutoPort == false)
                    needToReset = true;
                _masterServer.Port = dlg.Port;
                _masterServer.AutoOfferPort = dlg.AutoPort;
                _masterServer.ReplaceConfigs(dlg.Configs);
                _masterServer.LogFile = dlg.LogFile;
                _masterServer.LogEnabled = dlg.LogEnabled;
                _masterServer.EmailFrom = dlg.EmailFrom;
                _masterServer.SmtpServer = dlg.SmtpServer;
                _masterServer.SmtpUsername = dlg.SmtpUsername;
                _masterServer.SmtpPassword = dlg.SmtpPassword;
                _masterServer.OfferWeb = dlg.OfferWeb;
                _masterServer.OfferWebPort = dlg.OfferWebPort;
                _masterServer.RenderBlocks = dlg.RenderBlocks;
                string res = _masterServer.SetMappedDrives(dlg.MappedDrives);
                if (res != "")
                    MessageBox.Show(res);

                _masterServer.LoadFileFormats();
                _masterServer.SaveSettings();
                RepaintClientList();
            }
            dlg.Dispose();

            if (needToReset)
            {
                _masterServer.ResetPort(_masterServer.AutoOfferPort, _masterServer.Port);
            }
        }

        private void mnuSendKill_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in ClientStatusGrid.SelectedRows)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);
                _masterServer.AddPriorityJob(id, new KillJob());
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutServer about = new AboutServer();
            about.ShowDialog();
            about.Dispose();
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveProjectGrid.SelectedRows.Count == 0)
                return;
            int id = Convert.ToInt32(ActiveProjectGrid.SelectedRows[0].Cells[0].Value);
            _masterServer.PauseProject(id);
        }

        private void resumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveProjectGrid.SelectedRows.Count == 0)
                return;
            int id = Convert.ToInt32(ActiveProjectGrid.SelectedRows[0].Cells[0].Value);
            _masterServer.ResumeProject(id);
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveProjectGrid.SelectedRows.Count == 0)
                return;
            int id = Convert.ToInt32(ActiveProjectGrid.SelectedRows[0].Cells[0].Value);
            _masterServer.StopProject(id);
        }

        private void changePriortyStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem) sender;
            foreach (DataGridViewRow row in ClientStatusGrid.SelectedRows)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);
                _masterServer.SetClientPriority(id,
                                                (ProcessPriorityClass)
                                                Enum.Parse(typeof (ProcessPriorityClass), item.Text));
            }
        }

        private void SetupJobMenu()
        {
            if (ActiveProjectGrid.SelectedRows.Count == 0)
            {
                editNodeToolStripMenuItem.Enabled = false;
                pauseToolStripMenuItem.Enabled = false;
                resumeToolStripMenuItem.Enabled = false;
                stopToolStripMenuItem.Enabled = false;
                return;
            }

            editNodeToolStripMenuItem.Enabled = true;
            stopToolStripMenuItem.Enabled = true;

            int id = Convert.ToInt32(ActiveProjectGrid.SelectedRows[0].Cells[0].Value);

            if (_masterServer.IsProjectPaused(id))
            {
                pauseToolStripMenuItem.Enabled = false;
                resumeToolStripMenuItem.Enabled = true;
            }
            else
            {
                pauseToolStripMenuItem.Enabled = true;
                resumeToolStripMenuItem.Enabled = false;
            }
        }

        private void ProjectStatusGridMouseClick(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hitInfo = ActiveProjectGrid.HitTest(e.X, e.Y);
            if (hitInfo.RowIndex == -1)
                return;
            ActiveProjectGrid.Rows[hitInfo.RowIndex].Selected = true;

            if (e.Button != MouseButtons.Right)
                return;

            if (ActiveProjectGrid.SelectedRows.Count == 0)
                return;

            SetupJobMenu();

            contextMenuProjects.Items.Clear();
            contextMenuProjects.Items.AddRange(new ToolStripItem[]
                {
                    addToolStripMenuItem,
                    editNodeToolStripMenuItem,
                    pauseToolStripMenuItem,
                    resumeToolStripMenuItem,
                    stopToolStripMenuItem
                });
            contextMenuProjects.Show(MousePosition.X, MousePosition.Y);
        }

        private void SetupNodesMenu()
        {
            if (ClientStatusGrid.SelectedRows.Count == 0)
            {
                mnuSendKill.Enabled = false;
                resendConfigToolStripMenuItem.Enabled = false;
                showMessagesToolStripMenuItem.Enabled = false;
                configToolStripMenuItem.Enabled = false;
                renderPriorityToolStripMenuItem.Enabled = false;
                setupActivationTimeToolStripMenuItem.Enabled = false;
                pauseClientToolStripMenuItem.Enabled = false;
                resumeClientToolStripMenuItem.Enabled = false;
                closeNodeToolStripMenuItem.Enabled = false;
                stopAllNodesToolStripMenuItem.Enabled = false;
                cleanClientContentDirectoryToolStripMenuItem.Enabled = false;
                cleanAllClientsContentDirectoryToolStripMenuItem.Enabled = true;
                cleanClientOutputDirectoryToolStripMenuItem.Enabled = false;
                cleanAllClientsOutputDirectoryToolStripMenuItem.Enabled = true;
                return;
            }

            resendConfigToolStripMenuItem.Enabled = true;
            mnuSendKill.Enabled = true;
            showMessagesToolStripMenuItem.Enabled = true;
            configToolStripMenuItem.Enabled = true;
            renderPriorityToolStripMenuItem.Enabled = true;
            setupActivationTimeToolStripMenuItem.Enabled = true;
            pauseClientToolStripMenuItem.Enabled = true;
            resumeClientToolStripMenuItem.Enabled = true;
            closeNodeToolStripMenuItem.Enabled = true;
            stopAllNodesToolStripMenuItem.Enabled = true;
            cleanClientContentDirectoryToolStripMenuItem.Enabled = true;
            cleanAllClientsContentDirectoryToolStripMenuItem.Enabled = true;
            cleanClientOutputDirectoryToolStripMenuItem.Enabled = true;
            cleanAllClientsOutputDirectoryToolStripMenuItem.Enabled = true;

            bool multiSelect = ClientStatusGrid.SelectedRows.Count > 1;

            if (multiSelect)
            {
                showMessagesToolStripMenuItem.Enabled = false;
                setupActivationTimeToolStripMenuItem.Enabled = false;
            }
            else
            {
                showMessagesToolStripMenuItem.Enabled = true;
                setupActivationTimeToolStripMenuItem.Enabled = true;
            }

            int id = Convert.ToInt32(ClientStatusGrid.SelectedRows[0].Cells[0].Value);
            ProcessPriorityClass priority = _masterServer.GetClientPriority(id);

            List<string> configs = _masterServer.ConfigNames;
            configToolStripMenuItem.DropDownItems.Clear();
            int configNumber = 0;
            foreach (string s in configs)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem(s);
                configToolStripMenuItem.DropDownItems.Add(menuItem);
                menuItem.Tag = configNumber;
                menuItem.Click += ConfigSet_Click;
                configNumber++;
            }

            if (multiSelect)
                foreach (ToolStripMenuItem item in renderPriorityToolStripMenuItem.DropDownItems)
                    item.Checked = false;
            else
            {
                foreach (ToolStripMenuItem item in renderPriorityToolStripMenuItem.DropDownItems)
                {
                    if (item.Text == priority.ToString())
                        item.Checked = true;
                    else
                        item.Checked = false;
                }
            }

            if (multiSelect)
            {
                pauseClientToolStripMenuItem.Enabled = true;
                resumeClientToolStripMenuItem.Enabled = true;
            }
            else if (_masterServer.IsClientPaused(id))
            {
                pauseClientToolStripMenuItem.Enabled = false;
                resumeClientToolStripMenuItem.Enabled = true;
            }
            else
            {
                pauseClientToolStripMenuItem.Enabled = true;
                resumeClientToolStripMenuItem.Enabled = false;
            }
        }

        private void clientStatusGrid_MouseClick(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hitInfo = ClientStatusGrid.HitTest(e.X, e.Y);
            if (hitInfo.RowIndex == -1)
                return;

            // Multiple select!
            if (ModifierKeys != Keys.Control)
            {
                if (ClientStatusGrid.Rows[hitInfo.RowIndex].Selected == false)
                {
                    foreach (DataGridViewRow row in ClientStatusGrid.Rows)
                        row.Selected = false;
                }
            }
            ClientStatusGrid.Rows[hitInfo.RowIndex].Selected = true;

            if (e.Button != MouseButtons.Right)
                return;
            if (ClientStatusGrid.SelectedRows.Count == 0)
                return;
            SetupNodesMenu();

            contextMenuNodes.Items.Clear();
            contextMenuNodes.Items.AddRange(new ToolStripItem[]
                {
                    mnuSendKill,
                    showMessagesToolStripMenuItem,
                    resendConfigToolStripMenuItem,
                    configToolStripMenuItem,
                    renderPriorityToolStripMenuItem,
                    setupActivationTimeToolStripMenuItem,
                    pauseClientToolStripMenuItem,
                    resumeClientToolStripMenuItem,
                    closeNodeToolStripMenuItem,
                    stopAllNodesToolStripMenuItem,
                    cleanClientContentDirectoryToolStripMenuItem,
                    cleanAllClientsContentDirectoryToolStripMenuItem,
                    cleanClientOutputDirectoryToolStripMenuItem,
                    cleanAllClientsOutputDirectoryToolStripMenuItem
                });
            contextMenuNodes.Show(MousePosition.X, MousePosition.Y);
        }

        private void ConfigSet_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            int configNumber = (int) menuItem.Tag;

            foreach (DataGridViewRow row in ClientStatusGrid.SelectedRows)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);
                _masterServer.SetClientConfig(id, configNumber);
            }
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            if (_masterServer != null)
                _masterServer.KeepAlive();

            if (_clientListRefresh.ElapsedMilliseconds > 500)
            {
                _clientListRefresh.Reset();
                RepaintClientList();
            }

            if (_projectListRefresh.ElapsedMilliseconds > 1000)
            {
                _projectListRefresh.Reset();
                _projectListRefresh.Start();
                RepaintProjectList();
            }

            if (_finishedListRefresh.ElapsedMilliseconds > 1000)
            {
                _finishedListRefresh.Reset();
                RepaintFinishedList();
            }

            if (_imageRefresh.ElapsedMilliseconds > 1000)
            {
                _imageRefresh.Reset();
                DoShowImages();
            }

            int t = DateTime.Now.Hour*2 + (DateTime.Now.Minute < 30 ? 0 : 1);
            if (t != _lastActiveTime)
            {
                _lastActiveTime = t;
                BeginInvoke((MethodInvoker) delegate { RestoreClientList(); });
            }
        }

        private void pauseClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in ClientStatusGrid.SelectedRows)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);
                _masterServer.PauseClient(id);
            }
        }

        private void resumeClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in ClientStatusGrid.SelectedRows)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);
                _masterServer.ResumeClient(id);
            }
        }

        private void closeNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show("Are you sure you want to stop the selected node(s) remotely?", "Stop node",
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            foreach (DataGridViewRow row in ClientStatusGrid.SelectedRows)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);
                _masterServer.AddPriorityJob(id, new QuitJob());
            }
        }

        private void stopAllNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show("Are you sure you want to stop ALL the connected nodes remotely?", "Stop all nodes",
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            foreach (DataGridViewRow row in ClientStatusGrid.Rows)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);
                _masterServer.AddPriorityJob(id, new QuitJob());
            }
        }

        private void clientStatusGrid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (!_needToUpdateClient)
                return;
            if (ClientStatusGrid.SelectedRows.Count == 0)
                return;
            int id = Convert.ToInt32(ClientStatusGrid.SelectedRows[0].Cells[0].Value);
            _needToUpdateClient = false;
            ClientStatusGrid.EndEdit();
            DataGridViewRow row = ClientStatusGrid.SelectedRows[0];
            row.Cells[3].Selected = false;
            row.Cells[0].Selected = true;
            string newVal = (string) row.Cells[3].Value;

            List<string> configsName = _masterServer.ConfigNames;
            int configId = configsName.FindIndex(delegate(string x) { return x == newVal; });
            _masterServer.SetClientConfig(id, configId);

            _needToUpdateClient = true;
        }

        private void showMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ClientStatusGrid.SelectedRows.Count == 0)
                return;
            int id = Convert.ToInt32(ClientStatusGrid.SelectedRows[0].Cells[0].Value);

            _masterServer.AddPriorityJob(id, new SendLogJob(_eventBridge.ReceiveLog));
        }

        private void ShowClientLog(List<string> log)
        {
            BeginInvoke((MethodInvoker) delegate { DoShowClientLog(log); });
        }

        private void DoShowClientLog(List<string> log)
        {
            ClientLogWin dlg = new ClientLogWin(log);
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void editNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveProjectGrid.SelectedRows.Count == 0)
                return;
            int id = Convert.ToInt32(ActiveProjectGrid.SelectedRows[0].Cells[0].Value);
            AddProject dlg = new AddProject(_masterServer, _masterServer.GetProject(id));
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void projectStatusGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            editNodeToolStripMenuItem_Click(sender, e);
        }

        private void setupActivationTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ClientStatusGrid.SelectedRows.Count == 0)
                return;
            int id = Convert.ToInt32(ClientStatusGrid.SelectedRows[0].Cells[0].Value);

            ClientActiveWin dlg = new ClientActiveWin();
            dlg.ActiveHours = _masterServer.GetClientActivationTime(id);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _masterServer.SetClientActivationTime(id, dlg.ActiveHours);
                BeginInvoke((MethodInvoker) delegate { RestoreClientList(); });
            }
            dlg.Dispose();
        }

        private void jobsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            SetupJobMenu();
            jobsToolStripMenuItem.DropDownItems.Clear();
            jobsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
                {
                    addToolStripMenuItem,
                    editNodeToolStripMenuItem,
                    pauseToolStripMenuItem,
                    resumeToolStripMenuItem,
                    stopToolStripMenuItem
                });
        }

        private void nodesToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            SetupNodesMenu();
            nodesToolStripMenuItem.DropDownItems.Clear();
            nodesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
                {
                    mnuSendKill,
                    showMessagesToolStripMenuItem,
                    resendConfigToolStripMenuItem,
                    configToolStripMenuItem,
                    renderPriorityToolStripMenuItem,
                    setupActivationTimeToolStripMenuItem,
                    pauseClientToolStripMenuItem,
                    resumeClientToolStripMenuItem,
                    closeNodeToolStripMenuItem,
                    stopAllNodesToolStripMenuItem,
                    cleanClientContentDirectoryToolStripMenuItem,
                    cleanAllClientsContentDirectoryToolStripMenuItem,
                    cleanClientOutputDirectoryToolStripMenuItem,
                    cleanAllClientsOutputDirectoryToolStripMenuItem
                });
        }

        private void tool_MouseHover(object sender, EventArgs e)
        {
            if (sender is ToolStripButton)
            {
                ToolStripButton toolButton = sender as ToolStripButton;
                toolStripStatus.Text = toolButton.ToolTipText;
            }
            else
            {
                Control control = sender as Control;
                toolStripStatus.Text = (string) control.Tag;
            }
        }

        private void tool_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatus.Text = "";
        }

        private void clearListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show(
                    "Are you sure you want to clear the rendered list?\nThis affect all users and is permanent.",
                    "Clear rendered list", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
        }

        private void editProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int projectId = -1;
            string nodeText = "";

            string s = nodeText.Substring(nodeText.LastIndexOf('(') + 1);
            s = s.Substring(0, s.Length - 1);
            projectId = int.Parse(s);

            RenderProject project = _masterServer.GetProject(projectId);
            if (project == null)
            {
                MessageBox.Show("Cannot retreive project information", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            AddProject dlg = new AddProject(_masterServer, project);
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void resendConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in ClientStatusGrid.SelectedRows)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);
                _masterServer.AddPriorityJob(id, new SetupJob(true));
            }
        }

        private void cleanClientContentDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in ClientStatusGrid.SelectedRows)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);
                _masterServer.AddPriorityJob(id, new DeleteContentJob());
            }
        }

        private void cleanAllClientsContentDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in ClientStatusGrid.Rows)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);
                _masterServer.AddPriorityJob(id, new DeleteContentJob());
            }
        }

        private void cleanClientOutputDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in ClientStatusGrid.SelectedRows)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);
                _masterServer.AddPriorityJob(id, new DeleteOutputJob());
            }
        }

        private void cleanAllClientsOutputDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in ClientStatusGrid.Rows)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);
                _masterServer.AddPriorityJob(id, new DeleteOutputJob());
            }
        }

        private void ServerWin_Shown(object sender, EventArgs e)
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.Major.ToString();
            version = version + "." + Assembly.GetExecutingAssembly().GetName().Version.Minor;
            version = version + "." + Assembly.GetExecutingAssembly().GetName().Version.Build;

            Text = "Amleto Server " + version;

            BroadcastFinder findMaster = new BroadcastFinder();
            if (findMaster.Server != "") // Let's connect to the master
            {
                _isMaster = false;
                try
                {
                    // Find a free port for the callback...
                    for (int i = 10000; i < 60000; i++)
                    {
                        try
                        {
                            BinaryServerFormatterSinkProvider serverProv = new BinaryServerFormatterSinkProvider();
                            serverProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
                            BinaryClientFormatterSinkProvider clientProv = new BinaryClientFormatterSinkProvider();
                            IDictionary props = new Hashtable();

                            props["port"] = i;
                            _channel = new TcpChannel(props, clientProv, serverProv);
                            break;
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex, "Error creating TCP channel");
                        }
                    }

                    ChannelServices.RegisterChannel(_channel, false);
                    _masterServer = (MasterServer)Activator.CreateInstance(typeof(MasterServer),
                                                                            new object[] { Environment.UserName },
                                                                            new object[]
                                                                                {
                                                                                    new UrlAttribute("tcp://" +
                                                                                                     findMaster.Server +
                                                                                                     ":" +
                                                                                                     findMaster.Port)
                                                                                });

                    List<string> oldMessages = _masterServer.GetOldMessages();
                    foreach (string s in oldMessages)
                        DoConsumeMessage(s);
                    List<FinishedFrame> oldFrames = _masterServer.GetOldFrames();
                    DoAddFrames(oldFrames);

                    RepaintClientList();
                    RepaintProjectList();

                    Text = Text + " - Terminal";


                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Error initialising server");
                }
            }
            else // We should be master
            {
                _masterServer = new MasterServer(Environment.UserName);
                if (_masterServer.RestoreSettings() == false)
                {
                    SetupWin dlg = new SetupWin(_masterServer, false);
                    dlg.ScanAllConfigs();
                    if (dlg.ShowDialog() != DialogResult.OK)
                        Application.Exit();
                    _masterServer.Port = dlg.Port;
                    _masterServer.AutoOfferPort = dlg.AutoPort;
                    _masterServer.ReplaceConfigs(dlg.Configs);
                    _masterServer.LogFile = dlg.LogFile;
                    _masterServer.EmailFrom = dlg.EmailFrom;
                    _masterServer.SmtpServer = dlg.SmtpServer;
                    _masterServer.SmtpUsername = dlg.SmtpUsername;
                    _masterServer.SmtpPassword = dlg.SmtpPassword;
                    _masterServer.OfferWeb = dlg.OfferWeb;
                    _masterServer.OfferWebPort = dlg.OfferWebPort;
                    _masterServer.RenderBlocks = dlg.RenderBlocks;
                    string res = _masterServer.SetMappedDrives(dlg.MappedDrives);
                    if (res != "")
                        MessageBox.Show(res);
                    _masterServer.SaveSettings();
                }
                _isMaster = true;
                Text = Text + " - Master";
            }

            _eventBridge = new EventBridge(RefreshClientList, RefreshProjectList, RefreshFinishedList, ShowImage, ConsumeMessage, ShowClientLog);

            _masterServer.AddClientStatus(_eventBridge.ClientRefresh);
            _masterServer.AddProjectStatus(_eventBridge.ProjectRefresh);
            _masterServer.AddFinishedStatus(_eventBridge.FinishedRefresh);
            _masterServer.AddImagePreview(_eventBridge.ImagePreview);
            _masterServer.AddMessageConsumer(_eventBridge.MessageConsume);

            if (_isMaster)
            {
                _masterServer.Startup();

                // Restore previously saved jobs
                string loadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Amleto");
                loadPath = Path.Combine(loadPath, "RenderJobs");
                if (Directory.Exists(loadPath))
                {
                    DirectoryInfo bufferPath = new DirectoryInfo(loadPath);
                    foreach (FileInfo file in bufferPath.GetFiles("*.xml"))
                    {
                        RenderProject project = new RenderProject();
                        project = project.Load(file.FullName);
                        _masterServer.AddProject(project);
                    }
                }
            }

            DoConsumeMessage("Clearing thumbnail cache");
            string thumbnailPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Amleto");
            thumbnailPath = Path.Combine(thumbnailPath, @"Cache\thumbnails");
            try
            {
                if (Directory.Exists(thumbnailPath))
                    Directory.Delete(thumbnailPath, true);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error clearing thumbnail cache");
            }
        }

        private void FinishedProjectGridMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            DataGridView.HitTestInfo hitInfo = FinishedProjectGrid.HitTest(e.X, e.Y);

            if (hitInfo.RowIndex != -1)
                FinishedProjectGrid.Rows[hitInfo.RowIndex].Selected = true;

            if (FinishedProjectGrid.SelectedRows.Count == 0)
            {
                FinishedMenuEdit.Enabled = false;
                FinishedMenuClear.Enabled = false;
            }
            else
            {
                FinishedMenuEdit.Enabled = true;
                FinishedMenuClear.Enabled = true;
            }

            contextMenuFinished.Items.Clear();
            contextMenuFinished.Items.AddRange(new ToolStripItem[]
                                                   {
                                                       FinishedMenuEdit,
                                                       FinishedMenuClear,
                                                       FinishedMenuClearAll
                                                   });

            contextMenuFinished.Show(MousePosition.X, MousePosition.Y);
        }

        private void FinishedMenuEditClick(object sender, EventArgs e)
        {
            if (FinishedProjectGrid.SelectedRows.Count == 0)
                return;

            int id = Convert.ToInt32(FinishedProjectGrid.SelectedRows[0].Cells[0].Value);
            AddProject dlg = new AddProject(_masterServer, _masterServer.GetProject(id));
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void FinishedMenuClearClick(object sender, EventArgs e)
        {
            if (FinishedProjectGrid.SelectedRows.Count == 0)
                return;

            int id = Convert.ToInt32(FinishedProjectGrid.SelectedRows[0].Cells[0].Value);
            RenderProject project = _masterServer.GetProject(id);
            _masterServer.RemoveFinished(project);
        }

        private void FinishedMenuClearAllClick(object sender, EventArgs e)
        {
            _masterServer.RemoveAllFinished();
        }

        private void RenderedImagesSelectionChanged(object sender, EventArgs e)
        {
            if (RenderedImages.SelectedRows.Count > 0)
            {
                DataGridViewRow currentRow = RenderedImages.SelectedRows[0];

                ImagePreviews.Items.Clear();
                ImagePreviews.SuspendLayout();

                List<String> fileList = null;
                if (RenderedImages.CurrentRow.Tag.GetType() == typeof(List<String>))
                    fileList = (List<String>)RenderedImages.CurrentRow.Tag;

                Cursor.Current = Cursors.WaitCursor;
                BeginInvoke((MethodInvoker)delegate { UpdateThumbnails(fileList, currentRow); });

                ImagePreviews.ResumeLayout();
            }
        }

        private void UpdateThumbnails(List<string> fileList, DataGridViewRow currentRow)
        {
            foreach (String f in fileList)
            {
                Image thumb = Thumbnail.CreateImageThumbnail(currentRow.Cells[0].Value.ToString(), f, _thumbSize);

                if (RenderedImages.CurrentRow == currentRow)
                {
                    if (thumb != null)
                        ImagePreviews.Items.Add(f, thumb);
                    else
                        ImagePreviews.Items.Add(f);
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void ImagePreviewsItemDoubleClick(object sender, Manina.Windows.Forms.ItemClickEventArgs e)
        {
            ImageListViewItem item = null;
            if (ImagePreviews.SelectedItems.Count > 0)
            {
                item = ImagePreviews.SelectedItems[0];
                string imageFile = item.FileName;
                ImageViewer view = new ImageViewer(imageFile);
                view.Show();
            }
        }
    }
}