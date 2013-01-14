namespace AmletoClient
{
    partial class ClientSetupWin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientSetupWin));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.autoSearch = new System.Windows.Forms.CheckBox();
            this.lblName = new System.Windows.Forms.Label();
            this.serverAddress = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.serverPort = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.localScratch = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.renderPriority = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.logFile = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.nbThreads = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.memorySegment = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.SaveLog = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(362, 58);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(362, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "If the client still cannot connect after a manual setup, check your firewalls set" +
    "tings.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Automatic server search:";
            // 
            // autoSearch
            // 
            this.autoSearch.AutoSize = true;
            this.autoSearch.Checked = true;
            this.autoSearch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoSearch.Location = new System.Drawing.Point(145, 101);
            this.autoSearch.Name = "autoSearch";
            this.autoSearch.Size = new System.Drawing.Size(15, 14);
            this.autoSearch.TabIndex = 3;
            this.autoSearch.UseVisualStyleBackColor = true;
            this.autoSearch.CheckedChanged += new System.EventHandler(this.autoSearch_CheckedChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Enabled = false;
            this.lblName.Location = new System.Drawing.Point(15, 125);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(95, 13);
            this.lblName.TabIndex = 5;
            this.lblName.Text = "Server IP or name:";
            // 
            // serverAddress
            // 
            this.serverAddress.Enabled = false;
            this.serverAddress.Location = new System.Drawing.Point(145, 122);
            this.serverAddress.Name = "serverAddress";
            this.serverAddress.Size = new System.Drawing.Size(229, 20);
            this.serverAddress.TabIndex = 6;
            this.serverAddress.Text = "localhost";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Enabled = false;
            this.lblPort.Location = new System.Drawing.Point(15, 151);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(62, 13);
            this.lblPort.TabIndex = 7;
            this.lblPort.Text = "Server port:";
            // 
            // serverPort
            // 
            this.serverPort.Enabled = false;
            this.serverPort.Location = new System.Drawing.Point(145, 148);
            this.serverPort.Name = "serverPort";
            this.serverPort.Size = new System.Drawing.Size(51, 20);
            this.serverPort.TabIndex = 8;
            this.serverPort.Text = "2080";
            this.serverPort.Validating += new System.ComponentModel.CancelEventHandler(this.serverPort_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 235);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Local scratch directory:";
            // 
            // localScratch
            // 
            this.localScratch.Location = new System.Drawing.Point(145, 232);
            this.localScratch.Name = "localScratch";
            this.localScratch.Size = new System.Drawing.Size(229, 20);
            this.localScratch.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Green;
            this.label7.Location = new System.Drawing.Point(166, 101);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(198, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "(Uses UDP port 61111 for the discovery)";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(299, 353);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(218, 353);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 21;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 263);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Render priority:";
            // 
            // renderPriority
            // 
            this.renderPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.renderPriority.FormattingEnabled = true;
            this.renderPriority.Location = new System.Drawing.Point(145, 260);
            this.renderPriority.Name = "renderPriority";
            this.renderPriority.Size = new System.Drawing.Size(229, 21);
            this.renderPriority.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 201);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Log file:";
            // 
            // logFile
            // 
            this.logFile.Location = new System.Drawing.Point(145, 198);
            this.logFile.Name = "logFile";
            this.logFile.Size = new System.Drawing.Size(229, 20);
            this.logFile.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 290);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Render thread";
            // 
            // nbThreads
            // 
            this.nbThreads.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nbThreads.FormattingEnabled = true;
            this.nbThreads.Items.AddRange(new object[] {
            "Automatic",
            "1",
            "2",
            "4",
            "8",
            "16",
            "32",
            "64"});
            this.nbThreads.Location = new System.Drawing.Point(145, 290);
            this.nbThreads.Name = "nbThreads";
            this.nbThreads.Size = new System.Drawing.Size(101, 21);
            this.nbThreads.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 320);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(126, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Default memory segment:";
            // 
            // memorySegment
            // 
            this.memorySegment.Location = new System.Drawing.Point(145, 317);
            this.memorySegment.Name = "memorySegment";
            this.memorySegment.Size = new System.Drawing.Size(51, 20);
            this.memorySegment.TabIndex = 19;
            this.memorySegment.Text = "512";
            this.memorySegment.Validating += new System.ComponentModel.CancelEventHandler(this.memorySegment_Validating);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(202, 320);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "(MB)";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // SaveLog
            // 
            this.SaveLog.AutoSize = true;
            this.SaveLog.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SaveLog.Location = new System.Drawing.Point(145, 178);
            this.SaveLog.Name = "SaveLog";
            this.SaveLog.Size = new System.Drawing.Size(15, 14);
            this.SaveLog.TabIndex = 23;
            this.SaveLog.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Enabled = false;
            this.label8.Location = new System.Drawing.Point(15, 178);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Save Log File:";
            // 
            // ClientSetupWin
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(386, 385);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.SaveLog);
            this.Controls.Add(this.nbThreads);
            this.Controls.Add(this.renderPriority);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.logFile);
            this.Controls.Add(this.localScratch);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.memorySegment);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.serverPort);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.serverAddress);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.autoSearch);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClientSetupWin";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Amleto Client Setup";
            this.Load += new System.EventHandler(this.ClientSetupWin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox autoSearch;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox serverAddress;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox serverPort;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox localScratch;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox renderPriority;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox logFile;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox nbThreads;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox memorySegment;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox SaveLog;
    }
}