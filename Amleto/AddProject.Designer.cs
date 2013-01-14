namespace Amleto
{
    partial class AddProject
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddProject));
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textScene = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textContentDir = new System.Windows.Forms.TextBox();
            this.textOutputDir = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.renderSetupTabs = new System.Windows.Forms.TabControl();
            this.sceneTab = new System.Windows.Forms.TabPage();
            this.OverwriteFrames = new System.Windows.Forms.CheckBox();
            this.OverrideSettings = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.listOuputFormat = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.listImageFormat = new System.Windows.Forms.ComboBox();
            this.textPrefix = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label25 = new System.Windows.Forms.Label();
            this.AlphaPrefix = new System.Windows.Forms.TextBox();
            this.SaveAlpha = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.AlphaImageFormat = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.btnSelectOuputDir = new System.Windows.Forms.Button();
            this.listConfig = new System.Windows.Forms.ComboBox();
            this.btnSelectContentDir = new System.Windows.Forms.Button();
            this.btnSelectScene = new System.Windows.Forms.Button();
            this.framesTab = new System.Windows.Forms.TabPage();
            this.deleteSplitFrames = new System.Windows.Forms.CheckBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.slicesAcross = new System.Windows.Forms.TextBox();
            this.textStartFrame = new System.Windows.Forms.TextBox();
            this.textEndFrame = new System.Windows.Forms.TextBox();
            this.textFrameStep = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.renderBlock = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lblNbSlices = new System.Windows.Forms.Label();
            this.lblSplitExplain = new System.Windows.Forms.Label();
            this.slicesDown = new System.Windows.Forms.TextBox();
            this.slicesOverlap = new System.Windows.Forms.TextBox();
            this.lblSlicesOverlap = new System.Windows.Forms.Label();
            this.masterPluginTab = new System.Windows.Forms.TabPage();
            this.masterList = new System.Windows.Forms.CheckedListBox();
            this.label23 = new System.Windows.Forms.Label();
            this.noticeTab = new System.Windows.Forms.TabPage();
            this.emailContainLog = new System.Windows.Forms.CheckBox();
            this.emailNotify = new System.Windows.Forms.CheckBox();
            this.emailSubjectNotOk = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.emailSubjectOk = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.emailTo = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.qualityTab = new System.Windows.Forms.TabPage();
            this.recusionLimit = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.traceOcclusion = new System.Windows.Forms.CheckBox();
            this.traceTransp = new System.Windows.Forms.CheckBox();
            this.traceRefraction = new System.Windows.Forms.CheckBox();
            this.renderLine = new System.Windows.Forms.CheckBox();
            this.traceReflection = new System.Windows.Forms.CheckBox();
            this.traceShadow = new System.Windows.Forms.CheckBox();
            this.renderMode = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cameraTab = new System.Windows.Forms.TabPage();
            this.imageAspect = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.imageHeight = new System.Windows.Forms.TextBox();
            this.imageWidth = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.adaptiveThreshold = new System.Windows.Forms.TextBox();
            this.lblAdaptiveThreshold = new System.Windows.Forms.Label();
            this.adaptiveSampling = new System.Windows.Forms.CheckBox();
            this.softFilter = new System.Windows.Forms.CheckBox();
            this.lblReconFilter = new System.Windows.Forms.Label();
            this.lblAntialias = new System.Windows.Forms.Label();
            this.reconFilter = new System.Windows.Forms.ComboBox();
            this.classicAntialias = new System.Windows.Forms.ComboBox();
            this.samplingPattern = new System.Windows.Forms.ComboBox();
            this.lblSamplingPattern = new System.Windows.Forms.Label();
            this.cameraAntialias = new System.Windows.Forms.NumericUpDown();
            this.lblCameraAntialias = new System.Windows.Forms.Label();
            this.cameraType = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cameraName = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.giTab = new System.Windows.Forms.TabPage();
            this.indirectGI = new System.Windows.Forms.NumericUpDown();
            this.rayGI = new System.Windows.Forms.NumericUpDown();
            this.lblIndirectGI = new System.Windows.Forms.Label();
            this.minPixelGI = new System.Windows.Forms.TextBox();
            this.minEvalGI = new System.Windows.Forms.TextBox();
            this.toleranceGI = new System.Windows.Forms.TextBox();
            this.intensityGI = new System.Windows.Forms.TextBox();
            this.lblRayGI = new System.Windows.Forms.Label();
            this.lblMinPixelGI = new System.Windows.Forms.Label();
            this.lblMinEvalGI = new System.Windows.Forms.Label();
            this.lblToleranceGI = new System.Windows.Forms.Label();
            this.lblIntensityGI = new System.Windows.Forms.Label();
            this.directionalGI = new System.Windows.Forms.CheckBox();
            this.volumetricGI = new System.Windows.Forms.CheckBox();
            this.cachedGI = new System.Windows.Forms.CheckBox();
            this.useAmbientGI = new System.Windows.Forms.CheckBox();
            this.backdropTranspGI = new System.Windows.Forms.CheckBox();
            this.interpolatedGI = new System.Windows.Forms.CheckBox();
            this.radiosityType = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.logTab = new System.Windows.Forms.TabPage();
            this.textViewLog = new System.Windows.Forms.TextBox();
            this.checkValues = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.btnSaveProject = new System.Windows.Forms.Button();
            this.btnLoadProject = new System.Windows.Forms.Button();
            this.renderSetupTabs.SuspendLayout();
            this.sceneTab.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.framesTab.SuspendLayout();
            this.masterPluginTab.SuspendLayout();
            this.noticeTab.SuspendLayout();
            this.qualityTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recusionLimit)).BeginInit();
            this.cameraTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cameraAntialias)).BeginInit();
            this.giTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.indirectGI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rayGI)).BeginInit();
            this.logTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkValues)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(305, 368);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(386, 368);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Scene file:";
            // 
            // textScene
            // 
            this.textScene.Location = new System.Drawing.Point(97, 10);
            this.textScene.Name = "textScene";
            this.textScene.Size = new System.Drawing.Size(295, 20);
            this.textScene.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Content directory:";
            // 
            // textContentDir
            // 
            this.textContentDir.Location = new System.Drawing.Point(97, 36);
            this.textContentDir.Name = "textContentDir";
            this.textContentDir.Size = new System.Drawing.Size(295, 20);
            this.textContentDir.TabIndex = 4;
            // 
            // textOutputDir
            // 
            this.textOutputDir.Location = new System.Drawing.Point(97, 62);
            this.textOutputDir.Name = "textOutputDir";
            this.textOutputDir.Size = new System.Drawing.Size(295, 20);
            this.textOutputDir.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Ouput directory:";
            // 
            // renderSetupTabs
            // 
            this.renderSetupTabs.Controls.Add(this.sceneTab);
            this.renderSetupTabs.Controls.Add(this.framesTab);
            this.renderSetupTabs.Controls.Add(this.masterPluginTab);
            this.renderSetupTabs.Controls.Add(this.noticeTab);
            this.renderSetupTabs.Controls.Add(this.qualityTab);
            this.renderSetupTabs.Controls.Add(this.cameraTab);
            this.renderSetupTabs.Controls.Add(this.giTab);
            this.renderSetupTabs.Controls.Add(this.logTab);
            this.renderSetupTabs.Location = new System.Drawing.Point(12, 12);
            this.renderSetupTabs.Name = "renderSetupTabs";
            this.renderSetupTabs.SelectedIndex = 0;
            this.renderSetupTabs.Size = new System.Drawing.Size(445, 350);
            this.renderSetupTabs.TabIndex = 0;
            // 
            // sceneTab
            // 
            this.sceneTab.Controls.Add(this.OverwriteFrames);
            this.sceneTab.Controls.Add(this.OverrideSettings);
            this.sceneTab.Controls.Add(this.label9);
            this.sceneTab.Controls.Add(this.listOuputFormat);
            this.sceneTab.Controls.Add(this.groupBox2);
            this.sceneTab.Controls.Add(this.groupBox1);
            this.sceneTab.Controls.Add(this.textOutputDir);
            this.sceneTab.Controls.Add(this.label21);
            this.sceneTab.Controls.Add(this.label3);
            this.sceneTab.Controls.Add(this.label1);
            this.sceneTab.Controls.Add(this.label2);
            this.sceneTab.Controls.Add(this.textScene);
            this.sceneTab.Controls.Add(this.btnSelectOuputDir);
            this.sceneTab.Controls.Add(this.listConfig);
            this.sceneTab.Controls.Add(this.textContentDir);
            this.sceneTab.Controls.Add(this.btnSelectContentDir);
            this.sceneTab.Controls.Add(this.btnSelectScene);
            this.sceneTab.Location = new System.Drawing.Point(4, 22);
            this.sceneTab.Name = "sceneTab";
            this.sceneTab.Padding = new System.Windows.Forms.Padding(3);
            this.sceneTab.Size = new System.Drawing.Size(437, 324);
            this.sceneTab.TabIndex = 3;
            this.sceneTab.Text = "Scene setup";
            this.sceneTab.UseVisualStyleBackColor = true;
            // 
            // OverwriteFrames
            // 
            this.OverwriteFrames.AutoSize = true;
            this.OverwriteFrames.Location = new System.Drawing.Point(97, 142);
            this.OverwriteFrames.Name = "OverwriteFrames";
            this.OverwriteFrames.Size = new System.Drawing.Size(143, 17);
            this.OverwriteFrames.TabIndex = 23;
            this.OverwriteFrames.Text = "Overwrite existing frames";
            this.toolTips.SetToolTip(this.OverwriteFrames, "If checked any frames that already exist in the output path are going to be overw" +
                    "ritten");
            this.OverwriteFrames.UseVisualStyleBackColor = true;
            // 
            // OverrideSettings
            // 
            this.OverrideSettings.AutoSize = true;
            this.OverrideSettings.Location = new System.Drawing.Point(259, 142);
            this.OverrideSettings.Name = "OverrideSettings";
            this.OverrideSettings.Size = new System.Drawing.Size(172, 17);
            this.OverrideSettings.TabIndex = 26;
            this.OverrideSettings.Text = "Override LWS Render Settings";
            this.toolTips.SetToolTip(this.OverrideSettings, "Allows you to alter the render settings in the LWS file before rendering");
            this.OverrideSettings.UseVisualStyleBackColor = true;
            this.OverrideSettings.CheckedChanged += new System.EventHandler(this.OverrideSettings_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 118);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "Output format:";
            // 
            // listOuputFormat
            // 
            this.listOuputFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listOuputFormat.FormattingEnabled = true;
            this.listOuputFormat.Location = new System.Drawing.Point(97, 115);
            this.listOuputFormat.Name = "listOuputFormat";
            this.listOuputFormat.Size = new System.Drawing.Size(295, 21);
            this.listOuputFormat.TabIndex = 25;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.listImageFormat);
            this.groupBox2.Controls.Add(this.textPrefix);
            this.groupBox2.Location = new System.Drawing.Point(5, 160);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(425, 74);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "RGB";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "File prefix:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Image format:";
            // 
            // listImageFormat
            // 
            this.listImageFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listImageFormat.FormattingEnabled = true;
            this.listImageFormat.Location = new System.Drawing.Point(107, 42);
            this.listImageFormat.Name = "listImageFormat";
            this.listImageFormat.Size = new System.Drawing.Size(305, 21);
            this.listImageFormat.TabIndex = 22;
            // 
            // textPrefix
            // 
            this.textPrefix.Location = new System.Drawing.Point(107, 19);
            this.textPrefix.Name = "textPrefix";
            this.textPrefix.Size = new System.Drawing.Size(76, 20);
            this.textPrefix.TabIndex = 19;
            this.textPrefix.Text = "img_";
            this.textPrefix.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTips.SetToolTip(this.textPrefix, "The image name prefix");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.AlphaPrefix);
            this.groupBox1.Controls.Add(this.SaveAlpha);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.AlphaImageFormat);
            this.groupBox1.Location = new System.Drawing.Point(5, 240);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(428, 78);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Alpha";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(13, 22);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(54, 13);
            this.label25.TabIndex = 27;
            this.label25.Text = "File prefix:";
            // 
            // AlphaPrefix
            // 
            this.AlphaPrefix.Location = new System.Drawing.Point(107, 19);
            this.AlphaPrefix.Name = "AlphaPrefix";
            this.AlphaPrefix.Size = new System.Drawing.Size(76, 20);
            this.AlphaPrefix.TabIndex = 28;
            this.AlphaPrefix.Text = "img_";
            this.AlphaPrefix.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTips.SetToolTip(this.AlphaPrefix, "The image name prefix");
            // 
            // SaveAlpha
            // 
            this.SaveAlpha.AutoSize = true;
            this.SaveAlpha.Location = new System.Drawing.Point(249, 21);
            this.SaveAlpha.Name = "SaveAlpha";
            this.SaveAlpha.Size = new System.Drawing.Size(123, 17);
            this.SaveAlpha.TabIndex = 26;
            this.SaveAlpha.Text = "Save Alpha Channel";
            this.SaveAlpha.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(13, 48);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 13);
            this.label15.TabIndex = 24;
            this.label15.Text = "Image format:";
            // 
            // AlphaImageFormat
            // 
            this.AlphaImageFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AlphaImageFormat.FormattingEnabled = true;
            this.AlphaImageFormat.Location = new System.Drawing.Point(107, 45);
            this.AlphaImageFormat.Name = "AlphaImageFormat";
            this.AlphaImageFormat.Size = new System.Drawing.Size(305, 21);
            this.AlphaImageFormat.TabIndex = 25;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(3, 91);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(72, 13);
            this.label21.TabIndex = 9;
            this.label21.Text = "Configuration:";
            // 
            // btnSelectOuputDir
            // 
            this.btnSelectOuputDir.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectOuputDir.Image")));
            this.btnSelectOuputDir.Location = new System.Drawing.Point(398, 61);
            this.btnSelectOuputDir.Name = "btnSelectOuputDir";
            this.btnSelectOuputDir.Size = new System.Drawing.Size(33, 21);
            this.btnSelectOuputDir.TabIndex = 8;
            this.btnSelectOuputDir.UseVisualStyleBackColor = true;
            this.btnSelectOuputDir.Click += new System.EventHandler(this.btnSelectOuputDir_Click);
            // 
            // listConfig
            // 
            this.listConfig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listConfig.FormattingEnabled = true;
            this.listConfig.Location = new System.Drawing.Point(97, 88);
            this.listConfig.Name = "listConfig";
            this.listConfig.Size = new System.Drawing.Size(295, 21);
            this.listConfig.TabIndex = 10;
            this.listConfig.SelectedIndexChanged += new System.EventHandler(this.listConfig_SelectedIndexChanged);
            // 
            // btnSelectContentDir
            // 
            this.btnSelectContentDir.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectContentDir.Image")));
            this.btnSelectContentDir.Location = new System.Drawing.Point(398, 35);
            this.btnSelectContentDir.Name = "btnSelectContentDir";
            this.btnSelectContentDir.Size = new System.Drawing.Size(33, 21);
            this.btnSelectContentDir.TabIndex = 5;
            this.btnSelectContentDir.UseVisualStyleBackColor = true;
            this.btnSelectContentDir.Click += new System.EventHandler(this.btnSelectContentDir_Click);
            // 
            // btnSelectScene
            // 
            this.btnSelectScene.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectScene.Image")));
            this.btnSelectScene.Location = new System.Drawing.Point(399, 9);
            this.btnSelectScene.Name = "btnSelectScene";
            this.btnSelectScene.Size = new System.Drawing.Size(33, 20);
            this.btnSelectScene.TabIndex = 2;
            this.btnSelectScene.UseVisualStyleBackColor = true;
            this.btnSelectScene.Click += new System.EventHandler(this.btnSelectScene_Click);
            // 
            // framesTab
            // 
            this.framesTab.Controls.Add(this.deleteSplitFrames);
            this.framesTab.Controls.Add(this.label27);
            this.framesTab.Controls.Add(this.label26);
            this.framesTab.Controls.Add(this.slicesAcross);
            this.framesTab.Controls.Add(this.textStartFrame);
            this.framesTab.Controls.Add(this.textEndFrame);
            this.framesTab.Controls.Add(this.textFrameStep);
            this.framesTab.Controls.Add(this.label6);
            this.framesTab.Controls.Add(this.renderBlock);
            this.framesTab.Controls.Add(this.label4);
            this.framesTab.Controls.Add(this.label5);
            this.framesTab.Controls.Add(this.label22);
            this.framesTab.Controls.Add(this.lblNbSlices);
            this.framesTab.Controls.Add(this.lblSplitExplain);
            this.framesTab.Controls.Add(this.slicesDown);
            this.framesTab.Controls.Add(this.slicesOverlap);
            this.framesTab.Controls.Add(this.lblSlicesOverlap);
            this.framesTab.Location = new System.Drawing.Point(4, 22);
            this.framesTab.Name = "framesTab";
            this.framesTab.Padding = new System.Windows.Forms.Padding(3);
            this.framesTab.Size = new System.Drawing.Size(437, 324);
            this.framesTab.TabIndex = 0;
            this.framesTab.Text = "Frames";
            this.framesTab.UseVisualStyleBackColor = true;
            // 
            // deleteSplitFrames
            // 
            this.deleteSplitFrames.AutoSize = true;
            this.deleteSplitFrames.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.deleteSplitFrames.Location = new System.Drawing.Point(235, 219);
            this.deleteSplitFrames.Name = "deleteSplitFrames";
            this.deleteSplitFrames.Size = new System.Drawing.Size(117, 17);
            this.deleteSplitFrames.TabIndex = 30;
            this.deleteSplitFrames.Text = "Delete Split Images";
            this.toolTips.SetToolTip(this.deleteSplitFrames, "This will remove the split image files once they have been joined into final imag" +
                    "e");
            this.deleteSplitFrames.UseVisualStyleBackColor = true;
            // 
            // label27
            // 
            this.label27.Location = new System.Drawing.Point(27, 146);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(392, 32);
            this.label27.TabIndex = 29;
            this.label27.Text = "If using the split rendering option, make sure that your image format is PNG, BMP" +
                " or JPG otherwise combining image segments at the end of rendering will fail.";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(60, 193);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(70, 13);
            this.label26.TabIndex = 28;
            this.label26.Text = "Slices Across";
            // 
            // slicesAcross
            // 
            this.slicesAcross.Location = new System.Drawing.Point(136, 190);
            this.slicesAcross.Name = "slicesAcross";
            this.slicesAcross.Size = new System.Drawing.Size(50, 20);
            this.slicesAcross.TabIndex = 4;
            this.slicesAcross.Text = "1";
            this.slicesAcross.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTips.SetToolTip(this.slicesAcross, "Number of slices used to\r\nrender this image.\r\nThis is used to render a\r\nsingle im" +
                    "age on multiple\r\nnodes.");
            // 
            // textStartFrame
            // 
            this.textStartFrame.Location = new System.Drawing.Point(136, 30);
            this.textStartFrame.Name = "textStartFrame";
            this.textStartFrame.Size = new System.Drawing.Size(50, 20);
            this.textStartFrame.TabIndex = 0;
            this.textStartFrame.Text = "0";
            this.textStartFrame.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTips.SetToolTip(this.textStartFrame, "Define the start frame of the animation.");
            this.textStartFrame.Validating += new System.ComponentModel.CancelEventHandler(this.textStartFrame_Validating);
            // 
            // textEndFrame
            // 
            this.textEndFrame.Location = new System.Drawing.Point(302, 30);
            this.textEndFrame.Name = "textEndFrame";
            this.textEndFrame.Size = new System.Drawing.Size(50, 20);
            this.textEndFrame.TabIndex = 1;
            this.textEndFrame.Text = "60";
            this.textEndFrame.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTips.SetToolTip(this.textEndFrame, "Defines the end frame of the animation.");
            this.textEndFrame.Validating += new System.ComponentModel.CancelEventHandler(this.textEndFrame_Validating);
            // 
            // textFrameStep
            // 
            this.textFrameStep.Location = new System.Drawing.Point(136, 60);
            this.textFrameStep.Name = "textFrameStep";
            this.textFrameStep.Size = new System.Drawing.Size(50, 20);
            this.textFrameStep.TabIndex = 2;
            this.textFrameStep.Text = "1";
            this.textFrameStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTips.SetToolTip(this.textFrameStep, "Define how many frames need to be\r\nskiped bettween one frame an\r\nthe other (1 mea" +
                    "ns each frames\r\nwill be rendered).");
            this.textFrameStep.Validating += new System.ComponentModel.CancelEventHandler(this.positiveNumber_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(96, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Steps";
            // 
            // renderBlock
            // 
            this.renderBlock.Location = new System.Drawing.Point(302, 60);
            this.renderBlock.Name = "renderBlock";
            this.renderBlock.Size = new System.Drawing.Size(50, 20);
            this.renderBlock.TabIndex = 3;
            this.renderBlock.Text = "5";
            this.renderBlock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTips.SetToolTip(this.renderBlock, "Defines how many frames\r\nwill be rendered before\r\nbeeing sent back to the\r\nserver" +
                    ". A bigger number\r\nincrease performance but\r\ndecrease the distribution\r\ncapabili" +
                    "ties.");
            this.renderBlock.Validating += new System.ComponentModel.CancelEventHandler(this.positiveNumber_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(101, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Start";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(270, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "End";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(225, 63);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(71, 13);
            this.label22.TabIndex = 26;
            this.label22.Text = "Render block";
            // 
            // lblNbSlices
            // 
            this.lblNbSlices.AutoSize = true;
            this.lblNbSlices.Location = new System.Drawing.Point(64, 219);
            this.lblNbSlices.Name = "lblNbSlices";
            this.lblNbSlices.Size = new System.Drawing.Size(66, 13);
            this.lblNbSlices.TabIndex = 15;
            this.lblNbSlices.Text = "Slices Down";
            // 
            // lblSplitExplain
            // 
            this.lblSplitExplain.AutoSize = true;
            this.lblSplitExplain.Location = new System.Drawing.Point(25, 128);
            this.lblSplitExplain.Name = "lblSplitExplain";
            this.lblSplitExplain.Size = new System.Drawing.Size(224, 13);
            this.lblSplitExplain.TabIndex = 14;
            this.lblSplitExplain.Text = "Split rendering (one frame over multiple nodes)";
            // 
            // slicesDown
            // 
            this.slicesDown.Location = new System.Drawing.Point(136, 216);
            this.slicesDown.Name = "slicesDown";
            this.slicesDown.Size = new System.Drawing.Size(50, 20);
            this.slicesDown.TabIndex = 5;
            this.slicesDown.Text = "1";
            this.slicesDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTips.SetToolTip(this.slicesDown, "Number of slices used to\r\nrender this image.\r\nThis is used to render a\r\nsingle im" +
                    "age on multiple\r\nnodes.");
            this.slicesDown.Validating += new System.ComponentModel.CancelEventHandler(this.positiveNumber_Validating);
            // 
            // slicesOverlap
            // 
            this.slicesOverlap.Location = new System.Drawing.Point(302, 190);
            this.slicesOverlap.Name = "slicesOverlap";
            this.slicesOverlap.Size = new System.Drawing.Size(50, 20);
            this.slicesOverlap.TabIndex = 6;
            this.slicesOverlap.Text = "5";
            this.slicesOverlap.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTips.SetToolTip(this.slicesOverlap, "Defines in percent how much\r\noverlap there is between\r\nthe different slices.");
            this.slicesOverlap.Validating += new System.ComponentModel.CancelEventHandler(this.positiveZeroDouble_Validating);
            // 
            // lblSlicesOverlap
            // 
            this.lblSlicesOverlap.AutoSize = true;
            this.lblSlicesOverlap.Location = new System.Drawing.Point(235, 193);
            this.lblSlicesOverlap.Name = "lblSlicesOverlap";
            this.lblSlicesOverlap.Size = new System.Drawing.Size(61, 13);
            this.lblSlicesOverlap.TabIndex = 17;
            this.lblSlicesOverlap.Text = "Overlap (%)";
            // 
            // masterPluginTab
            // 
            this.masterPluginTab.Controls.Add(this.masterList);
            this.masterPluginTab.Controls.Add(this.label23);
            this.masterPluginTab.Location = new System.Drawing.Point(4, 22);
            this.masterPluginTab.Name = "masterPluginTab";
            this.masterPluginTab.Size = new System.Drawing.Size(437, 324);
            this.masterPluginTab.TabIndex = 4;
            this.masterPluginTab.Text = "Master plugins";
            this.masterPluginTab.UseVisualStyleBackColor = true;
            // 
            // masterList
            // 
            this.masterList.CheckOnClick = true;
            this.masterList.FormattingEnabled = true;
            this.masterList.Location = new System.Drawing.Point(16, 26);
            this.masterList.Name = "masterList";
            this.masterList.Size = new System.Drawing.Size(409, 289);
            this.masterList.TabIndex = 1;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(13, 10);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(230, 13);
            this.label23.TabIndex = 0;
            this.label23.Text = "Checked plugins will be removed during render.";
            // 
            // noticeTab
            // 
            this.noticeTab.Controls.Add(this.emailContainLog);
            this.noticeTab.Controls.Add(this.emailNotify);
            this.noticeTab.Controls.Add(this.emailSubjectNotOk);
            this.noticeTab.Controls.Add(this.label19);
            this.noticeTab.Controls.Add(this.emailSubjectOk);
            this.noticeTab.Controls.Add(this.label18);
            this.noticeTab.Controls.Add(this.emailTo);
            this.noticeTab.Controls.Add(this.label17);
            this.noticeTab.Location = new System.Drawing.Point(4, 22);
            this.noticeTab.Name = "noticeTab";
            this.noticeTab.Padding = new System.Windows.Forms.Padding(3);
            this.noticeTab.Size = new System.Drawing.Size(437, 324);
            this.noticeTab.TabIndex = 2;
            this.noticeTab.Text = "Notification";
            this.noticeTab.UseVisualStyleBackColor = true;
            // 
            // emailContainLog
            // 
            this.emailContainLog.AutoSize = true;
            this.emailContainLog.Checked = true;
            this.emailContainLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.emailContainLog.Location = new System.Drawing.Point(9, 118);
            this.emailContainLog.Name = "emailContainLog";
            this.emailContainLog.Size = new System.Drawing.Size(114, 17);
            this.emailContainLog.TabIndex = 9;
            this.emailContainLog.Text = "Include activity log";
            this.emailContainLog.UseVisualStyleBackColor = true;
            // 
            // emailNotify
            // 
            this.emailNotify.AutoSize = true;
            this.emailNotify.Location = new System.Drawing.Point(9, 6);
            this.emailNotify.Name = "emailNotify";
            this.emailNotify.Size = new System.Drawing.Size(132, 17);
            this.emailNotify.TabIndex = 0;
            this.emailNotify.Text = "Send email notification";
            this.emailNotify.UseVisualStyleBackColor = true;
            // 
            // emailSubjectNotOk
            // 
            this.emailSubjectNotOk.Location = new System.Drawing.Point(139, 81);
            this.emailSubjectNotOk.Name = "emailSubjectNotOk";
            this.emailSubjectNotOk.Size = new System.Drawing.Size(292, 20);
            this.emailSubjectNotOk.TabIndex = 8;
            this.emailSubjectNotOk.Text = "Amleto Job not completed";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 84);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(103, 13);
            this.label19.TabIndex = 7;
            this.label19.Text = "Subject when failed:";
            // 
            // emailSubjectOk
            // 
            this.emailSubjectOk.Location = new System.Drawing.Point(139, 55);
            this.emailSubjectOk.Name = "emailSubjectOk";
            this.emailSubjectOk.Size = new System.Drawing.Size(292, 20);
            this.emailSubjectOk.TabIndex = 6;
            this.emailSubjectOk.Text = "Amleto Job completed";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 58);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(127, 13);
            this.label18.TabIndex = 5;
            this.label18.Text = "Subject when completed:";
            // 
            // emailTo
            // 
            this.emailTo.Location = new System.Drawing.Point(139, 29);
            this.emailTo.Name = "emailTo";
            this.emailTo.Size = new System.Drawing.Size(292, 20);
            this.emailTo.TabIndex = 4;
            this.emailTo.Text = "your@email.com";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 32);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(90, 13);
            this.label17.TabIndex = 3;
            this.label17.Text = "To email address:";
            // 
            // qualityTab
            // 
            this.qualityTab.Controls.Add(this.recusionLimit);
            this.qualityTab.Controls.Add(this.label11);
            this.qualityTab.Controls.Add(this.traceOcclusion);
            this.qualityTab.Controls.Add(this.traceTransp);
            this.qualityTab.Controls.Add(this.traceRefraction);
            this.qualityTab.Controls.Add(this.renderLine);
            this.qualityTab.Controls.Add(this.traceReflection);
            this.qualityTab.Controls.Add(this.traceShadow);
            this.qualityTab.Controls.Add(this.renderMode);
            this.qualityTab.Controls.Add(this.label10);
            this.qualityTab.Location = new System.Drawing.Point(4, 22);
            this.qualityTab.Name = "qualityTab";
            this.qualityTab.Padding = new System.Windows.Forms.Padding(3);
            this.qualityTab.Size = new System.Drawing.Size(437, 324);
            this.qualityTab.TabIndex = 1;
            this.qualityTab.Text = "Quality";
            this.qualityTab.UseVisualStyleBackColor = true;
            // 
            // recusionLimit
            // 
            this.recusionLimit.Location = new System.Drawing.Point(368, 106);
            this.recusionLimit.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.recusionLimit.Name = "recusionLimit";
            this.recusionLimit.Size = new System.Drawing.Size(52, 20);
            this.recusionLimit.TabIndex = 9;
            this.recusionLimit.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(252, 108);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(104, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "Ray Recursion Limit:";
            // 
            // traceOcclusion
            // 
            this.traceOcclusion.AutoSize = true;
            this.traceOcclusion.Location = new System.Drawing.Point(18, 107);
            this.traceOcclusion.Name = "traceOcclusion";
            this.traceOcclusion.Size = new System.Drawing.Size(126, 17);
            this.traceOcclusion.TabIndex = 7;
            this.traceOcclusion.Text = "Ray Trace Occlusion";
            this.traceOcclusion.UseVisualStyleBackColor = true;
            // 
            // traceTransp
            // 
            this.traceTransp.AutoSize = true;
            this.traceTransp.Location = new System.Drawing.Point(18, 84);
            this.traceTransp.Name = "traceTransp";
            this.traceTransp.Size = new System.Drawing.Size(144, 17);
            this.traceTransp.TabIndex = 5;
            this.traceTransp.Text = "Ray Trace Transparency";
            this.traceTransp.UseVisualStyleBackColor = true;
            // 
            // traceRefraction
            // 
            this.traceRefraction.AutoSize = true;
            this.traceRefraction.Location = new System.Drawing.Point(255, 84);
            this.traceRefraction.Name = "traceRefraction";
            this.traceRefraction.Size = new System.Drawing.Size(128, 17);
            this.traceRefraction.TabIndex = 6;
            this.traceRefraction.Text = "Ray Trace Refraction";
            this.traceRefraction.UseVisualStyleBackColor = true;
            // 
            // renderLine
            // 
            this.renderLine.AutoSize = true;
            this.renderLine.Location = new System.Drawing.Point(255, 38);
            this.renderLine.Name = "renderLine";
            this.renderLine.Size = new System.Drawing.Size(89, 17);
            this.renderLine.TabIndex = 2;
            this.renderLine.Text = "Render Lines";
            this.renderLine.UseVisualStyleBackColor = true;
            // 
            // traceReflection
            // 
            this.traceReflection.AutoSize = true;
            this.traceReflection.Location = new System.Drawing.Point(255, 61);
            this.traceReflection.Name = "traceReflection";
            this.traceReflection.Size = new System.Drawing.Size(127, 17);
            this.traceReflection.TabIndex = 4;
            this.traceReflection.Text = "Ray Trace Reflection";
            this.traceReflection.UseVisualStyleBackColor = true;
            // 
            // traceShadow
            // 
            this.traceShadow.AutoSize = true;
            this.traceShadow.Location = new System.Drawing.Point(18, 61);
            this.traceShadow.Name = "traceShadow";
            this.traceShadow.Size = new System.Drawing.Size(123, 17);
            this.traceShadow.TabIndex = 3;
            this.traceShadow.Text = "Ray Trace Shadows";
            this.traceShadow.UseVisualStyleBackColor = true;
            // 
            // renderMode
            // 
            this.renderMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.renderMode.FormattingEnabled = true;
            this.renderMode.Items.AddRange(new object[] {
            "Wireframe",
            "Quickshade",
            "Realistic"});
            this.renderMode.Location = new System.Drawing.Point(97, 31);
            this.renderMode.Name = "renderMode";
            this.renderMode.Size = new System.Drawing.Size(121, 21);
            this.renderMode.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 34);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Render Mode";
            // 
            // cameraTab
            // 
            this.cameraTab.Controls.Add(this.imageAspect);
            this.cameraTab.Controls.Add(this.label14);
            this.cameraTab.Controls.Add(this.imageHeight);
            this.cameraTab.Controls.Add(this.imageWidth);
            this.cameraTab.Controls.Add(this.label13);
            this.cameraTab.Controls.Add(this.label12);
            this.cameraTab.Controls.Add(this.adaptiveThreshold);
            this.cameraTab.Controls.Add(this.lblAdaptiveThreshold);
            this.cameraTab.Controls.Add(this.adaptiveSampling);
            this.cameraTab.Controls.Add(this.softFilter);
            this.cameraTab.Controls.Add(this.lblReconFilter);
            this.cameraTab.Controls.Add(this.lblAntialias);
            this.cameraTab.Controls.Add(this.reconFilter);
            this.cameraTab.Controls.Add(this.classicAntialias);
            this.cameraTab.Controls.Add(this.samplingPattern);
            this.cameraTab.Controls.Add(this.lblSamplingPattern);
            this.cameraTab.Controls.Add(this.cameraAntialias);
            this.cameraTab.Controls.Add(this.lblCameraAntialias);
            this.cameraTab.Controls.Add(this.cameraType);
            this.cameraTab.Controls.Add(this.label20);
            this.cameraTab.Controls.Add(this.cameraName);
            this.cameraTab.Controls.Add(this.label16);
            this.cameraTab.Location = new System.Drawing.Point(4, 22);
            this.cameraTab.Name = "cameraTab";
            this.cameraTab.Size = new System.Drawing.Size(437, 324);
            this.cameraTab.TabIndex = 5;
            this.cameraTab.Text = "Camera";
            this.cameraTab.UseVisualStyleBackColor = true;
            // 
            // imageAspect
            // 
            this.imageAspect.Location = new System.Drawing.Point(151, 247);
            this.imageAspect.Name = "imageAspect";
            this.imageAspect.Size = new System.Drawing.Size(79, 20);
            this.imageAspect.TabIndex = 31;
            this.imageAspect.Text = "1.0";
            this.imageAspect.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTips.SetToolTip(this.imageAspect, "Defines the aspect ratio of the image. 1.0 means a square pixel ratio.");
            this.imageAspect.Validating += new System.ComponentModel.CancelEventHandler(this.positiveDouble_Validating);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 250);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 13);
            this.label14.TabIndex = 30;
            this.label14.Text = "Aspect:";
            // 
            // imageHeight
            // 
            this.imageHeight.Location = new System.Drawing.Point(151, 221);
            this.imageHeight.Name = "imageHeight";
            this.imageHeight.Size = new System.Drawing.Size(79, 20);
            this.imageHeight.TabIndex = 29;
            this.imageHeight.Text = "480";
            this.imageHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTips.SetToolTip(this.imageHeight, "The image height.");
            this.imageHeight.Validating += new System.ComponentModel.CancelEventHandler(this.positiveNumber_Validating);
            // 
            // imageWidth
            // 
            this.imageWidth.Location = new System.Drawing.Point(151, 195);
            this.imageWidth.Name = "imageWidth";
            this.imageWidth.Size = new System.Drawing.Size(79, 20);
            this.imageWidth.TabIndex = 27;
            this.imageWidth.Text = "640";
            this.imageWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTips.SetToolTip(this.imageWidth, "The image width.");
            this.imageWidth.Validating += new System.ComponentModel.CancelEventHandler(this.positiveNumber_Validating);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 224);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 13);
            this.label13.TabIndex = 28;
            this.label13.Text = "Height:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 195);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 13);
            this.label12.TabIndex = 26;
            this.label12.Text = "Width:";
            // 
            // adaptiveThreshold
            // 
            this.adaptiveThreshold.Location = new System.Drawing.Point(382, 170);
            this.adaptiveThreshold.Name = "adaptiveThreshold";
            this.adaptiveThreshold.Size = new System.Drawing.Size(52, 20);
            this.adaptiveThreshold.TabIndex = 25;
            this.adaptiveThreshold.Text = "0.1";
            this.adaptiveThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblAdaptiveThreshold
            // 
            this.lblAdaptiveThreshold.AutoSize = true;
            this.lblAdaptiveThreshold.Location = new System.Drawing.Point(303, 174);
            this.lblAdaptiveThreshold.Name = "lblAdaptiveThreshold";
            this.lblAdaptiveThreshold.Size = new System.Drawing.Size(57, 13);
            this.lblAdaptiveThreshold.TabIndex = 24;
            this.lblAdaptiveThreshold.Text = "Threshold:";
            // 
            // adaptiveSampling
            // 
            this.adaptiveSampling.AutoSize = true;
            this.adaptiveSampling.Location = new System.Drawing.Point(151, 172);
            this.adaptiveSampling.Name = "adaptiveSampling";
            this.adaptiveSampling.Size = new System.Drawing.Size(114, 17);
            this.adaptiveSampling.TabIndex = 23;
            this.adaptiveSampling.Text = "Adaptive Sampling";
            this.adaptiveSampling.UseVisualStyleBackColor = true;
            this.adaptiveSampling.CheckedChanged += new System.EventHandler(this.adaptiveSampling_CheckedChanged);
            // 
            // softFilter
            // 
            this.softFilter.AutoSize = true;
            this.softFilter.Location = new System.Drawing.Point(10, 172);
            this.softFilter.Name = "softFilter";
            this.softFilter.Size = new System.Drawing.Size(70, 17);
            this.softFilter.TabIndex = 22;
            this.softFilter.Text = "Soft Filter";
            this.softFilter.UseVisualStyleBackColor = true;
            // 
            // lblReconFilter
            // 
            this.lblReconFilter.AutoSize = true;
            this.lblReconFilter.Location = new System.Drawing.Point(7, 148);
            this.lblReconFilter.Name = "lblReconFilter";
            this.lblReconFilter.Size = new System.Drawing.Size(82, 13);
            this.lblReconFilter.TabIndex = 20;
            this.lblReconFilter.Text = "Reconstruction:";
            // 
            // lblAntialias
            // 
            this.lblAntialias.AutoSize = true;
            this.lblAntialias.Location = new System.Drawing.Point(7, 121);
            this.lblAntialias.Name = "lblAntialias";
            this.lblAntialias.Size = new System.Drawing.Size(138, 13);
            this.lblAntialias.TabIndex = 18;
            this.lblAntialias.Text = "Classic Camera Antialiasing:";
            // 
            // reconFilter
            // 
            this.reconFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.reconFilter.FormattingEnabled = true;
            this.reconFilter.Items.AddRange(new object[] {
            "Classic",
            "Box",
            "Box (Sharp)",
            "Box (Soft)",
            "Gaussian",
            "Gaussian (Sharp)",
            "Gaussian (Soft)",
            "Mitchell",
            "Mitchell (Sharp)",
            "Mitchell (Soft)",
            "Lanczos",
            "Lanczos (Sharp)",
            "Lanczos (Soft)"});
            this.reconFilter.Location = new System.Drawing.Point(151, 145);
            this.reconFilter.Name = "reconFilter";
            this.reconFilter.Size = new System.Drawing.Size(283, 21);
            this.reconFilter.TabIndex = 21;
            // 
            // classicAntialias
            // 
            this.classicAntialias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.classicAntialias.FormattingEnabled = true;
            this.classicAntialias.Items.AddRange(new object[] {
            "None",
            "PLD  1 - Pass",
            "PLD  2 - Pass",
            "PLD  3 - Pass",
            "PLD  4 - Pass",
            "PLD  5 - Pass",
            "PLD  7 - Pass",
            "PLD  9 - Pass",
            "PLD 12 - Pass",
            "PLD 15 - Pass",
            "PLD 17 - Pass",
            "PLD 21 - Pass",
            "PLD 24 - Pass",
            "PLD 28 - Pass",
            "PLD 33 - Pass",
            "PLD 35 - Pass",
            "Classic, Low",
            "Classic, Enhanced Low",
            "Classic, Medium",
            "Classic, Enhanced Medium",
            "Classic, High",
            "Classic, Enhanced High",
            "Classic, Extreme",
            "Classic, Enhanced Extreme"});
            this.classicAntialias.Location = new System.Drawing.Point(151, 118);
            this.classicAntialias.Name = "classicAntialias";
            this.classicAntialias.Size = new System.Drawing.Size(283, 21);
            this.classicAntialias.TabIndex = 19;
            this.classicAntialias.SelectedIndexChanged += new System.EventHandler(this.classicAntialias_SelectedIndexChanged);
            // 
            // samplingPattern
            // 
            this.samplingPattern.Enabled = false;
            this.samplingPattern.FormattingEnabled = true;
            this.samplingPattern.Items.AddRange(new object[] {
            "Blue Noise",
            "Fixed",
            "Classic"});
            this.samplingPattern.Location = new System.Drawing.Point(151, 91);
            this.samplingPattern.Name = "samplingPattern";
            this.samplingPattern.Size = new System.Drawing.Size(283, 21);
            this.samplingPattern.TabIndex = 7;
            // 
            // lblSamplingPattern
            // 
            this.lblSamplingPattern.AutoSize = true;
            this.lblSamplingPattern.Enabled = false;
            this.lblSamplingPattern.Location = new System.Drawing.Point(7, 94);
            this.lblSamplingPattern.Name = "lblSamplingPattern";
            this.lblSamplingPattern.Size = new System.Drawing.Size(89, 13);
            this.lblSamplingPattern.TabIndex = 6;
            this.lblSamplingPattern.Text = "Sampling pattern:";
            // 
            // cameraAntialias
            // 
            this.cameraAntialias.Enabled = false;
            this.cameraAntialias.Location = new System.Drawing.Point(151, 67);
            this.cameraAntialias.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.cameraAntialias.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cameraAntialias.Name = "cameraAntialias";
            this.cameraAntialias.Size = new System.Drawing.Size(89, 20);
            this.cameraAntialias.TabIndex = 5;
            this.cameraAntialias.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblCameraAntialias
            // 
            this.lblCameraAntialias.AutoSize = true;
            this.lblCameraAntialias.Enabled = false;
            this.lblCameraAntialias.Location = new System.Drawing.Point(7, 67);
            this.lblCameraAntialias.Name = "lblCameraAntialias";
            this.lblCameraAntialias.Size = new System.Drawing.Size(63, 13);
            this.lblCameraAntialias.TabIndex = 4;
            this.lblCameraAntialias.Text = "Antialiasing:";
            // 
            // cameraType
            // 
            this.cameraType.Location = new System.Drawing.Point(151, 39);
            this.cameraType.Name = "cameraType";
            this.cameraType.ReadOnly = true;
            this.cameraType.Size = new System.Drawing.Size(283, 20);
            this.cameraType.TabIndex = 3;
            this.cameraType.Text = "Classic";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(7, 42);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(34, 13);
            this.label20.TabIndex = 2;
            this.label20.Text = "Type:";
            // 
            // cameraName
            // 
            this.cameraName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cameraName.FormattingEnabled = true;
            this.cameraName.Location = new System.Drawing.Point(151, 12);
            this.cameraName.Name = "cameraName";
            this.cameraName.Size = new System.Drawing.Size(283, 21);
            this.cameraName.TabIndex = 1;
            this.cameraName.SelectedIndexChanged += new System.EventHandler(this.cameraName_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(7, 15);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(78, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "Active camera:";
            // 
            // giTab
            // 
            this.giTab.Controls.Add(this.indirectGI);
            this.giTab.Controls.Add(this.rayGI);
            this.giTab.Controls.Add(this.lblIndirectGI);
            this.giTab.Controls.Add(this.minPixelGI);
            this.giTab.Controls.Add(this.minEvalGI);
            this.giTab.Controls.Add(this.toleranceGI);
            this.giTab.Controls.Add(this.intensityGI);
            this.giTab.Controls.Add(this.lblRayGI);
            this.giTab.Controls.Add(this.lblMinPixelGI);
            this.giTab.Controls.Add(this.lblMinEvalGI);
            this.giTab.Controls.Add(this.lblToleranceGI);
            this.giTab.Controls.Add(this.lblIntensityGI);
            this.giTab.Controls.Add(this.directionalGI);
            this.giTab.Controls.Add(this.volumetricGI);
            this.giTab.Controls.Add(this.cachedGI);
            this.giTab.Controls.Add(this.useAmbientGI);
            this.giTab.Controls.Add(this.backdropTranspGI);
            this.giTab.Controls.Add(this.interpolatedGI);
            this.giTab.Controls.Add(this.radiosityType);
            this.giTab.Controls.Add(this.label24);
            this.giTab.Location = new System.Drawing.Point(4, 22);
            this.giTab.Name = "giTab";
            this.giTab.Size = new System.Drawing.Size(437, 324);
            this.giTab.TabIndex = 6;
            this.giTab.Text = "GI";
            this.giTab.UseVisualStyleBackColor = true;
            // 
            // indirectGI
            // 
            this.indirectGI.Enabled = false;
            this.indirectGI.Location = new System.Drawing.Point(128, 157);
            this.indirectGI.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.indirectGI.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.indirectGI.Name = "indirectGI";
            this.indirectGI.Size = new System.Drawing.Size(55, 20);
            this.indirectGI.TabIndex = 13;
            this.indirectGI.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // rayGI
            // 
            this.rayGI.Enabled = false;
            this.rayGI.Location = new System.Drawing.Point(128, 131);
            this.rayGI.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.rayGI.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.rayGI.Name = "rayGI";
            this.rayGI.Size = new System.Drawing.Size(55, 20);
            this.rayGI.TabIndex = 11;
            this.rayGI.Value = new decimal(new int[] {
            48,
            0,
            0,
            0});
            // 
            // lblIndirectGI
            // 
            this.lblIndirectGI.AutoSize = true;
            this.lblIndirectGI.Enabled = false;
            this.lblIndirectGI.Location = new System.Drawing.Point(14, 159);
            this.lblIndirectGI.Name = "lblIndirectGI";
            this.lblIndirectGI.Size = new System.Drawing.Size(89, 13);
            this.lblIndirectGI.TabIndex = 12;
            this.lblIndirectGI.Text = "Indirect bounces:";
            // 
            // minPixelGI
            // 
            this.minPixelGI.Enabled = false;
            this.minPixelGI.Location = new System.Drawing.Point(370, 157);
            this.minPixelGI.Name = "minPixelGI";
            this.minPixelGI.Size = new System.Drawing.Size(55, 20);
            this.minPixelGI.TabIndex = 19;
            this.minPixelGI.Text = "4.0";
            this.minPixelGI.Validating += new System.ComponentModel.CancelEventHandler(this.positiveDouble_Validating);
            // 
            // minEvalGI
            // 
            this.minEvalGI.Enabled = false;
            this.minEvalGI.Location = new System.Drawing.Point(370, 131);
            this.minEvalGI.Name = "minEvalGI";
            this.minEvalGI.Size = new System.Drawing.Size(55, 20);
            this.minEvalGI.TabIndex = 17;
            this.minEvalGI.Text = "20";
            this.minEvalGI.Validating += new System.ComponentModel.CancelEventHandler(this.positiveZeroDouble_Validating);
            // 
            // toleranceGI
            // 
            this.toleranceGI.Enabled = false;
            this.toleranceGI.Location = new System.Drawing.Point(370, 105);
            this.toleranceGI.Name = "toleranceGI";
            this.toleranceGI.Size = new System.Drawing.Size(55, 20);
            this.toleranceGI.TabIndex = 15;
            this.toleranceGI.Text = "0.0";
            this.toleranceGI.Validating += new System.ComponentModel.CancelEventHandler(this.positiveZeroDouble_Validating);
            // 
            // intensityGI
            // 
            this.intensityGI.Enabled = false;
            this.intensityGI.Location = new System.Drawing.Point(128, 104);
            this.intensityGI.Name = "intensityGI";
            this.intensityGI.Size = new System.Drawing.Size(55, 20);
            this.intensityGI.TabIndex = 9;
            this.intensityGI.Text = "100.0";
            this.intensityGI.Validating += new System.ComponentModel.CancelEventHandler(this.positiveZeroDouble_Validating);
            // 
            // lblRayGI
            // 
            this.lblRayGI.AutoSize = true;
            this.lblRayGI.Enabled = false;
            this.lblRayGI.Location = new System.Drawing.Point(14, 133);
            this.lblRayGI.Name = "lblRayGI";
            this.lblRayGI.Size = new System.Drawing.Size(104, 13);
            this.lblRayGI.TabIndex = 10;
            this.lblRayGI.Text = "Rays per evaluation:";
            // 
            // lblMinPixelGI
            // 
            this.lblMinPixelGI.AutoSize = true;
            this.lblMinPixelGI.Enabled = false;
            this.lblMinPixelGI.Location = new System.Drawing.Point(239, 160);
            this.lblMinPixelGI.Name = "lblMinPixelGI";
            this.lblMinPixelGI.Size = new System.Drawing.Size(91, 13);
            this.lblMinPixelGI.TabIndex = 18;
            this.lblMinPixelGI.Text = "Min pixel spacing:";
            // 
            // lblMinEvalGI
            // 
            this.lblMinEvalGI.AutoSize = true;
            this.lblMinEvalGI.Enabled = false;
            this.lblMinEvalGI.Location = new System.Drawing.Point(239, 134);
            this.lblMinEvalGI.Name = "lblMinEvalGI";
            this.lblMinEvalGI.Size = new System.Drawing.Size(93, 13);
            this.lblMinEvalGI.TabIndex = 16;
            this.lblMinEvalGI.Text = "Min eval. spacing:";
            // 
            // lblToleranceGI
            // 
            this.lblToleranceGI.AutoSize = true;
            this.lblToleranceGI.Enabled = false;
            this.lblToleranceGI.Location = new System.Drawing.Point(239, 108);
            this.lblToleranceGI.Name = "lblToleranceGI";
            this.lblToleranceGI.Size = new System.Drawing.Size(58, 13);
            this.lblToleranceGI.TabIndex = 14;
            this.lblToleranceGI.Text = "Tolerance:";
            // 
            // lblIntensityGI
            // 
            this.lblIntensityGI.AutoSize = true;
            this.lblIntensityGI.Enabled = false;
            this.lblIntensityGI.Location = new System.Drawing.Point(14, 107);
            this.lblIntensityGI.Name = "lblIntensityGI";
            this.lblIntensityGI.Size = new System.Drawing.Size(49, 13);
            this.lblIntensityGI.TabIndex = 8;
            this.lblIntensityGI.Text = "Intensity:";
            // 
            // directionalGI
            // 
            this.directionalGI.AutoSize = true;
            this.directionalGI.Enabled = false;
            this.directionalGI.Location = new System.Drawing.Point(242, 83);
            this.directionalGI.Name = "directionalGI";
            this.directionalGI.Size = new System.Drawing.Size(98, 17);
            this.directionalGI.TabIndex = 7;
            this.directionalGI.Text = "Directional rays";
            this.directionalGI.UseVisualStyleBackColor = true;
            // 
            // volumetricGI
            // 
            this.volumetricGI.AutoSize = true;
            this.volumetricGI.Enabled = false;
            this.volumetricGI.Location = new System.Drawing.Point(242, 60);
            this.volumetricGI.Name = "volumetricGI";
            this.volumetricGI.Size = new System.Drawing.Size(75, 17);
            this.volumetricGI.TabIndex = 5;
            this.volumetricGI.Text = "Volumetric";
            this.volumetricGI.UseVisualStyleBackColor = true;
            // 
            // cachedGI
            // 
            this.cachedGI.AutoSize = true;
            this.cachedGI.Enabled = false;
            this.cachedGI.Location = new System.Drawing.Point(242, 37);
            this.cachedGI.Name = "cachedGI";
            this.cachedGI.Size = new System.Drawing.Size(63, 17);
            this.cachedGI.TabIndex = 3;
            this.cachedGI.Text = "Cached";
            this.cachedGI.UseVisualStyleBackColor = true;
            // 
            // useAmbientGI
            // 
            this.useAmbientGI.AutoSize = true;
            this.useAmbientGI.Enabled = false;
            this.useAmbientGI.Location = new System.Drawing.Point(17, 83);
            this.useAmbientGI.Name = "useAmbientGI";
            this.useAmbientGI.Size = new System.Drawing.Size(85, 17);
            this.useAmbientGI.TabIndex = 6;
            this.useAmbientGI.Text = "Use ambient";
            this.useAmbientGI.UseVisualStyleBackColor = true;
            // 
            // backdropTranspGI
            // 
            this.backdropTranspGI.AutoSize = true;
            this.backdropTranspGI.Enabled = false;
            this.backdropTranspGI.Location = new System.Drawing.Point(17, 60);
            this.backdropTranspGI.Name = "backdropTranspGI";
            this.backdropTranspGI.Size = new System.Drawing.Size(136, 17);
            this.backdropTranspGI.TabIndex = 4;
            this.backdropTranspGI.Text = "Backdrop transparency";
            this.backdropTranspGI.UseVisualStyleBackColor = true;
            // 
            // interpolatedGI
            // 
            this.interpolatedGI.AutoSize = true;
            this.interpolatedGI.Enabled = false;
            this.interpolatedGI.Location = new System.Drawing.Point(17, 37);
            this.interpolatedGI.Name = "interpolatedGI";
            this.interpolatedGI.Size = new System.Drawing.Size(82, 17);
            this.interpolatedGI.TabIndex = 2;
            this.interpolatedGI.Text = "Interpolated";
            this.interpolatedGI.UseVisualStyleBackColor = true;
            this.interpolatedGI.CheckedChanged += new System.EventHandler(this.interpolatedGI_CheckedChanged);
            // 
            // radiosityType
            // 
            this.radiosityType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.radiosityType.FormattingEnabled = true;
            this.radiosityType.Items.AddRange(new object[] {
            "None",
            "Backdrop only",
            "Monte Carlo",
            "Final Gather"});
            this.radiosityType.Location = new System.Drawing.Point(110, 10);
            this.radiosityType.Name = "radiosityType";
            this.radiosityType.Size = new System.Drawing.Size(315, 21);
            this.radiosityType.TabIndex = 1;
            this.radiosityType.SelectedIndexChanged += new System.EventHandler(this.radiosityType_SelectedIndexChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(14, 13);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(53, 13);
            this.label24.TabIndex = 0;
            this.label24.Text = "Radiosity:";
            // 
            // logTab
            // 
            this.logTab.Controls.Add(this.textViewLog);
            this.logTab.Location = new System.Drawing.Point(4, 22);
            this.logTab.Name = "logTab";
            this.logTab.Size = new System.Drawing.Size(437, 324);
            this.logTab.TabIndex = 7;
            this.logTab.Text = "Log";
            this.logTab.UseVisualStyleBackColor = true;
            // 
            // textViewLog
            // 
            this.textViewLog.Location = new System.Drawing.Point(3, 3);
            this.textViewLog.Multiline = true;
            this.textViewLog.Name = "textViewLog";
            this.textViewLog.ReadOnly = true;
            this.textViewLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textViewLog.Size = new System.Drawing.Size(431, 318);
            this.textViewLog.TabIndex = 0;
            // 
            // checkValues
            // 
            this.checkValues.ContainerControl = this;
            // 
            // toolTips
            // 
            this.toolTips.AutomaticDelay = 0;
            this.toolTips.AutoPopDelay = 20000;
            this.toolTips.InitialDelay = 100;
            this.toolTips.ReshowDelay = 100;
            this.toolTips.UseAnimation = false;
            this.toolTips.UseFading = false;
            // 
            // btnSaveProject
            // 
            this.btnSaveProject.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSaveProject.Image = global::Amleto.Properties.Resources.table_save;
            this.btnSaveProject.Location = new System.Drawing.Point(46, 368);
            this.btnSaveProject.Name = "btnSaveProject";
            this.btnSaveProject.Size = new System.Drawing.Size(24, 23);
            this.btnSaveProject.TabIndex = 2;
            this.toolTips.SetToolTip(this.btnSaveProject, "Store the current settings");
            this.btnSaveProject.UseVisualStyleBackColor = true;
            this.btnSaveProject.Click += new System.EventHandler(this.btnSaveProject_Click);
            // 
            // btnLoadProject
            // 
            this.btnLoadProject.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnLoadProject.Image = global::Amleto.Properties.Resources.folder_table;
            this.btnLoadProject.Location = new System.Drawing.Point(16, 368);
            this.btnLoadProject.Name = "btnLoadProject";
            this.btnLoadProject.Size = new System.Drawing.Size(24, 23);
            this.btnLoadProject.TabIndex = 2;
            this.toolTips.SetToolTip(this.btnLoadProject, "Restore a project settings");
            this.btnLoadProject.UseVisualStyleBackColor = true;
            this.btnLoadProject.Click += new System.EventHandler(this.btnLoadProject_Click);
            // 
            // AddProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 403);
            this.Controls.Add(this.renderSetupTabs);
            this.Controls.Add(this.btnLoadProject);
            this.Controls.Add(this.btnSaveProject);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddProject";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddProject_FormClosing);
            this.Load += new System.EventHandler(this.AddProject_Load);
            this.renderSetupTabs.ResumeLayout(false);
            this.sceneTab.ResumeLayout(false);
            this.sceneTab.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.framesTab.ResumeLayout(false);
            this.framesTab.PerformLayout();
            this.masterPluginTab.ResumeLayout(false);
            this.masterPluginTab.PerformLayout();
            this.noticeTab.ResumeLayout(false);
            this.noticeTab.PerformLayout();
            this.qualityTab.ResumeLayout(false);
            this.qualityTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recusionLimit)).EndInit();
            this.cameraTab.ResumeLayout(false);
            this.cameraTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cameraAntialias)).EndInit();
            this.giTab.ResumeLayout(false);
            this.giTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.indirectGI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rayGI)).EndInit();
            this.logTab.ResumeLayout(false);
            this.logTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkValues)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textScene;
        private System.Windows.Forms.Button btnSelectScene;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textContentDir;
        private System.Windows.Forms.Button btnSelectContentDir;
        private System.Windows.Forms.TextBox textOutputDir;
        private System.Windows.Forms.Button btnSelectOuputDir;
        private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TabControl renderSetupTabs;
		private System.Windows.Forms.TabPage qualityTab;
        private System.Windows.Forms.ErrorProvider checkValues;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox renderMode;
        private System.Windows.Forms.NumericUpDown recusionLimit;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox traceOcclusion;
        private System.Windows.Forms.CheckBox traceTransp;
        private System.Windows.Forms.CheckBox traceRefraction;
        private System.Windows.Forms.CheckBox traceReflection;
        private System.Windows.Forms.CheckBox traceShadow;
		private System.Windows.Forms.CheckBox renderLine;
        private System.Windows.Forms.TabPage noticeTab;
        private System.Windows.Forms.TextBox emailSubjectOk;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox emailTo;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox emailSubjectNotOk;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckBox emailContainLog;
		private System.Windows.Forms.CheckBox emailNotify;
        private System.Windows.Forms.ToolTip toolTips;
        private System.Windows.Forms.TabPage sceneTab;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox listConfig;
        private System.Windows.Forms.TabPage masterPluginTab;
        private System.Windows.Forms.CheckedListBox masterList;
		private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TabPage cameraTab;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox samplingPattern;
        private System.Windows.Forms.Label lblSamplingPattern;
        private System.Windows.Forms.NumericUpDown cameraAntialias;
        private System.Windows.Forms.Label lblCameraAntialias;
        private System.Windows.Forms.TextBox cameraType;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cameraName;
        private System.Windows.Forms.TabPage giTab;
        private System.Windows.Forms.ComboBox radiosityType;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.NumericUpDown rayGI;
        private System.Windows.Forms.TextBox intensityGI;
        private System.Windows.Forms.Label lblIntensityGI;
        private System.Windows.Forms.CheckBox directionalGI;
        private System.Windows.Forms.CheckBox volumetricGI;
        private System.Windows.Forms.CheckBox cachedGI;
        private System.Windows.Forms.CheckBox useAmbientGI;
        private System.Windows.Forms.CheckBox backdropTranspGI;
        private System.Windows.Forms.CheckBox interpolatedGI;
        private System.Windows.Forms.NumericUpDown indirectGI;
        private System.Windows.Forms.Label lblIndirectGI;
        private System.Windows.Forms.TextBox minPixelGI;
        private System.Windows.Forms.TextBox minEvalGI;
        private System.Windows.Forms.TextBox toleranceGI;
        private System.Windows.Forms.Label lblRayGI;
        private System.Windows.Forms.Label lblMinPixelGI;
        private System.Windows.Forms.Label lblMinEvalGI;
        private System.Windows.Forms.Label lblToleranceGI;
        private System.Windows.Forms.TextBox adaptiveThreshold;
        private System.Windows.Forms.Label lblAdaptiveThreshold;
        private System.Windows.Forms.CheckBox adaptiveSampling;
        private System.Windows.Forms.CheckBox softFilter;
        private System.Windows.Forms.Label lblReconFilter;
        private System.Windows.Forms.Label lblAntialias;
        private System.Windows.Forms.ComboBox reconFilter;
        private System.Windows.Forms.ComboBox classicAntialias;
        private System.Windows.Forms.TabPage logTab;
        private System.Windows.Forms.TextBox textViewLog;
        private System.Windows.Forms.Button btnSaveProject;
        private System.Windows.Forms.Button btnLoadProject;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox listOuputFormat;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox listImageFormat;
        private System.Windows.Forms.TextBox textPrefix;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox AlphaPrefix;
        private System.Windows.Forms.CheckBox SaveAlpha;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox AlphaImageFormat;
		private System.Windows.Forms.CheckBox OverrideSettings;
		private System.Windows.Forms.TextBox imageAspect;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox imageHeight;
		private System.Windows.Forms.TextBox imageWidth;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TabPage framesTab;
		private System.Windows.Forms.TextBox textStartFrame;
		private System.Windows.Forms.TextBox textEndFrame;
		private System.Windows.Forms.TextBox textFrameStep;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox renderBlock;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label lblNbSlices;
		private System.Windows.Forms.Label lblSplitExplain;
		private System.Windows.Forms.TextBox slicesDown;
		private System.Windows.Forms.TextBox slicesOverlap;
		private System.Windows.Forms.Label lblSlicesOverlap;
        private System.Windows.Forms.CheckBox OverwriteFrames;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox slicesAcross;
        private System.Windows.Forms.CheckBox deleteSplitFrames;
        private System.Windows.Forms.Label label27;
    }
}