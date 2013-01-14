namespace Amleto
{
    partial class SetupWin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupWin));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkAutoPort = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textPort = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.textLogFile = new System.Windows.Forms.TextBox();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabConfigs = new System.Windows.Forms.TabPage();
            this.SetDefault = new System.Windows.Forms.Button();
            this.AddConfig = new System.Windows.Forms.Button();
            this.DeleteConfig = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.ConfigList = new System.Windows.Forms.DataGridView();
            this.ConfigDefault = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConfigName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConfigBits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConfigDir = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabLogging = new System.Windows.Forms.TabPage();
            this.cbEnableLogging = new System.Windows.Forms.CheckBox();
            this.SelectLogFile = new System.Windows.Forms.Button();
            this.tabNetwork = new System.Windows.Forms.TabPage();
            this.tabEmail = new System.Windows.Forms.TabPage();
            this.label21 = new System.Windows.Forms.Label();
            this.textSMTPPassword = new System.Windows.Forms.TextBox();
            this.textSTMPUser = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.textSMTPServer = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.textMailFrom = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.tabWeb = new System.Windows.Forms.TabPage();
            this.label23 = new System.Windows.Forms.Label();
            this.testUrl = new System.Windows.Forms.TextBox();
            this.lblTestUrl = new System.Windows.Forms.Label();
            this.webPort = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.lblWebPort = new System.Windows.Forms.Label();
            this.offerWebInterface = new System.Windows.Forms.CheckBox();
            this.tabDrives = new System.Windows.Forms.TabPage();
            this.listMapDrives = new System.Windows.Forms.DataGridView();
            this.driveLetter = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.driveShare = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.driveUsername = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drivePassword = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddDrive = new System.Windows.Forms.Button();
            this.btnDelDrive = new System.Windows.Forms.Button();
            this.lblMappedDrives = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tabs.SuspendLayout();
            this.tabConfigs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConfigList)).BeginInit();
            this.tabLogging.SuspendLayout();
            this.tabNetwork.SuspendLayout();
            this.tabEmail.SuspendLayout();
            this.tabWeb.SuspendLayout();
            this.tabDrives.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listMapDrives)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(373, 311);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(292, 311);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(446, 48);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Find free port:";
            // 
            // checkAutoPort
            // 
            this.checkAutoPort.AutoSize = true;
            this.checkAutoPort.Checked = true;
            this.checkAutoPort.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkAutoPort.Location = new System.Drawing.Point(106, 53);
            this.checkAutoPort.Name = "checkAutoPort";
            this.checkAutoPort.Size = new System.Drawing.Size(15, 14);
            this.checkAutoPort.TabIndex = 2;
            this.checkAutoPort.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Port number:";
            // 
            // textPort
            // 
            this.textPort.Location = new System.Drawing.Point(106, 99);
            this.textPort.Name = "textPort";
            this.textPort.Size = new System.Drawing.Size(61, 20);
            this.textPort.TabIndex = 5;
            this.textPort.Text = "2080";
            this.textPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textPort.Validating += new System.ComponentModel.CancelEventHandler(this.textPort_Validating);
            // 
            // label7
            // 
            this.label7.ForeColor = System.Drawing.Color.Green;
            this.label7.Location = new System.Drawing.Point(6, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(401, 47);
            this.label7.TabIndex = 0;
            this.label7.Text = resources.GetString("label7.Text");
            // 
            // label8
            // 
            this.label8.ForeColor = System.Drawing.Color.Green;
            this.label8.Location = new System.Drawing.Point(6, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(427, 16);
            this.label8.TabIndex = 3;
            this.label8.Text = "Used in case the \"Find free port\" option is disabled.";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 68);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(47, 13);
            this.label14.TabIndex = 4;
            this.label14.Text = "Log File:";
            // 
            // textLogFile
            // 
            this.textLogFile.Location = new System.Drawing.Point(62, 65);
            this.textLogFile.Name = "textLogFile";
            this.textLogFile.Size = new System.Drawing.Size(293, 20);
            this.textLogFile.TabIndex = 5;
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabConfigs);
            this.tabs.Controls.Add(this.tabLogging);
            this.tabs.Controls.Add(this.tabNetwork);
            this.tabs.Controls.Add(this.tabEmail);
            this.tabs.Controls.Add(this.tabWeb);
            this.tabs.Controls.Add(this.tabDrives);
            this.tabs.Location = new System.Drawing.Point(8, 60);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(450, 245);
            this.tabs.TabIndex = 1;
            // 
            // tabConfigs
            // 
            this.tabConfigs.Controls.Add(this.SetDefault);
            this.tabConfigs.Controls.Add(this.AddConfig);
            this.tabConfigs.Controls.Add(this.DeleteConfig);
            this.tabConfigs.Controls.Add(this.label4);
            this.tabConfigs.Controls.Add(this.ConfigList);
            this.tabConfigs.Location = new System.Drawing.Point(4, 22);
            this.tabConfigs.Name = "tabConfigs";
            this.tabConfigs.Padding = new System.Windows.Forms.Padding(3);
            this.tabConfigs.Size = new System.Drawing.Size(442, 219);
            this.tabConfigs.TabIndex = 2;
            this.tabConfigs.Text = "LW Configs";
            this.tabConfigs.UseVisualStyleBackColor = true;
            // 
            // SetDefault
            // 
            this.SetDefault.Location = new System.Drawing.Point(199, 190);
            this.SetDefault.Name = "SetDefault";
            this.SetDefault.Size = new System.Drawing.Size(75, 23);
            this.SetDefault.TabIndex = 5;
            this.SetDefault.Text = "Default";
            this.SetDefault.UseVisualStyleBackColor = true;
            this.SetDefault.Click += new System.EventHandler(this.SetDefaultClick);
            // 
            // AddConfig
            // 
            this.AddConfig.Location = new System.Drawing.Point(280, 190);
            this.AddConfig.Name = "AddConfig";
            this.AddConfig.Size = new System.Drawing.Size(75, 23);
            this.AddConfig.TabIndex = 4;
            this.AddConfig.Text = "Add";
            this.AddConfig.UseVisualStyleBackColor = true;
            this.AddConfig.Click += new System.EventHandler(this.AddConfigClick);
            // 
            // DeleteConfig
            // 
            this.DeleteConfig.Location = new System.Drawing.Point(361, 190);
            this.DeleteConfig.Name = "DeleteConfig";
            this.DeleteConfig.Size = new System.Drawing.Size(75, 23);
            this.DeleteConfig.TabIndex = 3;
            this.DeleteConfig.Text = "Delete";
            this.DeleteConfig.UseVisualStyleBackColor = true;
            this.DeleteConfig.Click += new System.EventHandler(this.DeleteConfigClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(172, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "List of available LightWave configs";
            // 
            // ConfigList
            // 
            this.ConfigList.AllowUserToAddRows = false;
            this.ConfigList.AllowUserToDeleteRows = false;
            this.ConfigList.AllowUserToResizeRows = false;
            this.ConfigList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ConfigList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ConfigDefault,
            this.ConfigName,
            this.ConfigBits,
            this.ConfigDir});
            this.ConfigList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.ConfigList.Location = new System.Drawing.Point(6, 24);
            this.ConfigList.Name = "ConfigList";
            this.ConfigList.RowHeadersVisible = false;
            this.ConfigList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ConfigList.Size = new System.Drawing.Size(430, 161);
            this.ConfigList.TabIndex = 1;
            // 
            // ConfigDefault
            // 
            this.ConfigDefault.HeaderText = "Default";
            this.ConfigDefault.Name = "ConfigDefault";
            this.ConfigDefault.ReadOnly = true;
            this.ConfigDefault.Width = 50;
            // 
            // ConfigName
            // 
            this.ConfigName.HeaderText = "Name";
            this.ConfigName.Name = "ConfigName";
            this.ConfigName.ReadOnly = true;
            // 
            // ConfigBits
            // 
            this.ConfigBits.HeaderText = "32/64 Bit";
            this.ConfigBits.Name = "ConfigBits";
            this.ConfigBits.ReadOnly = true;
            this.ConfigBits.Width = 75;
            // 
            // ConfigDir
            // 
            this.ConfigDir.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ConfigDir.HeaderText = "Path";
            this.ConfigDir.Name = "ConfigDir";
            this.ConfigDir.ReadOnly = true;
            // 
            // tabLogging
            // 
            this.tabLogging.Controls.Add(this.cbEnableLogging);
            this.tabLogging.Controls.Add(this.SelectLogFile);
            this.tabLogging.Controls.Add(this.textLogFile);
            this.tabLogging.Controls.Add(this.label14);
            this.tabLogging.Location = new System.Drawing.Point(4, 22);
            this.tabLogging.Name = "tabLogging";
            this.tabLogging.Padding = new System.Windows.Forms.Padding(3);
            this.tabLogging.Size = new System.Drawing.Size(442, 219);
            this.tabLogging.TabIndex = 0;
            this.tabLogging.Text = "Logging";
            this.tabLogging.UseVisualStyleBackColor = true;
            // 
            // cbEnableLogging
            // 
            this.cbEnableLogging.AutoSize = true;
            this.cbEnableLogging.Location = new System.Drawing.Point(62, 42);
            this.cbEnableLogging.Name = "cbEnableLogging";
            this.cbEnableLogging.Size = new System.Drawing.Size(100, 17);
            this.cbEnableLogging.TabIndex = 8;
            this.cbEnableLogging.Text = "Enable Logging";
            this.cbEnableLogging.UseVisualStyleBackColor = true;
            this.cbEnableLogging.CheckedChanged += new System.EventHandler(this.cbEnableLogging_CheckedChanged);
            // 
            // SelectLogFile
            // 
            this.SelectLogFile.Image = global::Amleto.Properties.Resources.folder_explore;
            this.SelectLogFile.Location = new System.Drawing.Point(374, 63);
            this.SelectLogFile.Name = "SelectLogFile";
            this.SelectLogFile.Size = new System.Drawing.Size(31, 23);
            this.SelectLogFile.TabIndex = 7;
            this.SelectLogFile.UseVisualStyleBackColor = true;
            this.SelectLogFile.Click += new System.EventHandler(this.SelectLogFile_Click);
            // 
            // tabNetwork
            // 
            this.tabNetwork.Controls.Add(this.textPort);
            this.tabNetwork.Controls.Add(this.label2);
            this.tabNetwork.Controls.Add(this.checkAutoPort);
            this.tabNetwork.Controls.Add(this.label3);
            this.tabNetwork.Controls.Add(this.label7);
            this.tabNetwork.Controls.Add(this.label8);
            this.tabNetwork.Location = new System.Drawing.Point(4, 22);
            this.tabNetwork.Name = "tabNetwork";
            this.tabNetwork.Padding = new System.Windows.Forms.Padding(3);
            this.tabNetwork.Size = new System.Drawing.Size(442, 219);
            this.tabNetwork.TabIndex = 1;
            this.tabNetwork.Text = "Network";
            this.tabNetwork.UseVisualStyleBackColor = true;
            // 
            // tabEmail
            // 
            this.tabEmail.Controls.Add(this.label21);
            this.tabEmail.Controls.Add(this.textSMTPPassword);
            this.tabEmail.Controls.Add(this.textSTMPUser);
            this.tabEmail.Controls.Add(this.label20);
            this.tabEmail.Controls.Add(this.textSMTPServer);
            this.tabEmail.Controls.Add(this.label19);
            this.tabEmail.Controls.Add(this.textMailFrom);
            this.tabEmail.Controls.Add(this.label18);
            this.tabEmail.Controls.Add(this.label17);
            this.tabEmail.Location = new System.Drawing.Point(4, 22);
            this.tabEmail.Name = "tabEmail";
            this.tabEmail.Size = new System.Drawing.Size(442, 219);
            this.tabEmail.TabIndex = 3;
            this.tabEmail.Text = "Email Setup";
            this.tabEmail.UseVisualStyleBackColor = true;
            // 
            // label21
            // 
            this.label21.ForeColor = System.Drawing.Color.Green;
            this.label21.Location = new System.Drawing.Point(12, 12);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(413, 33);
            this.label21.TabIndex = 0;
            this.label21.Text = "Settings to use if you want to use the email notification feature of Amleto. Inco" +
                "rrect settings may prevent emails notifications.";
            // 
            // textSMTPPassword
            // 
            this.textSMTPPassword.Location = new System.Drawing.Point(109, 126);
            this.textSMTPPassword.Name = "textSMTPPassword";
            this.textSMTPPassword.PasswordChar = '*';
            this.textSMTPPassword.Size = new System.Drawing.Size(316, 20);
            this.textSMTPPassword.TabIndex = 8;
            // 
            // textSTMPUser
            // 
            this.textSTMPUser.Location = new System.Drawing.Point(109, 100);
            this.textSTMPUser.Name = "textSTMPUser";
            this.textSTMPUser.Size = new System.Drawing.Size(316, 20);
            this.textSTMPUser.TabIndex = 6;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(12, 129);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(89, 13);
            this.label20.TabIndex = 7;
            this.label20.Text = "SMTP Password:";
            // 
            // textSMTPServer
            // 
            this.textSMTPServer.Location = new System.Drawing.Point(109, 74);
            this.textSMTPServer.Name = "textSMTPServer";
            this.textSMTPServer.Size = new System.Drawing.Size(316, 20);
            this.textSMTPServer.TabIndex = 4;
            this.textSMTPServer.Text = "mail.yourdomain.com";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(12, 103);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(91, 13);
            this.label19.TabIndex = 5;
            this.label19.Text = "SMTP Username:";
            // 
            // textMailFrom
            // 
            this.textMailFrom.Location = new System.Drawing.Point(109, 48);
            this.textMailFrom.Name = "textMailFrom";
            this.textMailFrom.Size = new System.Drawing.Size(316, 20);
            this.textMailFrom.TabIndex = 2;
            this.textMailFrom.Text = "amleto@yourdomain.com";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(12, 77);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(72, 13);
            this.label18.TabIndex = 3;
            this.label18.Text = "SMTP server:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(12, 51);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(89, 13);
            this.label17.TabIndex = 1;
            this.label17.Text = "Sending address:";
            // 
            // tabWeb
            // 
            this.tabWeb.Controls.Add(this.label23);
            this.tabWeb.Controls.Add(this.testUrl);
            this.tabWeb.Controls.Add(this.lblTestUrl);
            this.tabWeb.Controls.Add(this.webPort);
            this.tabWeb.Controls.Add(this.label24);
            this.tabWeb.Controls.Add(this.lblWebPort);
            this.tabWeb.Controls.Add(this.offerWebInterface);
            this.tabWeb.Location = new System.Drawing.Point(4, 22);
            this.tabWeb.Name = "tabWeb";
            this.tabWeb.Size = new System.Drawing.Size(442, 219);
            this.tabWeb.TabIndex = 4;
            this.tabWeb.Text = "Web Interface";
            this.tabWeb.UseVisualStyleBackColor = true;
            // 
            // label23
            // 
            this.label23.ForeColor = System.Drawing.Color.Green;
            this.label23.Location = new System.Drawing.Point(9, 13);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(413, 33);
            this.label23.TabIndex = 5;
            this.label23.Text = "If you change these settings you will need to restart Amleto in order to make the" +
                "m active.";
            // 
            // testUrl
            // 
            this.testUrl.Enabled = false;
            this.testUrl.Location = new System.Drawing.Point(146, 105);
            this.testUrl.Name = "testUrl";
            this.testUrl.ReadOnly = true;
            this.testUrl.Size = new System.Drawing.Size(251, 20);
            this.testUrl.TabIndex = 3;
            this.testUrl.Text = "http://localhost:9080/";
            // 
            // lblTestUrl
            // 
            this.lblTestUrl.AutoSize = true;
            this.lblTestUrl.Enabled = false;
            this.lblTestUrl.Location = new System.Drawing.Point(9, 108);
            this.lblTestUrl.Name = "lblTestUrl";
            this.lblTestUrl.Size = new System.Drawing.Size(56, 13);
            this.lblTestUrl.TabIndex = 4;
            this.lblTestUrl.Text = "Test URL:";
            // 
            // webPort
            // 
            this.webPort.Enabled = false;
            this.webPort.Location = new System.Drawing.Point(146, 72);
            this.webPort.Name = "webPort";
            this.webPort.Size = new System.Drawing.Size(100, 20);
            this.webPort.TabIndex = 3;
            this.webPort.Text = "9080";
            this.webPort.TextChanged += new System.EventHandler(this.webPort_TextChanged);
            this.webPort.Validating += new System.ComponentModel.CancelEventHandler(this.webPort_Validating);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(9, 46);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(112, 13);
            this.label24.TabIndex = 2;
            this.label24.Text = "Status visible via web:";
            // 
            // lblWebPort
            // 
            this.lblWebPort.AutoSize = true;
            this.lblWebPort.Enabled = false;
            this.lblWebPort.Location = new System.Drawing.Point(9, 75);
            this.lblWebPort.Name = "lblWebPort";
            this.lblWebPort.Size = new System.Drawing.Size(86, 13);
            this.lblWebPort.TabIndex = 1;
            this.lblWebPort.Text = "Web server port:";
            // 
            // offerWebInterface
            // 
            this.offerWebInterface.AutoSize = true;
            this.offerWebInterface.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.offerWebInterface.Location = new System.Drawing.Point(146, 46);
            this.offerWebInterface.Name = "offerWebInterface";
            this.offerWebInterface.Size = new System.Drawing.Size(15, 14);
            this.offerWebInterface.TabIndex = 0;
            this.offerWebInterface.UseVisualStyleBackColor = true;
            this.offerWebInterface.CheckedChanged += new System.EventHandler(this.offerWebInterface_CheckedChanged);
            // 
            // tabDrives
            // 
            this.tabDrives.Controls.Add(this.listMapDrives);
            this.tabDrives.Controls.Add(this.btnAddDrive);
            this.tabDrives.Controls.Add(this.btnDelDrive);
            this.tabDrives.Controls.Add(this.lblMappedDrives);
            this.tabDrives.Location = new System.Drawing.Point(4, 22);
            this.tabDrives.Name = "tabDrives";
            this.tabDrives.Padding = new System.Windows.Forms.Padding(3);
            this.tabDrives.Size = new System.Drawing.Size(442, 219);
            this.tabDrives.TabIndex = 5;
            this.tabDrives.Text = "Network drives";
            this.tabDrives.UseVisualStyleBackColor = true;
            // 
            // listMapDrives
            // 
            this.listMapDrives.AllowUserToAddRows = false;
            this.listMapDrives.AllowUserToDeleteRows = false;
            this.listMapDrives.AllowUserToResizeRows = false;
            this.listMapDrives.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.listMapDrives.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.driveLetter,
            this.driveShare,
            this.driveUsername,
            this.drivePassword});
            this.listMapDrives.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.listMapDrives.Location = new System.Drawing.Point(6, 19);
            this.listMapDrives.Name = "listMapDrives";
            this.listMapDrives.RowHeadersVisible = false;
            this.listMapDrives.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.listMapDrives.Size = new System.Drawing.Size(430, 164);
            this.listMapDrives.TabIndex = 0;
            // 
            // driveLetter
            // 
            this.driveLetter.HeaderText = "Disk";
            this.driveLetter.Name = "driveLetter";
            this.driveLetter.Width = 60;
            // 
            // driveShare
            // 
            this.driveShare.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.driveShare.HeaderText = "Share name";
            this.driveShare.Name = "driveShare";
            // 
            // driveUsername
            // 
            this.driveUsername.HeaderText = "Username";
            this.driveUsername.Name = "driveUsername";
            // 
            // drivePassword
            // 
            this.drivePassword.HeaderText = "Password";
            this.drivePassword.Name = "drivePassword";
            // 
            // btnAddDrive
            // 
            this.btnAddDrive.Location = new System.Drawing.Point(280, 190);
            this.btnAddDrive.Name = "btnAddDrive";
            this.btnAddDrive.Size = new System.Drawing.Size(75, 23);
            this.btnAddDrive.TabIndex = 2;
            this.btnAddDrive.Text = "Add";
            this.btnAddDrive.UseVisualStyleBackColor = true;
            this.btnAddDrive.Click += new System.EventHandler(this.btnAddDrive_Click);
            // 
            // btnDelDrive
            // 
            this.btnDelDrive.Location = new System.Drawing.Point(361, 189);
            this.btnDelDrive.Name = "btnDelDrive";
            this.btnDelDrive.Size = new System.Drawing.Size(75, 23);
            this.btnDelDrive.TabIndex = 2;
            this.btnDelDrive.Text = "Delete";
            this.btnDelDrive.UseVisualStyleBackColor = true;
            this.btnDelDrive.Click += new System.EventHandler(this.btnDelDrive_Click);
            // 
            // lblMappedDrives
            // 
            this.lblMappedDrives.Location = new System.Drawing.Point(6, 3);
            this.lblMappedDrives.Name = "lblMappedDrives";
            this.lblMappedDrives.Size = new System.Drawing.Size(430, 180);
            this.lblMappedDrives.TabIndex = 3;
            this.lblMappedDrives.Text = "Defines the drives which amleto needs to map at startup.";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // SetupWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 346);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupWin";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences";
            this.Load += new System.EventHandler(this.SetupWin_Load);
            this.tabs.ResumeLayout(false);
            this.tabConfigs.ResumeLayout(false);
            this.tabConfigs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConfigList)).EndInit();
            this.tabLogging.ResumeLayout(false);
            this.tabLogging.PerformLayout();
            this.tabNetwork.ResumeLayout(false);
            this.tabNetwork.PerformLayout();
            this.tabEmail.ResumeLayout(false);
            this.tabEmail.PerformLayout();
            this.tabWeb.ResumeLayout(false);
            this.tabWeb.PerformLayout();
            this.tabDrives.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listMapDrives)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkAutoPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textPort;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textLogFile;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabLogging;
        private System.Windows.Forms.TabPage tabNetwork;
        private System.Windows.Forms.TabPage tabConfigs;
        private System.Windows.Forms.TabPage tabEmail;
        private System.Windows.Forms.TextBox textSMTPPassword;
        private System.Windows.Forms.TextBox textSTMPUser;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textSMTPServer;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textMailFrom;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TabPage tabWeb;
        private System.Windows.Forms.Label lblWebPort;
        private System.Windows.Forms.CheckBox offerWebInterface;
        private System.Windows.Forms.TextBox testUrl;
        private System.Windows.Forms.Label lblTestUrl;
        private System.Windows.Forms.TextBox webPort;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TabPage tabDrives;
        private System.Windows.Forms.Label lblMappedDrives;
        private System.Windows.Forms.DataGridView listMapDrives;
        private System.Windows.Forms.Button btnAddDrive;
        private System.Windows.Forms.Button btnDelDrive;
        private System.Windows.Forms.DataGridViewComboBoxColumn driveLetter;
        private System.Windows.Forms.DataGridViewTextBoxColumn driveShare;
        private System.Windows.Forms.DataGridViewTextBoxColumn driveUsername;
        private System.Windows.Forms.DataGridViewTextBoxColumn drivePassword;
        private System.Windows.Forms.CheckBox cbEnableLogging;
        private System.Windows.Forms.Button SelectLogFile;
        private System.Windows.Forms.Button SetDefault;
        private System.Windows.Forms.Button AddConfig;
        private System.Windows.Forms.Button DeleteConfig;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView ConfigList;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConfigDefault;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConfigName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConfigBits;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConfigDir;
    }
}