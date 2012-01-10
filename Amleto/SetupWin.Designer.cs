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
            this.label4 = new System.Windows.Forms.Label();
            this.textProgPath = new System.Windows.Forms.TextBox();
            this.btnSelectProg = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textConfigPath = new System.Windows.Forms.TextBox();
            this.btnSelectConfig = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textPluginPath = new System.Windows.Forms.TextBox();
            this.btnSelectPlugin = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.textLogFile = new System.Windows.Forms.TextBox();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.bits64 = new System.Windows.Forms.RadioButton();
            this.bits32 = new System.Windows.Forms.RadioButton();
            this.btnDeleteConfig = new System.Windows.Forms.Button();
            this.selectConfig = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
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
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.listMapDrives = new System.Windows.Forms.DataGridView();
            this.driveLetter = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.driveShare = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.driveUsername = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drivePassword = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddDrive = new System.Windows.Forms.Button();
            this.btnDelDrive = new System.Windows.Forms.Button();
            this.lblMappedDrives = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.SelectLogFile = new System.Windows.Forms.Button();
            this.cbEnableLogging = new System.Windows.Forms.CheckBox();
            this.tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabWeb.SuspendLayout();
            this.tabPage5.SuspendLayout();
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
            this.label2.Location = new System.Drawing.Point(6, 38);
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
            this.checkAutoPort.Location = new System.Drawing.Point(106, 38);
            this.checkAutoPort.Name = "checkAutoPort";
            this.checkAutoPort.Size = new System.Drawing.Size(15, 14);
            this.checkAutoPort.TabIndex = 2;
            this.checkAutoPort.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Port number:";
            // 
            // textPort
            // 
            this.textPort.Location = new System.Drawing.Point(106, 82);
            this.textPort.Name = "textPort";
            this.textPort.Size = new System.Drawing.Size(61, 20);
            this.textPort.TabIndex = 5;
            this.textPort.Text = "2080";
            this.textPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textPort.Validating += new System.ComponentModel.CancelEventHandler(this.textPort_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "LWSN Path:";
            // 
            // textProgPath
            // 
            this.textProgPath.Location = new System.Drawing.Point(106, 90);
            this.textProgPath.Name = "textProgPath";
            this.textProgPath.Size = new System.Drawing.Size(296, 20);
            this.textProgPath.TabIndex = 9;
            this.textProgPath.TextChanged += new System.EventHandler(this.textProgPath_TextChanged);
            // 
            // btnSelectProg
            // 
            this.btnSelectProg.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectProg.Image")));
            this.btnSelectProg.Location = new System.Drawing.Point(408, 88);
            this.btnSelectProg.Name = "btnSelectProg";
            this.btnSelectProg.Size = new System.Drawing.Size(31, 23);
            this.btnSelectProg.TabIndex = 10;
            this.btnSelectProg.UseVisualStyleBackColor = true;
            this.btnSelectProg.Click += new System.EventHandler(this.btnSelectProg_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Config. Path:";
            // 
            // textConfigPath
            // 
            this.textConfigPath.Location = new System.Drawing.Point(106, 49);
            this.textConfigPath.Name = "textConfigPath";
            this.textConfigPath.Size = new System.Drawing.Size(296, 20);
            this.textConfigPath.TabIndex = 5;
            this.textConfigPath.TextChanged += new System.EventHandler(this.textConfigPath_TextChanged);
            // 
            // btnSelectConfig
            // 
            this.btnSelectConfig.Image = global::Amleto.Properties.Resources.folder_explore;
            this.btnSelectConfig.Location = new System.Drawing.Point(408, 47);
            this.btnSelectConfig.Name = "btnSelectConfig";
            this.btnSelectConfig.Size = new System.Drawing.Size(31, 23);
            this.btnSelectConfig.TabIndex = 6;
            this.btnSelectConfig.UseVisualStyleBackColor = true;
            this.btnSelectConfig.Click += new System.EventHandler(this.btnSelectConfig_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Plugin Path:";
            // 
            // textPluginPath
            // 
            this.textPluginPath.Location = new System.Drawing.Point(106, 132);
            this.textPluginPath.Name = "textPluginPath";
            this.textPluginPath.Size = new System.Drawing.Size(296, 20);
            this.textPluginPath.TabIndex = 13;
            this.textPluginPath.TextChanged += new System.EventHandler(this.textPluginPath_TextChanged);
            // 
            // btnSelectPlugin
            // 
            this.btnSelectPlugin.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectPlugin.Image")));
            this.btnSelectPlugin.Location = new System.Drawing.Point(408, 130);
            this.btnSelectPlugin.Name = "btnSelectPlugin";
            this.btnSelectPlugin.Size = new System.Drawing.Size(31, 23);
            this.btnSelectPlugin.TabIndex = 14;
            this.btnSelectPlugin.UseVisualStyleBackColor = true;
            this.btnSelectPlugin.Click += new System.EventHandler(this.btnSelectPlugin_Click);
            // 
            // label7
            // 
            this.label7.ForeColor = System.Drawing.Color.Green;
            this.label7.Location = new System.Drawing.Point(6, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(401, 33);
            this.label7.TabIndex = 0;
            this.label7.Text = resources.GetString("label7.Text");
            // 
            // label8
            // 
            this.label8.ForeColor = System.Drawing.Color.Green;
            this.label8.Location = new System.Drawing.Point(6, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(427, 16);
            this.label8.TabIndex = 3;
            this.label8.Text = "Used in case the \"Find free port\" option is disabled.";
            // 
            // label9
            // 
            this.label9.ForeColor = System.Drawing.Color.Green;
            this.label9.Location = new System.Drawing.Point(8, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(427, 16);
            this.label9.TabIndex = 7;
            this.label9.Text = "Path where LWSN, modeler, and Lightwave.exe are stored.";
            // 
            // label10
            // 
            this.label10.ForeColor = System.Drawing.Color.Green;
            this.label10.Location = new System.Drawing.Point(8, 31);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(427, 16);
            this.label10.TabIndex = 3;
            this.label10.Text = "Path where the different CFG files are stored.";
            // 
            // label11
            // 
            this.label11.ForeColor = System.Drawing.Color.Green;
            this.label11.Location = new System.Drawing.Point(9, 114);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(427, 16);
            this.label11.TabIndex = 11;
            this.label11.Text = "Parent directory of all the plugins directory.";
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
            this.tabs.Controls.Add(this.tabPage1);
            this.tabs.Controls.Add(this.tabPage2);
            this.tabs.Controls.Add(this.tabPage3);
            this.tabs.Controls.Add(this.tabPage4);
            this.tabs.Controls.Add(this.tabWeb);
            this.tabs.Controls.Add(this.tabPage5);
            this.tabs.Location = new System.Drawing.Point(8, 60);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(450, 245);
            this.tabs.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cbEnableLogging);
            this.tabPage1.Controls.Add(this.SelectLogFile);
            this.tabPage1.Controls.Add(this.textLogFile);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(442, 219);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Logging";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textPort);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.checkAutoPort);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(442, 219);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Network";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.bits64);
            this.tabPage3.Controls.Add(this.bits32);
            this.tabPage3.Controls.Add(this.btnDeleteConfig);
            this.tabPage3.Controls.Add(this.selectConfig);
            this.tabPage3.Controls.Add(this.textPluginPath);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.textProgPath);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.label15);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.textConfigPath);
            this.tabPage3.Controls.Add(this.btnSelectPlugin);
            this.tabPage3.Controls.Add(this.label22);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.btnSelectConfig);
            this.tabPage3.Controls.Add(this.btnSelectProg);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(442, 219);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "LW Configs";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // bits64
            // 
            this.bits64.AutoSize = true;
            this.bits64.Location = new System.Drawing.Point(190, 171);
            this.bits64.Name = "bits64";
            this.bits64.Size = new System.Drawing.Size(52, 17);
            this.bits64.TabIndex = 16;
            this.bits64.Text = "64-Bit";
            this.bits64.UseVisualStyleBackColor = true;
            this.bits64.CheckedChanged += new System.EventHandler(this.bits64_CheckedChanged);
            // 
            // bits32
            // 
            this.bits32.AutoSize = true;
            this.bits32.Checked = true;
            this.bits32.Location = new System.Drawing.Point(109, 171);
            this.bits32.Name = "bits32";
            this.bits32.Size = new System.Drawing.Size(52, 17);
            this.bits32.TabIndex = 15;
            this.bits32.TabStop = true;
            this.bits32.Text = "32-Bit";
            this.bits32.UseVisualStyleBackColor = true;
            this.bits32.CheckedChanged += new System.EventHandler(this.bits32_CheckedChanged);
            // 
            // btnDeleteConfig
            // 
            this.btnDeleteConfig.Enabled = false;
            this.btnDeleteConfig.Image = global::Amleto.Properties.Resources.bin_closed;
            this.btnDeleteConfig.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteConfig.Location = new System.Drawing.Point(408, 7);
            this.btnDeleteConfig.Name = "btnDeleteConfig";
            this.btnDeleteConfig.Size = new System.Drawing.Size(31, 23);
            this.btnDeleteConfig.TabIndex = 2;
            this.btnDeleteConfig.UseVisualStyleBackColor = true;
            this.btnDeleteConfig.Click += new System.EventHandler(this.btnDeleteConfig_Click);
            // 
            // selectConfig
            // 
            this.selectConfig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectConfig.FormattingEnabled = true;
            this.selectConfig.Location = new System.Drawing.Point(106, 7);
            this.selectConfig.Name = "selectConfig";
            this.selectConfig.Size = new System.Drawing.Size(296, 21);
            this.selectConfig.TabIndex = 1;
            this.selectConfig.SelectedIndexChanged += new System.EventHandler(this.selectConfig_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 10);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(74, 13);
            this.label15.TabIndex = 0;
            this.label15.Text = "Config. Name:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(9, 175);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(67, 13);
            this.label22.TabIndex = 12;
            this.label22.Text = "LW Binaries:";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label21);
            this.tabPage4.Controls.Add(this.textSMTPPassword);
            this.tabPage4.Controls.Add(this.textSTMPUser);
            this.tabPage4.Controls.Add(this.label20);
            this.tabPage4.Controls.Add(this.textSMTPServer);
            this.tabPage4.Controls.Add(this.label19);
            this.tabPage4.Controls.Add(this.textMailFrom);
            this.tabPage4.Controls.Add(this.label18);
            this.tabPage4.Controls.Add(this.label17);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(442, 219);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Email Setup";
            this.tabPage4.UseVisualStyleBackColor = true;
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
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.listMapDrives);
            this.tabPage5.Controls.Add(this.btnAddDrive);
            this.tabPage5.Controls.Add(this.btnDelDrive);
            this.tabPage5.Controls.Add(this.lblMappedDrives);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(442, 219);
            this.tabPage5.TabIndex = 5;
            this.tabPage5.Text = "Network drives";
            this.tabPage5.UseVisualStyleBackColor = true;
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
            this.lblMappedDrives.Text = "Defines the drives which amleto need to map at startup.";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
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
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabWeb.ResumeLayout(false);
            this.tabWeb.PerformLayout();
            this.tabPage5.ResumeLayout(false);
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textProgPath;
        private System.Windows.Forms.Button btnSelectProg;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textConfigPath;
        private System.Windows.Forms.Button btnSelectConfig;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textPluginPath;
        private System.Windows.Forms.Button btnSelectPlugin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textLogFile;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ComboBox selectConfig;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnDeleteConfig;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox textSMTPPassword;
        private System.Windows.Forms.TextBox textSTMPUser;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textSMTPServer;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textMailFrom;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.RadioButton bits64;
        private System.Windows.Forms.RadioButton bits32;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TabPage tabWeb;
        private System.Windows.Forms.Label lblWebPort;
        private System.Windows.Forms.CheckBox offerWebInterface;
        private System.Windows.Forms.TextBox testUrl;
        private System.Windows.Forms.Label lblTestUrl;
        private System.Windows.Forms.TextBox webPort;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TabPage tabPage5;
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
    }
}