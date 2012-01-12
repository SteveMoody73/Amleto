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
        public string ProgPath = "";
        public string ConfigPath = "";
        public string PluginPath = "";
        public string LogFile = "";
        public bool LogEnabled;
        public string EmailFrom = "amleto@yourdomain.com";
        public string SmtpServer = "mail.yourdomain.com";
        public string SmtpUsername = "";
        public string SmtpPassword = "";
        public bool OfferWeb = true;
        public int OfferWebPort = 9080;

        public List<MapDrive> MappedDrives = new List<MapDrive>();
        public List<ConfigSet> Configs = new List<ConfigSet>();
        
		private MasterServer _server;
        private int _selectedConfig;
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
            ProgPath = textProgPath.Text;
            ConfigPath = textConfigPath.Text;
            PluginPath = textPluginPath.Text;
            LogFile = textLogFile.Text;
            EmailFrom = textMailFrom.Text;
            SmtpServer = textSMTPServer.Text;
            SmtpUsername = textSTMPUser.Text;
            SmtpPassword = textSMTPPassword.Text;
            OfferWeb = offerWebInterface.Checked;
            OfferWebPort = Convert.ToInt32(webPort.Text);

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
            textProgPath.Text = ProgPath;
            textConfigPath.Text = ConfigPath;
            textPluginPath.Text = PluginPath;
            textLogFile.Text = LogFile;
            textMailFrom.Text = EmailFrom;
            textSMTPServer.Text = SmtpServer;
            textSTMPUser.Text = SmtpUsername;
            textSMTPPassword.Text = SmtpPassword;
            offerWebInterface.Checked = OfferWeb;
            webPort.Text = "" + OfferWebPort;
            cbEnableLogging.Checked = LogEnabled;

            // If log file name is empty, generate a default log path
            if (textLogFile.Text == "")
                textLogFile.Text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Amleto\\Amleto.log";


            foreach(ConfigSet c in Configs)
                selectConfig.Items.Add(c.Name);
            selectConfig.SelectedIndex=0;
            selectConfig.Items.Add("-- Create New Config --");
        }

        private void btnSelectProg_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = textProgPath.Text;

            if (dlg.ShowDialog() != DialogResult.OK || dlg.SelectedPath == "")
                return;
            textProgPath.Text = dlg.SelectedPath;
            dlg.Dispose();
        }

        private void btnSelectConfig_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = textConfigPath.Text;
            
            if (dlg.ShowDialog() != DialogResult.OK)
            {
                dlg.Dispose();
                return;
            }

			DirectoryInfo cfgFolder = new DirectoryInfo(dlg.SelectedPath);
        	FileInfo[] cfgFiles = cfgFolder.GetFiles("LW*.CFG");

			if (cfgFiles.Length == 0)
            {
                MessageBox.Show("LW*.CFG file not found in the specified directory.", "Wrong config directory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dlg.Dispose();
                return;
            }

            textConfigPath.Text = dlg.SelectedPath;
            dlg.Dispose();

            AutoSearch();
        }

        private void btnSelectPlugin_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = textPluginPath.Text;
            
            if (dlg.ShowDialog() != DialogResult.OK || dlg.SelectedPath == "")
                return;
            textPluginPath.Text = dlg.SelectedPath;
            dlg.Dispose();
        }

        public void AutoSearch()
        {
            string searchConfig;

            if (textConfigPath.Text != "")
            {
                searchConfig = textConfigPath.Text;
                ConfigPath = searchConfig;
            }
            else
            {
                searchConfig = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.Personal)).FullName;
                string newtekFolder = searchConfig + "\\.NewTek";

				if (Directory.Exists(searchConfig + "\\.NewTek\\LightWave\\11.0"))
					searchConfig += "\\.NewTek\\LightWave\\11.1";
				else if (Directory.Exists(searchConfig + "\\.NewTek\\LightWave\\10.1"))
                    searchConfig += "\\.NewTek\\LightWave\\10.1";
                else if (Directory.Exists(searchConfig + "\\.NewTek\\LightWave\\10.0"))
                    searchConfig += "\\.NewTek\\LightWave\\10.0";
                else
                    searchConfig += "\\.NewTek\\LightWave\\9.0";

				if (_server.FileExists(searchConfig + "\\LW11-64.CFG") ||
					_server.FileExists(searchConfig + "\\LW11.CFG") || 
					_server.FileExists(searchConfig + "\\LW10-64.CFG") || 
                    _server.FileExists(searchConfig + "\\LW10.CFG") || 
                    _server.FileExists(searchConfig + "\\LW9.CFG") || 
                    _server.FileExists(searchConfig + "\\LW9-64.CFG") || 
                    _server.FileExists(searchConfig + "\\LW8.CFG") || 
                    _server.FileExists(searchConfig + "\\LW3.CFG") || 
                    _server.FileExists(searchConfig + "\\LW.CFG"))
                    ConfigPath = searchConfig;
            }

            if(ConfigPath != "")
            {
                string[] configLines=null;

				if (_server.FileExists(ConfigPath + "\\LW11-64.CFG"))
					configLines = _server.FileReadAllLines(ConfigPath + "\\LW11-64.CFG");
				else if (_server.FileExists(ConfigPath + "\\LW11.CFG"))
					configLines = _server.FileReadAllLines(ConfigPath + "\\LW11.CFG");
				else if (_server.FileExists(ConfigPath + "\\LW10-64.CFG"))
                    configLines = _server.FileReadAllLines(ConfigPath + "\\LW10-64.CFG");
                else if (_server.FileExists(ConfigPath + "\\LW10.CFG"))
                    configLines = _server.FileReadAllLines(ConfigPath + "\\LW10.CFG"); 
                else if (_server.FileExists(ConfigPath + "\\LW9.CFG"))
                    configLines = _server.FileReadAllLines(ConfigPath + "\\LW9.CFG");
                else if (_server.FileExists(ConfigPath + "\\LW9-64.CFG"))
                    configLines = _server.FileReadAllLines(ConfigPath + "\\LW9-64.CFG");
                else if (_server.FileExists(ConfigPath + "\\LW8.CFG"))
                    configLines = _server.FileReadAllLines(ConfigPath + "\\LW8.CFG");
                else if (_server.FileExists(ConfigPath + "\\LW3.CFG"))
                    configLines = _server.FileReadAllLines(ConfigPath + "\\LW3.CFG");
                else if (_server.FileExists(ConfigPath + "\\LW.CFG"))
                    configLines = _server.FileReadAllLines(ConfigPath + "\\LW.CFG");

            	if (configLines != null)
            	{
            		foreach (string t in configLines)
            		{
            			if (t.StartsWith("CommandDirectory "))
            			{
            				ProgPath = t.Substring(17);
            				textProgPath.Text = ProgPath;
            				break;
            			}
            		}

					if (_server.FileExists(ConfigPath + "\\LWEXT11-64.CFG"))
						configLines = _server.FileReadAllLines(ConfigPath + "\\LWEXT11-64.CFG");
					else if (_server.FileExists(ConfigPath + "\\LWEXT11.CFG"))
						configLines = _server.FileReadAllLines(ConfigPath + "\\LWEXT11.CFG");
					else if (_server.FileExists(ConfigPath + "\\LWEXT10-64.CFG"))
            			configLines = _server.FileReadAllLines(ConfigPath + "\\LWEXT10-64.CFG");
            		else if (_server.FileExists(ConfigPath + "\\LWEXT10.CFG"))
            			configLines = _server.FileReadAllLines(ConfigPath + "\\LWEXT10.CFG");
            		else if (_server.FileExists(ConfigPath + "\\LWEXT9.CFG"))
            			configLines = _server.FileReadAllLines(ConfigPath + "\\LWEXT9.CFG");
            		else if (_server.FileExists(ConfigPath + "\\LWEXT9-64.CFG"))
            			configLines = _server.FileReadAllLines(ConfigPath + "\\LWEXT9-64.CFG");
            		else if (_server.FileExists(ConfigPath + "\\LWEXT8.CFG"))
            			configLines = _server.FileReadAllLines(ConfigPath + "\\LWEXT8.CFG");
            		else if (_server.FileExists(ConfigPath + "\\LWEXT3.CFG"))
            			configLines = _server.FileReadAllLines(ConfigPath + "\\LWEXT3.CFG");
            		else if (_server.FileExists(ConfigPath + "\\LWEXT.CFG"))
            			configLines = _server.FileReadAllLines(ConfigPath + "\\LWEXT.CFG");

            		if (configLines.Length == 0)
            		{
            			if (ProgPath != "" && Directory.Exists(ProgPath+"\\plugins"))
            			{
            				PluginPath = ProgPath + "\\plugins";
            				textPluginPath.Text = PluginPath;
            			}
            		}
            		else
            		{
            			foreach (string t in configLines)
            			{
            				if (t.Trim().StartsWith("Module "))
            				{
            					PluginPath = t.Trim().Replace("\\\\", "\\").Replace("\"", "").Substring(7);
            					PluginPath = Directory.GetParent(Directory.GetParent(PluginPath).FullName).FullName;
            					textPluginPath.Text = PluginPath;
            					break;
            				}
            			}
            		}
            	}
            }

			if (_server.FileExists(ConfigPath + "\\LWEXT9-64.CFG") || _server.FileExists(ConfigPath + "\\LWEXT10-64.CFG") || _server.FileExists(ConfigPath + "\\LWEXT11-64.CFG"))
                bits64.Checked = true;
        }

        private void textConfigPath_TextChanged(object sender, EventArgs e)
        {
            Configs[_selectedConfig].ServerConfigPath = textConfigPath.Text;
        }

        private void textProgPath_TextChanged(object sender, EventArgs e)
        {
            Configs[_selectedConfig].ServerProgramPath = textProgPath.Text;
        }

        private void textPluginPath_TextChanged(object sender, EventArgs e)
        {
            Configs[_selectedConfig].ServerPluginPath = textPluginPath.Text;
        }

        private void selectConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectConfig.SelectedIndex >= Configs.Count)
            {
                PromptNewConfigWin dlg = new PromptNewConfigWin();
                if (dlg.ShowDialog() == DialogResult.Cancel)
                {
                    selectConfig.SelectedIndex = _selectedConfig;
                    dlg.Dispose();
                    return;
                }
                bool canAdd = true;
                foreach (ConfigSet cs in Configs)
                {
                    if (cs.Name == dlg.ConfigName)
                    {
                        MessageBox.Show("You already used this config name.", "Cannot use this name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        canAdd = false;
                        break;
                    }
                }

                if (dlg.ConfigName == "ClientsConfig" || dlg.ConfigName == "NetDrives")
                {
                    MessageBox.Show("This is a reserved name.", "Cannot use this name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    canAdd = false;
                }

                if (canAdd)
                {
                    ConfigSet c = new ConfigSet();
                    c.Name = dlg.ConfigName;
                    dlg.Dispose();
                    Configs.Add(c);
                    _selectedConfig = selectConfig.SelectedIndex;
                    selectConfig.Items[_selectedConfig] = c.Name;
                    selectConfig.Items.Add("-- Create New Config --");
                }
            }
            else
            {
                _selectedConfig = selectConfig.SelectedIndex;
                textConfigPath.Text = Configs[_selectedConfig].ServerConfigPath;
                textProgPath.Text = Configs[_selectedConfig].ServerProgramPath;
                textPluginPath.Text = Configs[_selectedConfig].ServerPluginPath;
                if (Configs[_selectedConfig].PtrSize == 4)
                {
                    bits32.Checked = true;
                    bits64.Checked = false;
                }
                else
                {
                    bits64.Checked = true;
                    bits32.Checked = false;
                }
            }
            if (selectConfig.SelectedIndex == 0)
                btnDeleteConfig.Enabled = false;
            else
                btnDeleteConfig.Enabled = true;
        }

        private void btnDeleteConfig_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this configuration?", "Delete config", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            Configs.RemoveAt(_selectedConfig);
            int n=_selectedConfig;
            selectConfig.SelectedIndex = 0;
            selectConfig.Items.RemoveAt(n);
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

        private void bits32_CheckedChanged(object sender, EventArgs e)
        {
            if (bits32.Checked)
                Configs[_selectedConfig].PtrSize = 4;
            else
                Configs[_selectedConfig].PtrSize = 8;
        }

        private void bits64_CheckedChanged(object sender, EventArgs e)
        {
            if (bits32.Checked)
                Configs[_selectedConfig].PtrSize = 4;
            else
                Configs[_selectedConfig].PtrSize = 8;
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
            OpenFileDialog dialog = new OpenFileDialog();
            
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
    }
}