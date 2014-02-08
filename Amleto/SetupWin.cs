using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using RemoteExecution;

namespace Amleto
{
    public partial class SetupWin : Form
    {
        public int Port = 2080;
        public bool AutoPort = true;
        public string LogFile = "";
        public bool LogEnabled;
        public string EmailFrom = "amleto@yourdomain.com";
        public string SmtpServer = "mail.yourdomain.com";
        public string SmtpUsername = "";
        public string SmtpPassword = "";
        public bool OfferWeb = true;
        public int OfferWebPort = 9080;
        public int RenderBlocks = 5;

        public List<MapDrive> MappedDrives = new List<MapDrive>();
        public List<ConfigSet> Configs = new List<ConfigSet>();
        
		private MasterServer _server;
		private List<string> _availableDrives = new List<string>();

        public SetupWin(MasterServer server, bool canQuit)
        {
            InitializeComponent();
            _server = server;
            
			if (canQuit == false)
                btnCancel.Enabled = false;
            else
                tabs.TabPages.Remove(tabWeb);

			DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Port = Convert.ToInt32(textPort.Text);
            AutoPort = checkAutoPort.Checked;
            LogFile = textLogFile.Text;
            EmailFrom = textMailFrom.Text;
            SmtpServer = textSMTPServer.Text;
            SmtpUsername = textSTMPUser.Text;
            SmtpPassword = textSMTPPassword.Text;
            OfferWeb = offerWebInterface.Checked;
            OfferWebPort = Convert.ToInt32(webPort.Text);
            RenderBlocks = Convert.ToInt32(renderBlock.Text);

            MappedDrives.Clear();
            foreach (DataGridViewRow row in listMapDrives.Rows)
            {
                if ((string)row.Cells[1].Value == "")
                    continue;

                MapDrive mapDrive = new MapDrive();
                mapDrive.Drive = (string)row.Cells[0].Value;
                mapDrive.Share = (string)row.Cells[1].Value;
                mapDrive.Username = (string)row.Cells[2].Value;
                mapDrive.Password = (string)row.Cells[3].Value;
                MappedDrives.Add(mapDrive);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void SetupWin_Load(object sender, EventArgs e)
        {


            string account = _server.GetUser();
            if (account == "SYSTEM")
            {
                lblMappedDrives.Text = "If you want to use network drives, you need to run the service with a local machine account and not a local system account.\n\nTo do so, open the services panel, right click on the Amleto server service, and choose \"Properties\". In the second tab specify a username and a password, and restart the service.";
                listMapDrives.Visible = false;
                btnAddDrive.Visible = false;
                btnDelDrive.Visible = false;
            }
            else
            {
                // Build a list of available drives
                for (char letter = 'D'; letter <= 'Z'; letter++)
                    _availableDrives.Add("" + letter + ":\\");
                List<string> usedDrives = _server.DriveList();

                foreach (MapDrive mapDrive in MappedDrives)
                {
                    int idx = usedDrives.FindIndex(delegate(string x) { return x.StartsWith(mapDrive.Drive); });
                    if (idx != -1)
                        usedDrives.RemoveAt(idx);
                }

                foreach (string s in usedDrives)
                {
                    string d = s.Substring(0, 3);
                    try
                    {
                        _availableDrives.RemoveAt(_availableDrives.IndexOf(d));
                    }
					catch (Exception ex)
					{
						Debug.WriteLine("Error removing list of available drives: " + ex);
					}
                }

                // Build map drives list
                foreach (MapDrive mapDrive in MappedDrives)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    DataGridViewComboBoxCell letterSelect = new DataGridViewComboBoxCell();
                    /*for (char letter = 'E'; letter <= 'Z'; letter++)
                        letterSelect.Items.Add("" + letter + ":\\");*/
                    foreach (string s in _availableDrives)
                        letterSelect.Items.Add(s);
                    letterSelect.Value = mapDrive.Drive;
                    row.Cells.Add(letterSelect);

                    DataGridViewCell cell = new DataGridViewTextBoxCell();
                    cell.Value = mapDrive.Share;
                    row.Cells.Add(cell);

                    cell = new DataGridViewTextBoxCell();
                    cell.Value = mapDrive.Username;
                    row.Cells.Add(cell);

                    cell = new DataGridViewTextBoxCell();
                    cell.Value = mapDrive.Password;
                    row.Cells.Add(cell);

                    listMapDrives.Rows.Add(row);
                }
            }

            checkAutoPort.Checked = AutoPort;
            textPort.Text = "" + Port;
            textLogFile.Text = LogFile;
            textMailFrom.Text = EmailFrom;
            textSMTPServer.Text = SmtpServer;
            textSTMPUser.Text = SmtpUsername;
            textSMTPPassword.Text = SmtpPassword;
            offerWebInterface.Checked = OfferWeb;
            webPort.Text = "" + OfferWebPort;
            cbEnableLogging.Checked = LogEnabled;
            renderBlock.Text = RenderBlocks.ToString();

            // If log file name is empty, generate a default log path
            if (textLogFile.Text == "")
            {
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Amleto");
                textLogFile.Text = Path.Combine(path, "AmletoServer.log");
            }


            DisplayConfigs();
        }

        public void DisplayConfigs()
        {
            ConfigList.Rows.Clear();

            foreach (ConfigSet config in Configs)
            {
                DataGridViewRow row = new DataGridViewRow();
                DataGridViewCell cell = new DataGridViewTextBoxCell();

                if (config.DefaultConfig)
                    cell.Value = @"✔";
                else 
                    cell.Value = "";
                row.Cells.Add(cell);

                cell = new DataGridViewTextBoxCell();
                cell.Value = config.Name;
                row.Cells.Add(cell);

                cell = new DataGridViewTextBoxCell();
                cell.Value = config.BitSize.ToString();
                row.Cells.Add(cell);

                cell = new DataGridViewTextBoxCell();
                cell.Value = config.ConfigPath;
                row.Cells.Add(cell);

                ConfigList.Rows.Add(row);
            }
        }

        public void ScanConfig(string configFolder)
        {
            string[] configLines;

            DirectoryInfo configPath = new DirectoryInfo(configFolder);
            foreach (FileInfo configFile in configPath.GetFiles("*.CFG"))
            {
                configLines = File.ReadAllLines(configFile.FullName);

                foreach (string line in configLines)
                {
                    if (line.StartsWith("CommandDirectory "))
                    {
                        // This is the main lightwave config file
                        string progPath = line.Substring(17);
                        string pluginPath = "";
                        string supportPath = "";
                        bool bit64;
                        int lwVersion;

                        string fileName = Path.GetFileName(configFile.FullName).ToUpper();
                        bit64 = fileName.Contains("-64");

                        if (bit64)
                        {
                            fileName = fileName.Substring(0, fileName.IndexOf("-64"));
                            lwVersion = int.Parse(fileName.Substring(2));
                        }
                        else
                        {
                            fileName = fileName.Substring(0, fileName.IndexOf("."));
                            lwVersion = int.Parse(fileName.Substring(2));
                        }

                        if (progPath != "")
                        {
                            pluginPath = Path.Combine(Path.GetDirectoryName(progPath), "Plugins");
                            if (!Directory.Exists(pluginPath))
                                pluginPath = "";

                            supportPath = Path.Combine(Path.GetDirectoryName(progPath), "Support");
                            if (!Directory.Exists(supportPath))
                                supportPath = "";
                        }

                        // Check that the config file has not already been added
                        bool exists = false;
                        foreach (ConfigSet config in Configs)
                        {
                            if (config.ConfigFile == configFile.FullName)
                                exists = true;
                        }

                        if (exists == false)
                        {
                            ConfigSet config = new ConfigSet();
                            config.ConfigFile = configFile.FullName;
                            config.ConfigPath = Path.GetDirectoryName(configFile.FullName);
                            config.ProgramPath = progPath;
                            config.PluginPath = pluginPath;
                            config.SupportPath = supportPath;
                            config.LightwaveVersion = lwVersion;
                            config.Name = Path.GetFileNameWithoutExtension(configFile.FullName);
                            if (bit64)
                                config.BitSize = 64;
                            else
                                config.BitSize = 32;
                            Configs.Add(config);
                        }
                        break;
                    }
                }
            }
        }
        
        public void ScanAllConfigs()
        {
            string newtekFolder = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.Personal)).FullName;
            newtekFolder = Path.Combine(newtekFolder, ".NewTek");
            newtekFolder = Path.Combine(newtekFolder, "LightWave");

            DirectoryInfo configFolder = new DirectoryInfo(newtekFolder);

            foreach (DirectoryInfo folder in configFolder.GetDirectories())
            {
                ScanConfig(folder.FullName);
            }
        }

        private void offerWebInterface_CheckedChanged(object sender, EventArgs e)
        {
            lblWebPort.Enabled = offerWebInterface.Checked;
            webPort.Enabled = offerWebInterface.Checked;
            lblTestUrl.Enabled = offerWebInterface.Checked;
            testUrl.Enabled = offerWebInterface.Checked;
        }

        private void webPort_TextChanged(object sender, EventArgs e)
        {
            int wport=9080;
            try
            {
                wport = Convert.ToInt32(webPort.Text);
            }
			catch (Exception ex)
			{
				Debug.WriteLine("Error converting webport to number: " + ex);
			}
			

            webPort.Text = "" + wport;
            testUrl.Text = "http://localhost:" + webPort + "/";
        }

        private void textPort_Validating(object sender, CancelEventArgs e)
        {
            int a;
            if (int.TryParse(((TextBox)sender).Text, out a) == false)
            {
                errorProvider.SetError((TextBox)sender, "Must be a number");
                e.Cancel = true;
                return;
            }
            if (a < 1 || a > 62000)
            {
                errorProvider.SetError((TextBox)sender, "Must be a number between 1 and 62000");
                e.Cancel = true;
                return;
            }
            errorProvider.SetError((TextBox)sender, "");
        }

        private void webPort_Validating(object sender, CancelEventArgs e)
        {
            int a;
            if (int.TryParse(((TextBox)sender).Text, out a) == false)
            {
                errorProvider.SetError((TextBox)sender, "Must be a number");
                e.Cancel = true;
                return;
            }
            if (a < 1 || a > 62000)
            {
                errorProvider.SetError((TextBox)sender, "Must be a number between 1 and 62000");
                e.Cancel = true;
                return;
            }
            errorProvider.SetError((TextBox)sender, "");
        }

        private void btnAddDrive_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            DataGridViewComboBoxCell letterSelect = new DataGridViewComboBoxCell();
            foreach (string s in _availableDrives)
                letterSelect.Items.Add(s);
            if (_availableDrives.Count > 0)
                letterSelect.Value = _availableDrives[0];
            row.Cells.Add(letterSelect);

            DataGridViewCell cell = new DataGridViewTextBoxCell();
            cell.Value = "";
            row.Cells.Add(cell);

            cell = new DataGridViewTextBoxCell();
            cell.Value = "";
            row.Cells.Add(cell);

            cell = new DataGridViewTextBoxCell();
            cell.Value = "";
            row.Cells.Add(cell);

            listMapDrives.Rows.Add(row);
            row.Cells[0].Selected = true; ;
        }

        private void btnDelDrive_Click(object sender, EventArgs e)
        {
            if(listMapDrives.SelectedCells.Count == 0)
                return;
            if(MessageBox.Show("Are you sure you want to remove this mapped drive?","Remove mapped drive",MessageBoxButtons.YesNo,MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            listMapDrives.Rows.RemoveAt(listMapDrives.SelectedCells[0].RowIndex);
        }

        private void SelectLogFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            
			dialog.Filter = "Log Files|*.log|All Files|*.*";
            dialog.InitialDirectory =  Path.GetDirectoryName(textLogFile.Text);
            dialog.Title = "Select a log File";

            if (dialog.ShowDialog() == DialogResult.OK)
                textLogFile.Text = dialog.FileName;
        }

        private void cbEnableLogging_CheckedChanged(object sender, EventArgs e)
        {
            LogEnabled = cbEnableLogging.Checked;
        }

        private void AddConfigClick(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() != DialogResult.OK)
            {
                dlg.Dispose();
                return;
            }

            DirectoryInfo cfgFolder = new DirectoryInfo(dlg.SelectedPath);
            FileInfo[] cfgFiles = cfgFolder.GetFiles("LW*.CFG");

            if (cfgFiles.Length == 0)
            {
                MessageBox.Show("There was no LW*.CFG file not found in the specified directory.", "Wrong config directory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dlg.Dispose();
                return;
            }

            ScanConfig(dlg.SelectedPath);
            dlg.Dispose();
            DisplayConfigs();
        }

        private void SetDefaultClick(object sender, EventArgs e)
        {
            if (ConfigList.SelectedCells.Count == 0)
                return;

            int index = ConfigList.SelectedCells[0].RowIndex;

            foreach (ConfigSet config in Configs)
                config.DefaultConfig = false;

            Configs[index].DefaultConfig = true;
            ConfigSet swap = Configs[index];
            Configs[index] = Configs[0];
            Configs[0] = swap;

            DisplayConfigs();
        }

        private void DeleteConfigClick(object sender, EventArgs e)
        {
            if (ConfigList.SelectedCells.Count == 0)
                return;

            if (MessageBox.Show("Are you sure you want to remove this config setting?", "Remove Config", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            
            Configs.RemoveAt(ConfigList.SelectedCells[0].RowIndex);
            DisplayConfigs();
        }

        private void textStartFrame_Validating(object sender, CancelEventArgs e)
        {

        }

        private void textEndFrame_Validating(object sender, CancelEventArgs e)
        {

        }

        private void positiveNumber_Validating(object sender, CancelEventArgs e)
        {

        }

        private void positiveZeroDouble_Validating(object sender, CancelEventArgs e)
        {

        }
    }
}