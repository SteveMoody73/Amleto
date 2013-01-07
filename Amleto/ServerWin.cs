using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;
using RemoteExecution;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.IO;
using System.Runtime.Remoting.Activation;
using System.Collections;
using RemoteExecution.Jobs;

namespace Amleto
{
    public partial class ServerWin : Form
    {
        MasterServer _masterServer;
        bool _isMaster;
        TcpChannel _channel;

        private List<ClientConnection> _clients;
        private List<RenderProject> _projects;
        private readonly object _clientsLock = new object();
        private readonly object _projectsLock = new object();

        private EventBridge _eventBridge;

        private Stopwatch _clientListRefresh = new Stopwatch();
        private Stopwatch _projectListRefresh = new Stopwatch();
        private Stopwatch _imageRefresh = new Stopwatch();

        private int _lastActiveTime = -1;

        private bool _needToUpdateClient = true;

        private Queue<FinishedFrame> _framesToAdd=new Queue<FinishedFrame>();

		public ServerWin()
        {
            InitializeComponent();

            // Try to recover old setting...
            if (Properties.Settings.Default.NeedToUpgradeSettings)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.NeedToUpgradeSettings = false;
                Properties.Settings.Default.Save();
            }

            clientStatusGrid.DataError += dataGrid_DataError;
            projectStatusGrid.DataError += dataGrid_DataError;

            Size = Properties.Settings.Default.WinSize;
            Location = Properties.Settings.Default.WinPosition;
            autoRenderLast.Checked = Properties.Settings.Default.AutoShowLast;
            textPreviewSpeed.Text = "" + Properties.Settings.Default.PlaySpeed;
        }

        void dataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void ServerWin_Load(object sender, EventArgs e)
        {
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
            while (_framesToAdd.Count > 0)
            {
                FinishedFrame frame = _framesToAdd.Dequeue();
                DoShowImage(frame.Nodename, frame.Filename);
            }
        }

        private void DoShowImage(string nodename, string fname)
        {
            if (nodename == null)
            {
                BeginInvoke((MethodInvoker) delegate() { UpdatePreviewPanel(fname); });
                return;
            }

            TreeNode baseNode = null;
            for (int i = renderTree.Nodes.Count - 1; i >= 0; i--)
            {
                if (renderTree.Nodes[i].Text == nodename)
                {
                    baseNode = renderTree.Nodes[i];
                    break;
                }
            }
            if (baseNode == null)
            {
                baseNode = new TreeNode(nodename);
                renderTree.Nodes.Add(baseNode);
            }
            TreeNode child = AddSortSceneBranch(baseNode, fname);

            if (autoRenderLast.Checked == true && btnPlay.Checked == false && _framesToAdd.Count == 0)
                renderTree.SelectedNode = child;
        }

        private void UpdatePreviewPanel(string fname)
        {
            try
            {
                MemoryStream mem = new MemoryStream(_masterServer.FileReadAllBytes(fname));
                Invoke((MethodInvoker)delegate()
                                      	{
                                      		try { UpdatePreviewPanel(new Bitmap(mem)); }
											catch (Exception ex) { Debug.WriteLine("Error updating preview panel:" + ex); }
                                      	});
                mem.Close();
                mem.Dispose();
                splitContainer3.Panel2.AutoScrollMinSize = pictureRender.Image.Size;
            }
            catch (Exception ex)
            {
				Debug.WriteLine("Error updating preview panel: " + ex);
            }

        }

        private void UpdatePreviewPanel(Image img)
        {
            pictureRender.Image = img;
        }

        private TreeNode AddSortSceneBranch(TreeNode branch, string toAdd)
        {
            for (int i = 0; i < branch.Nodes.Count; i++)
            {
                if (branch.Nodes[i].Text.CompareTo(toAdd) > 0)
                    return branch.Nodes.Insert(i, toAdd);
            }
            return  branch.Nodes.Add(toAdd);
        }

        private TreeNode SortSceneBranch(TreeNode branch,string toAdd)
        {
            List<string> elements = new List<string>();
            
			for (int i = 0; i < branch.Nodes.Count; i++)
                elements.Add(branch.Nodes[i].Text);
            
			if(toAdd != null)
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

        private void DoAddFrames(List<FinishedFrame> frames)
        {
            TreeNode baseNode = null;
        	string prevNodeName = "-----------------------";

            foreach (FinishedFrame frame in frames)
            {
                if (prevNodeName != frame.Nodename)
                {
                    for (int i = renderTree.Nodes.Count - 1; i >= 0; i--)
                    {
                        if (renderTree.Nodes[i].Text == frame.Nodename)
                        {
                            baseNode = renderTree.Nodes[i];
                            break;
                        }
                    }
                    if (baseNode == null)
                    {
                        baseNode = new TreeNode(frame.Nodename);
                        renderTree.Nodes.Add(baseNode);
                    }
                    prevNodeName = frame.Nodename;
                }
                TreeNode child = new TreeNode(frame.Filename);
            	if (baseNode != null) baseNode.Nodes.Add(child);
            }

            for (int i = 0; i < renderTree.Nodes.Count; i++)
            {
                SortSceneBranch(renderTree.Nodes[i],null);
            }
        }

        private void ConsumeMessage(string msg)
        {
            BeginInvoke((MethodInvoker)delegate { DoConsumeMessage(msg);  });
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

                while (messageList.Rows.Count > 300)
                    messageList.Rows.RemoveAt(0);

                if (messageList.SelectedRows.Count > 0)
                    messageList.SelectedRows[0].Selected = false;

                row.Height = 17;
                messageList.Rows.Add(row);
                row.Selected = true;
                messageList.FirstDisplayedScrollingRowIndex = messageList.Rows.Count - 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("Invalid message: " + msg);
                Debug.WriteLine("Invalid message: " + e);
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

        private void DoRepaintClientList()
        {
            List<string> oldSelected = new List<string>();
            foreach (DataGridViewRow row in clientStatusGrid.SelectedRows)
                oldSelected.Add((string)row.Cells[0].Value);

			int vpos = clientStatusGrid.FirstDisplayedScrollingRowIndex;

            lock (_clientsLock)
            {
                List<ClientConnection> hosts = _clients;
                try
                {
                    clientStatusGrid.Rows.Clear();
                }
                catch (Exception ex)
                {
					Debug.WriteLine("Error clearing grid row" + ex);
                }

                List<string> configsName = _masterServer.ConfigNames;

                foreach (ClientConnection client in hosts)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    DataGridViewCell cell = new DataGridViewTextBoxCell();

					try { cell.Value = "" + client.Id; }
					catch (Exception ex) { Debug.WriteLine("Error setting client id: " + ex); }
					try { row.Cells.Add(cell); }
					catch (Exception ex) { Debug.WriteLine("Error adding cell client id: " + ex); }

                    cell = new DataGridViewTextBoxCell();
                    try { cell.Value = client.HostName + " (" + client.IPAddress + ")"; }
					catch (Exception ex) { Debug.WriteLine("Error setting hostname: " + ex); }
					try { row.Cells.Add(cell); }
					catch (Exception ex) { Debug.WriteLine("Error adding hostname: " + ex); }

                    cell = new DataGridViewTextBoxCell();
					try { cell.Value = "" + client.Instance; }
					catch (Exception ex) { Debug.WriteLine("Error setting client instance: " + ex); }
                    try { row.Cells.Add(cell); }
					catch (Exception ex) { Debug.WriteLine("Error adding client instance: " + ex); }
					
                    DataGridViewComboBoxCell dcell = new DataGridViewComboBoxCell();

					foreach (string s in configsName)
                    {
                            try { dcell.Items.Add(s); }
							catch (Exception ex) { Debug.WriteLine("Error adding config item: " + ex); }
					}
                    
					try { dcell.Value = configsName[client.Config]; }
					catch (Exception ex) { Debug.WriteLine("Error adding config name: " + ex); }

					try { row.Cells.Add(dcell); }
					catch (Exception ex) { Debug.WriteLine("Error adding cell: " + ex); }

                    cell = new DataGridViewImageCell();
                    try
                    {
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
                    }
					catch (Exception ex)
					{
						Debug.WriteLine("Error setting cell items: " + ex);
					}

                    try { row.Cells.Add(cell); }
					catch (Exception ex) { Debug.WriteLine("Error adding cell: " + ex); }

					cell = new DataGridViewTextBoxCell();
					try { cell.Value = client.CurrentJob; }
					catch (Exception ex) { Debug.WriteLine("Error setting cell currentjob: " + ex); }
					try { row.Cells.Add(cell); }
					catch (Exception ex) { Debug.WriteLine("Error adding cell currentjob: " + ex); }
					try { clientStatusGrid.Rows.Add(row); }
					catch (Exception ex) { Debug.WriteLine("Error adding client status row: " + ex); }

                    try
                    {
                        if (oldSelected.Contains((string)row.Cells[0].Value))
                            row.Selected = true;
                        else
                            row.Selected = false;
                    }
                    catch (Exception ex)
					{
						Debug.WriteLine("Error selecting row: " + ex);
					}
                }
                try
                {
                    clientStatusGrid.FirstDisplayedScrollingRowIndex = vpos;
                }
                catch (Exception ex)
                {
					Debug.WriteLine("Error setting client status grid row index:" + ex);
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

        private void DoRepaintProjectList()
        {
            string oldSelected = "";
            if (clientStatusGrid.SelectedRows.Count > 0)
                oldSelected = (string)clientStatusGrid.SelectedRows[0].Cells[0].Value;
            int vpos = clientStatusGrid.FirstDisplayedScrollingRowIndex;

            lock (_projectsLock)
            {
                try
                {
                    projectStatusGrid.Rows.Clear();
                }
                catch (Exception ex)
                {
					Debug.WriteLine("Error clearing project status rows: " + ex);
                }

                foreach (RenderProject project in _projects)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    
                    // Project ID
                    DataGridViewCell cell = new DataGridViewTextBoxCell();                    
                    cell.Value = "" + project.ProjectId;
                    row.Cells.Add(cell);
                    // Project Name
                    cell = new DataGridViewTextBoxCell();
                    cell.Value = project.SceneFile.Substring(project.SceneFile.LastIndexOf('\\') + 1);
                    row.Cells.Add(cell);
                    // Project Owner
                    cell = new DataGridViewTextBoxCell();
                    cell.Value = project.Owner;
                    row.Cells.Add(cell);
                    // Start Frame
                    cell = new DataGridViewTextBoxCell();
                    cell.Value = "" + project.StartFrame;
                    row.Cells.Add(cell);
                    // End Frame
                    cell = new DataGridViewTextBoxCell();
                    cell.Value = "" + project.EndFrame;
                    row.Cells.Add(cell);
                    // Number of frames to render
                    cell = new DataGridViewTextBoxCell();
                    cell.Value = "" + project.StartJobs;
                    row.Cells.Add(cell);
                    // Percent completed
                    cell = new DataGridViewTextBoxCell();
                    if (project.StartJobs == 0)
                        cell.Value = "";
                    else
                    {
						string status = ((project.StartJobs - project.NbRemainingJobs()) * 100 / project.StartJobs) + "%";
						if (project.Paused)
							status = "(Paused) " + status;
						
						cell.Value = status;
                    }                    
					row.Cells.Add(cell);
                    // Elapsed Time
                    cell = new DataGridViewTextBoxCell();
                    if (project.StartTimeSet)
                    {
                        TimeSpan elapsed = DateTime.Now - project.StartTime;
                        cell.Value = String.Format("{0:00}:{1:00}:{2:00}",
                            (elapsed.Days * 24) + elapsed.Hours, elapsed.Minutes, elapsed.Seconds);
                    }
                    else
                        cell.Value = "";
                    row.Cells.Add(cell);
                    // Estimated time
                    cell = new DataGridViewTextBoxCell();
                    if (project.StartTimeSet && project.RenderedFrames.Count > 0)
                    {
                        TimeSpan elapsed = DateTime.Now - project.StartTime;
                        long frameTime = elapsed.Ticks / (long)project.RenderedFrames.Count;
                        long totalframes = (long)project.StartJobs;
                        if (project.SaveAlpha)
                            totalframes *= 2;
                        long remainFrames = (long)(totalframes - project.RenderedFrames.Count);
                        long estimated = remainFrames * frameTime;
                        TimeSpan remain = new TimeSpan(estimated);
                        cell.Value = String.Format("{0:00}:{1:00}:{2:00}",
                            (remain.Days * 24) + remain.Hours, remain.Minutes, remain.Seconds);
                    }
                    else
                        cell.Value = "";
                    row.Cells.Add(cell);
                    
                    try
                    {
                        projectStatusGrid.Rows.Add(row);
                    }
					catch (Exception ex)
					{
						Debug.WriteLine("Error adding project status row: " + ex);
					}

                    if ((string)row.Cells[0].Value == oldSelected)
                        row.Selected = true;
                }
                try
                {
                    clientStatusGrid.FirstDisplayedScrollingRowIndex = vpos;
                }
                catch (Exception ex)
                {
					Debug.WriteLine("Error adding project status: " + ex);
                }
            }
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ServerWin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.WinSize = this.Size;
            Properties.Settings.Default.WinPosition = this.Location;
            Properties.Settings.Default.AutoShowLast = autoRenderLast.Checked;
            Properties.Settings.Default.PlaySpeed = Convert.ToInt32(textPreviewSpeed.Text);
            Properties.Settings.Default.Save();

            if (_isMaster && MessageBox.Show("Are you sure you want to exit?", "Closing Amleto", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                e.Cancel = true;
            else
            {
                try
                {
                    if (_isMaster)
                        _masterServer.Shutdown();
                    _masterServer.Disconnect();
                }
				catch (Exception ex)
				{
					Debug.WriteLine("Error shutting down server: " + ex);
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
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            AddProject dlg = new AddProject(_masterServer, files[0]);
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void mnuOptions_Click(object sender, EventArgs e)
        {
            bool needToReset=false;

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
                string res=_masterServer.SetMappedDrives(dlg.MappedDrives);
                if (res != "")
                    MessageBox.Show(res);

                _masterServer.LoadFileFormats();
                _masterServer.SaveSettings();
                DoRepaintClientList();
            }
            dlg.Dispose();

            if (needToReset)
            {
                _masterServer.ResetPort(_masterServer.AutoOfferPort, _masterServer.Port);
            }
        }

        private void renderTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level >= 1)
                DoShowImage(null, e.Node.Text);
        }

        private void btnPlay_CheckedChanged(object sender, EventArgs e)
        {
            if (btnPlay.Checked)
            {
                if (renderTree.SelectedNode == null)
                {
                    btnPlay.Checked = false;
                    MessageBox.Show("Select first either a rendered scene or an image within a rendered scene.");
                    return;
                }
                textPreviewSpeed.ReadOnly = true;
                playTimer.Interval = 1000 / Convert.ToInt32(textPreviewSpeed.Text);
                playTimer.Enabled = true;
                playTimer.Start();
            }
            else
            {
                textPreviewSpeed.ReadOnly = false;
                playTimer.Enabled = false;
                playTimer.Stop();
            }
        }

        private void playTimer_Tick(object sender, EventArgs e)
        {
            if (!btnPlay.Checked)
                return;

            if (renderTree.SelectedNode == null)
                return;

            if (renderTree.SelectedNode.Level == 0)
            {
                if (renderTree.SelectedNode.Nodes.Count == 0)
                    return;
                renderTree.SelectedNode = renderTree.SelectedNode.Nodes[0];
            }
            else
            {
                if (renderTree.SelectedNode.NextNode == null)
                    renderTree.SelectedNode = renderTree.SelectedNode.Parent.Nodes[0];
                else
                    renderTree.SelectedNode = renderTree.SelectedNode.NextNode;
            }
        }

        private void textPreviewSpeed_Leave(object sender, EventArgs e)
        {
            int a;
            try
            {
                a = Convert.ToInt32(textPreviewSpeed.Text);
            }
			catch (Exception ex)
			{
				Debug.WriteLine("Error setting text preview speed: " + ex);
				a = 10;
                textPreviewSpeed.Text = "" + a;
            }

            if (a < 1)
            {
                a = 1;
                textPreviewSpeed.Text = "" + a;
            }
            if (a > 60)
            {
                a = 60;
                textPreviewSpeed.Text = "" + a;
            }
        }

        private void mnuSendKill_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in clientStatusGrid.SelectedRows)
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
            if (projectStatusGrid.SelectedRows.Count == 0)
                return;
            int id = Convert.ToInt32(projectStatusGrid.SelectedRows[0].Cells[0].Value);
            _masterServer.PauseProject(id);
        }

        private void resumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (projectStatusGrid.SelectedRows.Count == 0)
                return;
            int id = Convert.ToInt32(projectStatusGrid.SelectedRows[0].Cells[0].Value);
            _masterServer.ResumeProject(id);
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (projectStatusGrid.SelectedRows.Count == 0)
                return;
            int id = Convert.ToInt32(projectStatusGrid.SelectedRows[0].Cells[0].Value);
            _masterServer.StopProject(id);
        }

        private void changePriortyStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            foreach (DataGridViewRow row in clientStatusGrid.SelectedRows)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);
                _masterServer.SetClientPriority(id, (ProcessPriorityClass)Enum.Parse(typeof(ProcessPriorityClass), item.Text));
            }
       }

        private void SetupJobMenu()
        {
            if (projectStatusGrid.SelectedRows.Count == 0)
            {
                editNodeToolStripMenuItem.Enabled=false;
                pauseToolStripMenuItem.Enabled=false;
                resumeToolStripMenuItem.Enabled=false;
                stopToolStripMenuItem.Enabled = false;
                return;
            }

            editNodeToolStripMenuItem.Enabled = true;
            stopToolStripMenuItem.Enabled = true;

            int id = Convert.ToInt32(projectStatusGrid.SelectedRows[0].Cells[0].Value);

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

        private void projectStatusGrid_MouseClick(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hitInfo = projectStatusGrid.HitTest(e.X, e.Y);
            if (hitInfo.RowIndex == -1)
                return;
            projectStatusGrid.Rows[hitInfo.RowIndex].Selected = true;

            if (e.Button != MouseButtons.Right)
                return;
            if (projectStatusGrid.SelectedRows.Count == 0)
                return;

            SetupJobMenu();

            contextMenuProjects.Items.Clear();
            contextMenuProjects.Items.AddRange(new ToolStripItem[] {
            addToolStripMenuItem,
            editNodeToolStripMenuItem,
            pauseToolStripMenuItem,
            resumeToolStripMenuItem,
            stopToolStripMenuItem});
            contextMenuProjects.Show(MousePosition.X, MousePosition.Y);
        }

        private void SetupNodesMenu()
        {
            if (clientStatusGrid.SelectedRows.Count == 0)
            {
                mnuSendKill.Enabled=false;
                resendConfigToolStripMenuItem.Enabled = false;
                showMessagesToolStripMenuItem.Enabled=false;
                configToolStripMenuItem.Enabled=false;
                renderPriorityToolStripMenuItem.Enabled=false;
                setupActivationTimeToolStripMenuItem.Enabled=false;
                pauseClientToolStripMenuItem.Enabled=false;
                resumeClientToolStripMenuItem.Enabled=false;
                closeNodeToolStripMenuItem.Enabled=false;
                stopAllNodesToolStripMenuItem.Enabled = false;
                cleanClientContentDirectoryToolStripMenuItem.Enabled=false;
                cleanAllClientsContentDirectoryToolStripMenuItem.Enabled=true;
                cleanClientOutputDirectoryToolStripMenuItem.Enabled=false;
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

            bool multiSelect = clientStatusGrid.SelectedRows.Count > 1;

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

            int id = Convert.ToInt32(clientStatusGrid.SelectedRows[0].Cells[0].Value);
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
            DataGridView.HitTestInfo hitInfo = clientStatusGrid.HitTest(e.X, e.Y);
            if (hitInfo.RowIndex == -1)
                return;

            // Multiple select!
            if (ModifierKeys != Keys.Control)
            {
                if (clientStatusGrid.Rows[hitInfo.RowIndex].Selected == false)
                {
                    foreach (DataGridViewRow row in clientStatusGrid.Rows)
                        row.Selected = false;
                }
            }
            clientStatusGrid.Rows[hitInfo.RowIndex].Selected = true;

            if (e.Button != MouseButtons.Right)
                return;
            if (clientStatusGrid.SelectedRows.Count == 0)
                return;
            SetupNodesMenu();

            contextMenuNodes.Items.Clear();
            contextMenuNodes.Items.AddRange(new ToolStripItem[] {
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
            cleanAllClientsOutputDirectoryToolStripMenuItem});
            contextMenuNodes.Show(MousePosition.X, MousePosition.Y);
        }

        void ConfigSet_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            int configNumber = (int)menuItem.Tag;

            foreach (DataGridViewRow row in clientStatusGrid.SelectedRows)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);
                _masterServer.SetClientConfig(id, configNumber);
            }
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            _masterServer.KeepAlive();
            if (_clientListRefresh.ElapsedMilliseconds > 500)
            {
                _clientListRefresh.Reset();
                DoRepaintClientList();
            }

            if (_projectListRefresh.ElapsedMilliseconds > 500)
            {
                _projectListRefresh.Reset();
                DoRepaintProjectList();
            }

            if (_imageRefresh.ElapsedMilliseconds > 500)
            {
                _imageRefresh.Reset();
                DoShowImages();
            }

            int t = DateTime.Now.Hour * 2 + (DateTime.Now.Minute < 30 ? 0 : 1);
            if(t != _lastActiveTime)
            {
                _lastActiveTime=t;
                BeginInvoke((MethodInvoker)delegate { RestoreClientList(); });
            }
        }

        private void pauseClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in clientStatusGrid.SelectedRows)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);
                _masterServer.PauseClient(id);
            }
        }

        private void resumeClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in clientStatusGrid.SelectedRows)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);
                _masterServer.ResumeClient(id);
            }
        }

        private void closeNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to stop the selected node(s) remotely?", "Stop node", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            foreach (DataGridViewRow row in clientStatusGrid.SelectedRows)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);
                _masterServer.AddPriorityJob(id, new QuitJob());
            }
        }

        private void stopAllNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to stop ALL the connected nodes remotely?", "Stop all nodes", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            foreach (DataGridViewRow row in clientStatusGrid.Rows)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);
                _masterServer.AddPriorityJob(id, new QuitJob());
            }
        }

        private void clientStatusGrid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (!_needToUpdateClient)
                return;
            if (clientStatusGrid.SelectedRows.Count == 0)
                return;
            int id = Convert.ToInt32(clientStatusGrid.SelectedRows[0].Cells[0].Value);
            _needToUpdateClient = false;
            clientStatusGrid.EndEdit();
            DataGridViewRow row = clientStatusGrid.SelectedRows[0];
            row.Cells[3].Selected = false;
            row.Cells[0].Selected = true;
            string newVal=(string)row.Cells[3].Value;

            List<string> configsName = _masterServer.ConfigNames;
            int configId=configsName.FindIndex(delegate(string x) { return x == newVal; });
            _masterServer.SetClientConfig(id, configId);

            _needToUpdateClient = true;
        }

        private void showMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clientStatusGrid.SelectedRows.Count == 0)
                return;
            int id = Convert.ToInt32(clientStatusGrid.SelectedRows[0].Cells[0].Value);

            _masterServer.AddPriorityJob(id, new SendLogJob(_eventBridge.ReceiveLog));
        }

        private void ShowClientLog(List<string> log)
        {
            BeginInvoke((MethodInvoker)delegate { DoShowClientLog(log); });
        }

        private void DoShowClientLog(List<string> log)
        {
            ClientLogWin dlg = new ClientLogWin(log);
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void editNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (projectStatusGrid.SelectedRows.Count == 0)
                return;
            int id = Convert.ToInt32(projectStatusGrid.SelectedRows[0].Cells[0].Value);
            AddProject dlg=new AddProject(_masterServer,_masterServer.GetProject(id));
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void projectStatusGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            editNodeToolStripMenuItem_Click(sender, e);
        }

        private void setupActivationTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clientStatusGrid.SelectedRows.Count == 0)
                return;
            int id = Convert.ToInt32(clientStatusGrid.SelectedRows[0].Cells[0].Value);

            ClientActiveWin dlg = new ClientActiveWin();
            dlg.ActiveHours = _masterServer.GetClientActivationTime(id);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _masterServer.SetClientActivationTime(id, dlg.ActiveHours);
                BeginInvoke((MethodInvoker)delegate { RestoreClientList(); });
            }
            dlg.Dispose();
        }

        private void jobsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            SetupJobMenu();
            jobsToolStripMenuItem.DropDownItems.Clear();
            jobsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            addToolStripMenuItem,
            editNodeToolStripMenuItem,
            pauseToolStripMenuItem,
            resumeToolStripMenuItem,
            stopToolStripMenuItem});
        }

        private void nodesToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            SetupNodesMenu();
            nodesToolStripMenuItem.DropDownItems.Clear();
            nodesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
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
            cleanAllClientsOutputDirectoryToolStripMenuItem});
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
                toolStripStatus.Text = (string)control.Tag;
            }
        }

        private void tool_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatus.Text = "";
        }

        private void clearListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear the rendered list?\nThis affect all users and is permanent.", "Clear rendered list", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            renderTree.Nodes.Clear();
        }

        private void editProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (renderTree.SelectedNode == null)
                return;
            int projectId = -1;
            string nodeText = "";
            if (renderTree.SelectedNode.Parent == null)
                nodeText = renderTree.SelectedNode.Text;
            else
                nodeText = renderTree.SelectedNode.Parent.Text;
            string s = nodeText.Substring(nodeText.LastIndexOf('(') + 1);
            s = s.Substring(0, s.Length - 1);
            projectId = int.Parse(s);

            RenderProject project = _masterServer.GetProject(projectId);
            if (project == null)
            {
                MessageBox.Show("Cannot retreive project information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AddProject dlg = new AddProject(_masterServer, project);
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void resendConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in clientStatusGrid.SelectedRows)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);
                _masterServer.AddPriorityJob(id, new SetupJob(true));
            }
        }

        private void cleanClientContentDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in clientStatusGrid.SelectedRows)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);
                _masterServer.AddPriorityJob(id, new DeleteContentJob());
            }
        }

        private void cleanAllClientsContentDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in clientStatusGrid.Rows)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);
                _masterServer.AddPriorityJob(id, new DeleteContentJob());
            }
        }

        private void cleanClientOutputDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in clientStatusGrid.SelectedRows)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);
                _masterServer.AddPriorityJob(id, new DeleteOutputJob());
            }
        }

        private void cleanAllClientsOutputDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in clientStatusGrid.Rows)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);
                _masterServer.AddPriorityJob(id, new DeleteOutputJob());
            }
        }

        private void ServerWin_Shown(object sender, EventArgs e)
        {
            Text = "Amleto Server " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
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
							Debug.WriteLine("Error creating TCPChannel" + ex);
                        }
                    }

                    ChannelServices.RegisterChannel(_channel, false);
                    _masterServer = (MasterServer)Activator.CreateInstance(typeof(MasterServer), 
						new object[] { Environment.UserName }, 
						new object[] { new UrlAttribute("tcp://" + findMaster.Server + ":" + findMaster.Port) });

                    List<string> oldMessages = _masterServer.GetOldMessages();
                    foreach (string s in oldMessages)
                        DoConsumeMessage(s);
                    List<FinishedFrame> oldFrames = _masterServer.GetOldFrames();
                    DoAddFrames(oldFrames);

                    DoRepaintClientList();
                    DoRepaintProjectList();

                    Text = Text + " - Terminal";
                }
                catch (Exception ex)
                {
					Debug.WriteLine("Error showing server window: " + ex);
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
                    string res = _masterServer.SetMappedDrives(dlg.MappedDrives);
                    if (res != "")
                        MessageBox.Show(res);
                    _masterServer.SaveSettings();
                }
                _isMaster = true;
                Text = Text + " - Master";
            }

            _eventBridge = new EventBridge(RefreshClientList, RefreshProjectList, ShowImage, ConsumeMessage, ShowClientLog);

            _masterServer.AddClientStatus(_eventBridge.ClientRefresh);
            _masterServer.AddProjectStatus(_eventBridge.ProjectRefresh);
            _masterServer.AddImagePreview(_eventBridge.ImagePreview);
            _masterServer.AddMessageConsumer(_eventBridge.MessageConsume);
            if (_isMaster)
                _masterServer.Startup();
        }
    }
}