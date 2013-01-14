namespace Amleto
{
    partial class ServerWin
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerWin));
            this.status = new System.Windows.Forms.StatusStrip();
            this.toolStripStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.mainTabs = new System.Windows.Forms.TabControl();
            this.clientStatus = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ClientStatusGrid = new System.Windows.Forms.DataGridView();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.ActiveProjectGrid = new System.Windows.Forms.DataGridView();
            this.projId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Project = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOwner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Start = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.End = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjectStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.elapsedCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstimatedCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FinishedProjectGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeStarted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RenderTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.messages = new System.Windows.Forms.TabPage();
            this.messageList = new System.Windows.Forms.DataGridView();
            this.colType = new System.Windows.Forms.DataGridViewImageColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabRender = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.textPreviewSpeed = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPlay = new System.Windows.Forms.CheckBox();
            this.autoRenderLast = new System.Windows.Forms.CheckBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.renderTree = new System.Windows.Forms.TreeView();
            this.contextMenuRender = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureRender = new System.Windows.Forms.PictureBox();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.nodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jobsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolAddJob = new System.Windows.Forms.ToolStripButton();
            this.toolEditJob = new System.Windows.Forms.ToolStripButton();
            this.toolDeleteJob = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolPauseNode = new System.Windows.Forms.ToolStripButton();
            this.toolResumeNode = new System.Windows.Forms.ToolStripButton();
            this.toolTimeActivation = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolViewMessages = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStopNode = new System.Windows.Forms.ToolStripButton();
            this.contextMenuNodes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuSendKill = new System.Windows.Forms.ToolStripMenuItem();
            this.showMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resendConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renderPriorityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.highToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboveNormalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.belowNormalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setupActivationTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resumeClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopAllNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cleanClientContentDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cleanAllClientsContentDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cleanClientOutputDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cleanAllClientsOutputDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuProjects = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resumeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playTimer = new System.Windows.Forms.Timer(this.components);
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.iconList = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuFinished = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.FinishedMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.FinishedMenuClear = new System.Windows.Forms.ToolStripMenuItem();
            this.FinishedMenuClearAll = new System.Windows.Forms.ToolStripMenuItem();
            this.HostId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hostName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HostInstance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCurConfig = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.hostStatus = new System.Windows.Forms.DataGridViewImageColumn();
            this.LastRender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hostJob = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.mainTabs.SuspendLayout();
            this.clientStatus.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ClientStatusGrid)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ActiveProjectGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FinishedProjectGrid)).BeginInit();
            this.messages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messageList)).BeginInit();
            this.tabRender.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.contextMenuRender.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureRender)).BeginInit();
            this.mainMenu.SuspendLayout();
            this.mainToolStrip.SuspendLayout();
            this.contextMenuNodes.SuspendLayout();
            this.contextMenuProjects.SuspendLayout();
            this.contextMenuFinished.SuspendLayout();
            this.SuspendLayout();
            // 
            // status
            // 
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatus});
            this.status.Location = new System.Drawing.Point(0, 692);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(884, 22);
            this.status.TabIndex = 0;
            this.status.Text = "statusStrip1";
            // 
            // toolStripStatus
            // 
            this.toolStripStatus.Name = "toolStripStatus";
            this.toolStripStatus.Size = new System.Drawing.Size(869, 17);
            this.toolStripStatus.Spring = true;
            this.toolStripStatus.Text = "Welcome to the Amleto console";
            this.toolStripStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.AllowDrop = true;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.mainTabs);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(884, 643);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(884, 692);
            this.toolStripContainer1.TabIndex = 1;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.mainMenu);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.mainToolStrip);
            this.toolStripContainer1.DragDrop += new System.Windows.Forms.DragEventHandler(this.ServerWin_DragDrop);
            this.toolStripContainer1.DragEnter += new System.Windows.Forms.DragEventHandler(this.ServerWin_DragEnter);
            // 
            // mainTabs
            // 
            this.mainTabs.AllowDrop = true;
            this.mainTabs.Controls.Add(this.clientStatus);
            this.mainTabs.Controls.Add(this.messages);
            this.mainTabs.Controls.Add(this.tabRender);
            this.mainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabs.Location = new System.Drawing.Point(0, 0);
            this.mainTabs.Name = "mainTabs";
            this.mainTabs.SelectedIndex = 0;
            this.mainTabs.Size = new System.Drawing.Size(884, 643);
            this.mainTabs.TabIndex = 0;
            this.mainTabs.DragDrop += new System.Windows.Forms.DragEventHandler(this.ServerWin_DragDrop);
            this.mainTabs.DragEnter += new System.Windows.Forms.DragEventHandler(this.ServerWin_DragEnter);
            // 
            // clientStatus
            // 
            this.clientStatus.AllowDrop = true;
            this.clientStatus.Controls.Add(this.splitContainer1);
            this.clientStatus.Location = new System.Drawing.Point(4, 22);
            this.clientStatus.Name = "clientStatus";
            this.clientStatus.Padding = new System.Windows.Forms.Padding(3);
            this.clientStatus.Size = new System.Drawing.Size(876, 617);
            this.clientStatus.TabIndex = 0;
            this.clientStatus.Text = "Clients & Projects Status";
            this.clientStatus.UseVisualStyleBackColor = true;
            this.clientStatus.DragDrop += new System.Windows.Forms.DragEventHandler(this.ServerWin_DragDrop);
            this.clientStatus.DragEnter += new System.Windows.Forms.DragEventHandler(this.ServerWin_DragEnter);
            // 
            // splitContainer1
            // 
            this.splitContainer1.AllowDrop = true;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ClientStatusGrid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AllowDrop = true;
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer1.Panel2.DragDrop += new System.Windows.Forms.DragEventHandler(this.ServerWin_DragEnter);
            this.splitContainer1.Panel2.DragEnter += new System.Windows.Forms.DragEventHandler(this.ServerWin_DragEnter);
            this.splitContainer1.Size = new System.Drawing.Size(870, 611);
            this.splitContainer1.SplitterDistance = 208;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.DragDrop += new System.Windows.Forms.DragEventHandler(this.ServerWin_DragDrop);
            this.splitContainer1.DragEnter += new System.Windows.Forms.DragEventHandler(this.ServerWin_DragEnter);
            // 
            // ClientStatusGrid
            // 
            this.ClientStatusGrid.AllowUserToAddRows = false;
            this.ClientStatusGrid.AllowUserToDeleteRows = false;
            this.ClientStatusGrid.AllowUserToResizeRows = false;
            this.ClientStatusGrid.BackgroundColor = System.Drawing.Color.White;
            this.ClientStatusGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ClientStatusGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HostId,
            this.hostName,
            this.HostInstance,
            this.colCurConfig,
            this.hostStatus,
            this.LastRender,
            this.hostJob});
            this.ClientStatusGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClientStatusGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.ClientStatusGrid.Location = new System.Drawing.Point(0, 0);
            this.ClientStatusGrid.Name = "ClientStatusGrid";
            this.ClientStatusGrid.RowHeadersVisible = false;
            this.ClientStatusGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ClientStatusGrid.ShowEditingIcon = false;
            this.ClientStatusGrid.Size = new System.Drawing.Size(870, 208);
            this.ClientStatusGrid.TabIndex = 1;
            this.ClientStatusGrid.Tag = "Shows the nodes which are currently usable to render projects. Ctrl+Click to mult" +
    "i select.";
            this.ClientStatusGrid.CurrentCellDirtyStateChanged += new System.EventHandler(this.clientStatusGrid_CurrentCellDirtyStateChanged);
            this.ClientStatusGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.clientStatusGrid_MouseClick);
            this.ClientStatusGrid.MouseEnter += new System.EventHandler(this.tool_MouseHover);
            this.ClientStatusGrid.MouseLeave += new System.EventHandler(this.tool_MouseLeave);
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.ActiveProjectGrid);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.FinishedProjectGrid);
            this.splitContainer4.Size = new System.Drawing.Size(870, 399);
            this.splitContainer4.SplitterDistance = 210;
            this.splitContainer4.TabIndex = 1;
            // 
            // ActiveProjectGrid
            // 
            this.ActiveProjectGrid.AllowDrop = true;
            this.ActiveProjectGrid.AllowUserToAddRows = false;
            this.ActiveProjectGrid.AllowUserToDeleteRows = false;
            this.ActiveProjectGrid.BackgroundColor = System.Drawing.Color.White;
            this.ActiveProjectGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ActiveProjectGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.projId,
            this.Project,
            this.colOwner,
            this.Start,
            this.End,
            this.NB,
            this.ProjectStatus,
            this.elapsedCol,
            this.EstimatedCol});
            this.ActiveProjectGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ActiveProjectGrid.Location = new System.Drawing.Point(0, 0);
            this.ActiveProjectGrid.MultiSelect = false;
            this.ActiveProjectGrid.Name = "ActiveProjectGrid";
            this.ActiveProjectGrid.ReadOnly = true;
            this.ActiveProjectGrid.RowHeadersVisible = false;
            this.ActiveProjectGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ActiveProjectGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ActiveProjectGrid.Size = new System.Drawing.Size(870, 210);
            this.ActiveProjectGrid.TabIndex = 0;
            this.ActiveProjectGrid.Tag = "Shows the projects in the queue. Right click to access the context menu.";
            this.ActiveProjectGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.projectStatusGrid_CellDoubleClick);
            this.ActiveProjectGrid.DragDrop += new System.Windows.Forms.DragEventHandler(this.ServerWin_DragDrop);
            this.ActiveProjectGrid.DragEnter += new System.Windows.Forms.DragEventHandler(this.ServerWin_DragEnter);
            this.ActiveProjectGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ProjectStatusGridMouseClick);
            this.ActiveProjectGrid.MouseEnter += new System.EventHandler(this.tool_MouseHover);
            this.ActiveProjectGrid.MouseLeave += new System.EventHandler(this.tool_MouseLeave);
            // 
            // projId
            // 
            this.projId.HeaderText = "ID";
            this.projId.Name = "projId";
            this.projId.ReadOnly = true;
            this.projId.Width = 40;
            // 
            // Project
            // 
            this.Project.HeaderText = "Active Project Name";
            this.Project.Name = "Project";
            this.Project.ReadOnly = true;
            this.Project.Width = 257;
            // 
            // colOwner
            // 
            this.colOwner.HeaderText = "Owner";
            this.colOwner.Name = "colOwner";
            this.colOwner.ReadOnly = true;
            // 
            // Start
            // 
            this.Start.HeaderText = "Start";
            this.Start.Name = "Start";
            this.Start.ReadOnly = true;
            this.Start.Width = 70;
            // 
            // End
            // 
            this.End.HeaderText = "End";
            this.End.Name = "End";
            this.End.ReadOnly = true;
            this.End.Width = 70;
            // 
            // NB
            // 
            this.NB.HeaderText = "Frames";
            this.NB.Name = "NB";
            this.NB.ReadOnly = true;
            this.NB.Width = 70;
            // 
            // ProjectStatus
            // 
            this.ProjectStatus.HeaderText = "Status";
            this.ProjectStatus.Name = "ProjectStatus";
            this.ProjectStatus.ReadOnly = true;
            this.ProjectStatus.Width = 70;
            // 
            // elapsedCol
            // 
            this.elapsedCol.HeaderText = "Elapsed";
            this.elapsedCol.Name = "elapsedCol";
            this.elapsedCol.ReadOnly = true;
            this.elapsedCol.Width = 80;
            // 
            // EstimatedCol
            // 
            this.EstimatedCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EstimatedCol.HeaderText = "Remaining (Est)";
            this.EstimatedCol.Name = "EstimatedCol";
            this.EstimatedCol.ReadOnly = true;
            // 
            // FinishedProjectGrid
            // 
            this.FinishedProjectGrid.AllowDrop = true;
            this.FinishedProjectGrid.AllowUserToAddRows = false;
            this.FinishedProjectGrid.AllowUserToDeleteRows = false;
            this.FinishedProjectGrid.BackgroundColor = System.Drawing.Color.White;
            this.FinishedProjectGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FinishedProjectGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.TimeStarted,
            this.RenderTime});
            this.FinishedProjectGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FinishedProjectGrid.Location = new System.Drawing.Point(0, 0);
            this.FinishedProjectGrid.MultiSelect = false;
            this.FinishedProjectGrid.Name = "FinishedProjectGrid";
            this.FinishedProjectGrid.ReadOnly = true;
            this.FinishedProjectGrid.RowHeadersVisible = false;
            this.FinishedProjectGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.FinishedProjectGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.FinishedProjectGrid.Size = new System.Drawing.Size(870, 185);
            this.FinishedProjectGrid.TabIndex = 1;
            this.FinishedProjectGrid.Tag = "Shows the projects in the queue. Right click to access the context menu.";
            this.FinishedProjectGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FinishedProjectGridMouseClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 40;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Finished Project Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 427;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Total Frames";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Status";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // TimeStarted
            // 
            this.TimeStarted.HeaderText = "Time Started";
            this.TimeStarted.Name = "TimeStarted";
            this.TimeStarted.ReadOnly = true;
            // 
            // RenderTime
            // 
            this.RenderTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.RenderTime.HeaderText = "Render Time";
            this.RenderTime.Name = "RenderTime";
            this.RenderTime.ReadOnly = true;
            // 
            // messages
            // 
            this.messages.Controls.Add(this.messageList);
            this.messages.Location = new System.Drawing.Point(4, 22);
            this.messages.Name = "messages";
            this.messages.Padding = new System.Windows.Forms.Padding(3);
            this.messages.Size = new System.Drawing.Size(876, 617);
            this.messages.TabIndex = 1;
            this.messages.Text = "Messages";
            this.messages.UseVisualStyleBackColor = true;
            // 
            // messageList
            // 
            this.messageList.AllowUserToAddRows = false;
            this.messageList.AllowUserToDeleteRows = false;
            this.messageList.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.messageList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.messageList.BackgroundColor = System.Drawing.Color.White;
            this.messageList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.messageList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colType,
            this.colDate,
            this.colMessage});
            this.messageList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageList.GridColor = System.Drawing.SystemColors.Window;
            this.messageList.Location = new System.Drawing.Point(3, 3);
            this.messageList.Name = "messageList";
            this.messageList.ReadOnly = true;
            this.messageList.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.messageList.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.messageList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.messageList.Size = new System.Drawing.Size(870, 611);
            this.messageList.TabIndex = 0;
            this.messageList.Tag = "Shows the system messages.";
            this.messageList.MouseEnter += new System.EventHandler(this.tool_MouseHover);
            this.messageList.MouseLeave += new System.EventHandler(this.tool_MouseLeave);
            // 
            // colType
            // 
            this.colType.HeaderText = "";
            this.colType.Name = "colType";
            this.colType.ReadOnly = true;
            this.colType.Width = 20;
            // 
            // colDate
            // 
            this.colDate.HeaderText = "Date";
            this.colDate.Name = "colDate";
            this.colDate.ReadOnly = true;
            this.colDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colDate.Width = 150;
            // 
            // colMessage
            // 
            this.colMessage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMessage.HeaderText = "Message";
            this.colMessage.Name = "colMessage";
            this.colMessage.ReadOnly = true;
            this.colMessage.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colMessage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tabRender
            // 
            this.tabRender.Controls.Add(this.splitContainer2);
            this.tabRender.Location = new System.Drawing.Point(4, 22);
            this.tabRender.Name = "tabRender";
            this.tabRender.Padding = new System.Windows.Forms.Padding(3);
            this.tabRender.Size = new System.Drawing.Size(876, 617);
            this.tabRender.TabIndex = 2;
            this.tabRender.Text = "Renders preview";
            this.tabRender.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.textPreviewSpeed);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.btnPlay);
            this.splitContainer2.Panel1.Controls.Add(this.autoRenderLast);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(870, 611);
            this.splitContainer2.SplitterDistance = 35;
            this.splitContainer2.TabIndex = 0;
            // 
            // textPreviewSpeed
            // 
            this.textPreviewSpeed.Location = new System.Drawing.Point(361, 8);
            this.textPreviewSpeed.Name = "textPreviewSpeed";
            this.textPreviewSpeed.Size = new System.Drawing.Size(50, 20);
            this.textPreviewSpeed.TabIndex = 4;
            this.textPreviewSpeed.Text = "10";
            this.textPreviewSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textPreviewSpeed.Leave += new System.EventHandler(this.textPreviewSpeed_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(222, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Preview speed (img./sec.):";
            // 
            // btnPlay
            // 
            this.btnPlay.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnPlay.Image = global::Amleto.Properties.Resources.resultset_next;
            this.btnPlay.Location = new System.Drawing.Point(161, 3);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(31, 29);
            this.btnPlay.TabIndex = 2;
            this.btnPlay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.CheckedChanged += new System.EventHandler(this.btnPlay_CheckedChanged);
            // 
            // autoRenderLast
            // 
            this.autoRenderLast.AutoSize = true;
            this.autoRenderLast.Location = new System.Drawing.Point(9, 10);
            this.autoRenderLast.Name = "autoRenderLast";
            this.autoRenderLast.Size = new System.Drawing.Size(146, 17);
            this.autoRenderLast.TabIndex = 0;
            this.autoRenderLast.Text = "Always display last render";
            this.autoRenderLast.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.renderTree);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.AutoScroll = true;
            this.splitContainer3.Panel2.Controls.Add(this.pictureRender);
            this.splitContainer3.Size = new System.Drawing.Size(870, 572);
            this.splitContainer3.SplitterDistance = 286;
            this.splitContainer3.TabIndex = 1;
            // 
            // renderTree
            // 
            this.renderTree.ContextMenuStrip = this.contextMenuRender;
            this.renderTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderTree.HideSelection = false;
            this.renderTree.Location = new System.Drawing.Point(0, 0);
            this.renderTree.Name = "renderTree";
            this.renderTree.Size = new System.Drawing.Size(282, 568);
            this.renderTree.TabIndex = 0;
            this.renderTree.Tag = "Shows the projects which have some rendered frames.";
            this.renderTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.renderTree_AfterSelect);
            this.renderTree.MouseEnter += new System.EventHandler(this.tool_MouseHover);
            this.renderTree.MouseLeave += new System.EventHandler(this.tool_MouseLeave);
            // 
            // contextMenuRender
            // 
            this.contextMenuRender.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editProjectToolStripMenuItem,
            this.clearListToolStripMenuItem});
            this.contextMenuRender.Name = "contextMenuRender";
            this.contextMenuRender.Size = new System.Drawing.Size(135, 48);
            // 
            // editProjectToolStripMenuItem
            // 
            this.editProjectToolStripMenuItem.Name = "editProjectToolStripMenuItem";
            this.editProjectToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.editProjectToolStripMenuItem.Text = "Edit Project";
            this.editProjectToolStripMenuItem.Click += new System.EventHandler(this.editProjectToolStripMenuItem_Click);
            // 
            // clearListToolStripMenuItem
            // 
            this.clearListToolStripMenuItem.Name = "clearListToolStripMenuItem";
            this.clearListToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.clearListToolStripMenuItem.Text = "Clear List";
            this.clearListToolStripMenuItem.Click += new System.EventHandler(this.clearListToolStripMenuItem_Click);
            // 
            // pictureRender
            // 
            this.pictureRender.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureRender.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureRender.Location = new System.Drawing.Point(0, 0);
            this.pictureRender.Name = "pictureRender";
            this.pictureRender.Size = new System.Drawing.Size(576, 568);
            this.pictureRender.TabIndex = 0;
            this.pictureRender.TabStop = false;
            // 
            // mainMenu
            // 
            this.mainMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.nodesToolStripMenuItem,
            this.jobsToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(884, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // mnuExit
            // 
            this.mnuExit.Image = global::Amleto.Properties.Resources.door_in;
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(92, 22);
            this.mnuExit.Text = "&Exit";
            this.mnuExit.Click += new System.EventHandler(this.MenuExitClick);
            // 
            // nodesToolStripMenuItem
            // 
            this.nodesToolStripMenuItem.Name = "nodesToolStripMenuItem";
            this.nodesToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.nodesToolStripMenuItem.Text = "&Nodes";
            this.nodesToolStripMenuItem.DropDownOpening += new System.EventHandler(this.nodesToolStripMenuItem_DropDownOpening);
            // 
            // jobsToolStripMenuItem
            // 
            this.jobsToolStripMenuItem.Name = "jobsToolStripMenuItem";
            this.jobsToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.jobsToolStripMenuItem.Text = "&Jobs";
            this.jobsToolStripMenuItem.DropDownOpening += new System.EventHandler(this.jobsToolStripMenuItem_DropDownOpening);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOptions});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // mnuOptions
            // 
            this.mnuOptions.Image = global::Amleto.Properties.Resources.cog;
            this.mnuOptions.Name = "mnuOptions";
            this.mnuOptions.Size = new System.Drawing.Size(116, 22);
            this.mnuOptions.Text = "&Options";
            this.mnuOptions.Click += new System.EventHandler(this.MenuOptionsClick);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = global::Amleto.Properties.Resources.comment;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolAddJob,
            this.toolEditJob,
            this.toolDeleteJob,
            this.toolStripSeparator1,
            this.toolPauseNode,
            this.toolResumeNode,
            this.toolTimeActivation,
            this.toolStripSeparator2,
            this.toolViewMessages,
            this.toolStripSeparator3,
            this.toolStopNode});
            this.mainToolStrip.Location = new System.Drawing.Point(3, 24);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(214, 25);
            this.mainToolStrip.TabIndex = 1;
            // 
            // toolAddJob
            // 
            this.toolAddJob.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolAddJob.Image = global::Amleto.Properties.Resources.application_form_add;
            this.toolAddJob.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolAddJob.Name = "toolAddJob";
            this.toolAddJob.Size = new System.Drawing.Size(23, 22);
            this.toolAddJob.Text = "Add Job";
            this.toolAddJob.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            this.toolAddJob.MouseEnter += new System.EventHandler(this.tool_MouseHover);
            this.toolAddJob.MouseLeave += new System.EventHandler(this.tool_MouseLeave);
            // 
            // toolEditJob
            // 
            this.toolEditJob.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolEditJob.Image = global::Amleto.Properties.Resources.application_form_edit;
            this.toolEditJob.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolEditJob.Name = "toolEditJob";
            this.toolEditJob.Size = new System.Drawing.Size(23, 22);
            this.toolEditJob.Text = "Edit Job";
            this.toolEditJob.Click += new System.EventHandler(this.editNodeToolStripMenuItem_Click);
            this.toolEditJob.MouseEnter += new System.EventHandler(this.tool_MouseHover);
            this.toolEditJob.MouseLeave += new System.EventHandler(this.tool_MouseLeave);
            // 
            // toolDeleteJob
            // 
            this.toolDeleteJob.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolDeleteJob.Image = global::Amleto.Properties.Resources.application_form_delete;
            this.toolDeleteJob.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDeleteJob.Name = "toolDeleteJob";
            this.toolDeleteJob.Size = new System.Drawing.Size(23, 22);
            this.toolDeleteJob.Text = "Stop Job";
            this.toolDeleteJob.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            this.toolDeleteJob.MouseEnter += new System.EventHandler(this.tool_MouseHover);
            this.toolDeleteJob.MouseLeave += new System.EventHandler(this.tool_MouseLeave);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolPauseNode
            // 
            this.toolPauseNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolPauseNode.Image = global::Amleto.Properties.Resources.clock_pause;
            this.toolPauseNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolPauseNode.Name = "toolPauseNode";
            this.toolPauseNode.Size = new System.Drawing.Size(23, 22);
            this.toolPauseNode.Text = "Pause Node";
            this.toolPauseNode.Click += new System.EventHandler(this.pauseClientToolStripMenuItem_Click);
            this.toolPauseNode.MouseEnter += new System.EventHandler(this.tool_MouseHover);
            this.toolPauseNode.MouseLeave += new System.EventHandler(this.tool_MouseLeave);
            // 
            // toolResumeNode
            // 
            this.toolResumeNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolResumeNode.Image = global::Amleto.Properties.Resources.clock_go;
            this.toolResumeNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolResumeNode.Name = "toolResumeNode";
            this.toolResumeNode.Size = new System.Drawing.Size(23, 22);
            this.toolResumeNode.Text = "Resume Node";
            this.toolResumeNode.Click += new System.EventHandler(this.resumeClientToolStripMenuItem_Click);
            this.toolResumeNode.MouseEnter += new System.EventHandler(this.tool_MouseHover);
            this.toolResumeNode.MouseLeave += new System.EventHandler(this.tool_MouseLeave);
            // 
            // toolTimeActivation
            // 
            this.toolTimeActivation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolTimeActivation.Image = global::Amleto.Properties.Resources.clock_edit;
            this.toolTimeActivation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolTimeActivation.Name = "toolTimeActivation";
            this.toolTimeActivation.Size = new System.Drawing.Size(23, 22);
            this.toolTimeActivation.Text = "Edit Time Activation";
            this.toolTimeActivation.Click += new System.EventHandler(this.setupActivationTimeToolStripMenuItem_Click);
            this.toolTimeActivation.MouseEnter += new System.EventHandler(this.tool_MouseHover);
            this.toolTimeActivation.MouseLeave += new System.EventHandler(this.tool_MouseLeave);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolViewMessages
            // 
            this.toolViewMessages.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolViewMessages.Image = global::Amleto.Properties.Resources.application_view_list;
            this.toolViewMessages.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolViewMessages.Name = "toolViewMessages";
            this.toolViewMessages.Size = new System.Drawing.Size(23, 22);
            this.toolViewMessages.Text = "Show Node Clients";
            this.toolViewMessages.Click += new System.EventHandler(this.showMessagesToolStripMenuItem_Click);
            this.toolViewMessages.MouseEnter += new System.EventHandler(this.tool_MouseHover);
            this.toolViewMessages.MouseLeave += new System.EventHandler(this.tool_MouseLeave);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStopNode
            // 
            this.toolStopNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStopNode.Image = global::Amleto.Properties.Resources.bomb;
            this.toolStopNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStopNode.Name = "toolStopNode";
            this.toolStopNode.Size = new System.Drawing.Size(23, 22);
            this.toolStopNode.Text = "Stop Node";
            this.toolStopNode.Click += new System.EventHandler(this.closeNodeToolStripMenuItem_Click);
            this.toolStopNode.MouseEnter += new System.EventHandler(this.tool_MouseHover);
            this.toolStopNode.MouseLeave += new System.EventHandler(this.tool_MouseLeave);
            // 
            // contextMenuNodes
            // 
            this.contextMenuNodes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSendKill,
            this.showMessagesToolStripMenuItem,
            this.resendConfigToolStripMenuItem,
            this.configToolStripMenuItem,
            this.renderPriorityToolStripMenuItem,
            this.setupActivationTimeToolStripMenuItem,
            this.pauseClientToolStripMenuItem,
            this.resumeClientToolStripMenuItem,
            this.closeNodeToolStripMenuItem,
            this.stopAllNodesToolStripMenuItem,
            this.cleanClientContentDirectoryToolStripMenuItem,
            this.cleanAllClientsContentDirectoryToolStripMenuItem,
            this.cleanClientOutputDirectoryToolStripMenuItem,
            this.cleanAllClientsOutputDirectoryToolStripMenuItem});
            this.contextMenuNodes.Name = "contextMenuNodes";
            this.contextMenuNodes.Size = new System.Drawing.Size(251, 312);
            // 
            // mnuSendKill
            // 
            this.mnuSendKill.Name = "mnuSendKill";
            this.mnuSendKill.Size = new System.Drawing.Size(250, 22);
            this.mnuSendKill.Text = "Stop job";
            this.mnuSendKill.Click += new System.EventHandler(this.mnuSendKill_Click);
            // 
            // showMessagesToolStripMenuItem
            // 
            this.showMessagesToolStripMenuItem.Image = global::Amleto.Properties.Resources.application_view_list;
            this.showMessagesToolStripMenuItem.Name = "showMessagesToolStripMenuItem";
            this.showMessagesToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.showMessagesToolStripMenuItem.Text = "Show messages";
            this.showMessagesToolStripMenuItem.Click += new System.EventHandler(this.showMessagesToolStripMenuItem_Click);
            // 
            // resendConfigToolStripMenuItem
            // 
            this.resendConfigToolStripMenuItem.Name = "resendConfigToolStripMenuItem";
            this.resendConfigToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.resendConfigToolStripMenuItem.Text = "Resend config";
            this.resendConfigToolStripMenuItem.Click += new System.EventHandler(this.resendConfigToolStripMenuItem_Click);
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.configToolStripMenuItem.Text = "Config";
            // 
            // renderPriorityToolStripMenuItem
            // 
            this.renderPriorityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.highToolStripMenuItem,
            this.aboveNormalToolStripMenuItem,
            this.normalToolStripMenuItem,
            this.belowNormalToolStripMenuItem,
            this.lowToolStripMenuItem});
            this.renderPriorityToolStripMenuItem.Name = "renderPriorityToolStripMenuItem";
            this.renderPriorityToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.renderPriorityToolStripMenuItem.Text = "Render priority";
            // 
            // highToolStripMenuItem
            // 
            this.highToolStripMenuItem.Name = "highToolStripMenuItem";
            this.highToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.highToolStripMenuItem.Text = "High";
            this.highToolStripMenuItem.Click += new System.EventHandler(this.changePriortyStripMenuItem_Click);
            // 
            // aboveNormalToolStripMenuItem
            // 
            this.aboveNormalToolStripMenuItem.Name = "aboveNormalToolStripMenuItem";
            this.aboveNormalToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.aboveNormalToolStripMenuItem.Text = "AboveNormal";
            this.aboveNormalToolStripMenuItem.Click += new System.EventHandler(this.changePriortyStripMenuItem_Click);
            // 
            // normalToolStripMenuItem
            // 
            this.normalToolStripMenuItem.Name = "normalToolStripMenuItem";
            this.normalToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.normalToolStripMenuItem.Text = "Normal";
            this.normalToolStripMenuItem.Click += new System.EventHandler(this.changePriortyStripMenuItem_Click);
            // 
            // belowNormalToolStripMenuItem
            // 
            this.belowNormalToolStripMenuItem.Name = "belowNormalToolStripMenuItem";
            this.belowNormalToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.belowNormalToolStripMenuItem.Text = "BelowNormal";
            this.belowNormalToolStripMenuItem.Click += new System.EventHandler(this.changePriortyStripMenuItem_Click);
            // 
            // lowToolStripMenuItem
            // 
            this.lowToolStripMenuItem.Name = "lowToolStripMenuItem";
            this.lowToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.lowToolStripMenuItem.Text = "Idle";
            this.lowToolStripMenuItem.Click += new System.EventHandler(this.changePriortyStripMenuItem_Click);
            // 
            // setupActivationTimeToolStripMenuItem
            // 
            this.setupActivationTimeToolStripMenuItem.Image = global::Amleto.Properties.Resources.clock_edit;
            this.setupActivationTimeToolStripMenuItem.Name = "setupActivationTimeToolStripMenuItem";
            this.setupActivationTimeToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.setupActivationTimeToolStripMenuItem.Text = "Setup activation time";
            this.setupActivationTimeToolStripMenuItem.Click += new System.EventHandler(this.setupActivationTimeToolStripMenuItem_Click);
            // 
            // pauseClientToolStripMenuItem
            // 
            this.pauseClientToolStripMenuItem.Image = global::Amleto.Properties.Resources.clock_pause;
            this.pauseClientToolStripMenuItem.Name = "pauseClientToolStripMenuItem";
            this.pauseClientToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.pauseClientToolStripMenuItem.Text = "Pause";
            this.pauseClientToolStripMenuItem.Click += new System.EventHandler(this.pauseClientToolStripMenuItem_Click);
            // 
            // resumeClientToolStripMenuItem
            // 
            this.resumeClientToolStripMenuItem.Image = global::Amleto.Properties.Resources.clock_go;
            this.resumeClientToolStripMenuItem.Name = "resumeClientToolStripMenuItem";
            this.resumeClientToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.resumeClientToolStripMenuItem.Text = "Resume";
            this.resumeClientToolStripMenuItem.Click += new System.EventHandler(this.resumeClientToolStripMenuItem_Click);
            // 
            // closeNodeToolStripMenuItem
            // 
            this.closeNodeToolStripMenuItem.Image = global::Amleto.Properties.Resources.bomb;
            this.closeNodeToolStripMenuItem.Name = "closeNodeToolStripMenuItem";
            this.closeNodeToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.closeNodeToolStripMenuItem.Text = "Stop Node";
            this.closeNodeToolStripMenuItem.Click += new System.EventHandler(this.closeNodeToolStripMenuItem_Click);
            // 
            // stopAllNodesToolStripMenuItem
            // 
            this.stopAllNodesToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.stopAllNodesToolStripMenuItem.Name = "stopAllNodesToolStripMenuItem";
            this.stopAllNodesToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.stopAllNodesToolStripMenuItem.Text = "Stop All Nodes";
            this.stopAllNodesToolStripMenuItem.Click += new System.EventHandler(this.stopAllNodesToolStripMenuItem_Click);
            // 
            // cleanClientContentDirectoryToolStripMenuItem
            // 
            this.cleanClientContentDirectoryToolStripMenuItem.Name = "cleanClientContentDirectoryToolStripMenuItem";
            this.cleanClientContentDirectoryToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.cleanClientContentDirectoryToolStripMenuItem.Text = "Clean client content directory";
            this.cleanClientContentDirectoryToolStripMenuItem.Click += new System.EventHandler(this.cleanClientContentDirectoryToolStripMenuItem_Click);
            // 
            // cleanAllClientsContentDirectoryToolStripMenuItem
            // 
            this.cleanAllClientsContentDirectoryToolStripMenuItem.Name = "cleanAllClientsContentDirectoryToolStripMenuItem";
            this.cleanAllClientsContentDirectoryToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.cleanAllClientsContentDirectoryToolStripMenuItem.Text = "Clean all clients content directory";
            this.cleanAllClientsContentDirectoryToolStripMenuItem.Click += new System.EventHandler(this.cleanAllClientsContentDirectoryToolStripMenuItem_Click);
            // 
            // cleanClientOutputDirectoryToolStripMenuItem
            // 
            this.cleanClientOutputDirectoryToolStripMenuItem.Name = "cleanClientOutputDirectoryToolStripMenuItem";
            this.cleanClientOutputDirectoryToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.cleanClientOutputDirectoryToolStripMenuItem.Text = "Clean client output directory";
            this.cleanClientOutputDirectoryToolStripMenuItem.Click += new System.EventHandler(this.cleanClientOutputDirectoryToolStripMenuItem_Click);
            // 
            // cleanAllClientsOutputDirectoryToolStripMenuItem
            // 
            this.cleanAllClientsOutputDirectoryToolStripMenuItem.Name = "cleanAllClientsOutputDirectoryToolStripMenuItem";
            this.cleanAllClientsOutputDirectoryToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.cleanAllClientsOutputDirectoryToolStripMenuItem.Text = "Clean all clients output directory";
            this.cleanAllClientsOutputDirectoryToolStripMenuItem.Click += new System.EventHandler(this.cleanAllClientsOutputDirectoryToolStripMenuItem_Click);
            // 
            // contextMenuProjects
            // 
            this.contextMenuProjects.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.editNodeToolStripMenuItem,
            this.pauseToolStripMenuItem,
            this.resumeToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.contextMenuProjects.Name = "contextMenuProjects";
            this.contextMenuProjects.Size = new System.Drawing.Size(158, 114);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Image = global::Amleto.Properties.Resources.application_form_add;
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.addToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // editNodeToolStripMenuItem
            // 
            this.editNodeToolStripMenuItem.Image = global::Amleto.Properties.Resources.application_form_edit;
            this.editNodeToolStripMenuItem.Name = "editNodeToolStripMenuItem";
            this.editNodeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.editNodeToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.editNodeToolStripMenuItem.Text = "Edit";
            this.editNodeToolStripMenuItem.Click += new System.EventHandler(this.editNodeToolStripMenuItem_Click);
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.pauseToolStripMenuItem.Text = "Pause";
            this.pauseToolStripMenuItem.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
            // 
            // resumeToolStripMenuItem
            // 
            this.resumeToolStripMenuItem.Name = "resumeToolStripMenuItem";
            this.resumeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.resumeToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.resumeToolStripMenuItem.Text = "Resume";
            this.resumeToolStripMenuItem.Click += new System.EventHandler(this.resumeToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Image = global::Amleto.Properties.Resources.application_form_delete;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // playTimer
            // 
            this.playTimer.Tick += new System.EventHandler(this.playTimer_Tick);
            // 
            // refreshTimer
            // 
            this.refreshTimer.Enabled = true;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // iconList
            // 
            this.iconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iconList.ImageStream")));
            this.iconList.TransparentColor = System.Drawing.Color.Transparent;
            this.iconList.Images.SetKeyName(0, "info.ico");
            this.iconList.Images.SetKeyName(1, "error.ico");
            this.iconList.Images.SetKeyName(2, "gears4.ico");
            this.iconList.Images.SetKeyName(3, "ok.ico");
            this.iconList.Images.SetKeyName(4, "small_lw.ico");
            this.iconList.Images.SetKeyName(5, "wait.ico");
            this.iconList.Images.SetKeyName(6, "zzz.ico");
            // 
            // contextMenuFinished
            // 
            this.contextMenuFinished.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FinishedMenuEdit,
            this.FinishedMenuClear,
            this.FinishedMenuClearAll});
            this.contextMenuFinished.Name = "contextMenuProjects";
            this.contextMenuFinished.Size = new System.Drawing.Size(164, 70);
            // 
            // FinishedMenuEdit
            // 
            this.FinishedMenuEdit.Image = global::Amleto.Properties.Resources.application_form_edit;
            this.FinishedMenuEdit.Name = "FinishedMenuEdit";
            this.FinishedMenuEdit.Size = new System.Drawing.Size(163, 22);
            this.FinishedMenuEdit.Text = "Edit and Restart";
            this.FinishedMenuEdit.Click += new System.EventHandler(this.FinishedMenuEditClick);
            // 
            // FinishedMenuClear
            // 
            this.FinishedMenuClear.Name = "FinishedMenuClear";
            this.FinishedMenuClear.Size = new System.Drawing.Size(163, 22);
            this.FinishedMenuClear.Text = "Clear Project";
            this.FinishedMenuClear.Click += new System.EventHandler(this.FinishedMenuClearClick);
            // 
            // FinishedMenuClearAll
            // 
            this.FinishedMenuClearAll.Name = "FinishedMenuClearAll";
            this.FinishedMenuClearAll.Size = new System.Drawing.Size(163, 22);
            this.FinishedMenuClearAll.Text = "Clear All Projects";
            this.FinishedMenuClearAll.Click += new System.EventHandler(this.FinishedMenuClearAllClick);
            // 
            // HostId
            // 
            this.HostId.HeaderText = "ID";
            this.HostId.Name = "HostId";
            this.HostId.ReadOnly = true;
            this.HostId.Width = 50;
            // 
            // hostName
            // 
            this.hostName.HeaderText = "Host Name";
            this.hostName.Name = "hostName";
            this.hostName.ReadOnly = true;
            this.hostName.Width = 200;
            // 
            // HostInstance
            // 
            this.HostInstance.FillWeight = 95F;
            this.HostInstance.HeaderText = "Instance";
            this.HostInstance.Name = "HostInstance";
            this.HostInstance.ReadOnly = true;
            this.HostInstance.Width = 55;
            // 
            // colCurConfig
            // 
            this.colCurConfig.HeaderText = "Config";
            this.colCurConfig.Name = "colCurConfig";
            this.colCurConfig.Width = 135;
            // 
            // hostStatus
            // 
            this.hostStatus.HeaderText = "Status";
            this.hostStatus.Name = "hostStatus";
            this.hostStatus.Width = 50;
            // 
            // LastRender
            // 
            this.LastRender.HeaderText = "Render Time";
            this.LastRender.Name = "LastRender";
            // 
            // hostJob
            // 
            this.hostJob.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.hostJob.HeaderText = "Current Job";
            this.hostJob.Name = "hostJob";
            this.hostJob.ReadOnly = true;
            // 
            // ServerWin
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 714);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.status);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "ServerWin";
            this.Text = "Amleto Server - 3.1.1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerWinFormClosing);
            this.Shown += new System.EventHandler(this.ServerWin_Shown);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ServerWin_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.ServerWin_DragEnter);
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.mainTabs.ResumeLayout(false);
            this.clientStatus.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ClientStatusGrid)).EndInit();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ActiveProjectGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FinishedProjectGrid)).EndInit();
            this.messages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.messageList)).EndInit();
            this.tabRender.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.contextMenuRender.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureRender)).EndInit();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.contextMenuNodes.ResumeLayout(false);
            this.contextMenuProjects.ResumeLayout(false);
            this.contextMenuFinished.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem jobsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuOptions;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.TabControl mainTabs;
        private System.Windows.Forms.TabPage clientStatus;
        private System.Windows.Forms.TabPage messages;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView ClientStatusGrid;
        private System.Windows.Forms.DataGridView ActiveProjectGrid;
        private System.Windows.Forms.TabPage tabRender;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TreeView renderTree;
        private System.Windows.Forms.PictureBox pictureRender;
        private System.Windows.Forms.CheckBox autoRenderLast;
        private System.Windows.Forms.CheckBox btnPlay;
        private System.Windows.Forms.Timer playTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textPreviewSpeed;
        private System.Windows.Forms.ContextMenuStrip contextMenuProjects;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resumeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuNodes;
        private System.Windows.Forms.ToolStripMenuItem mnuSendKill;
        private System.Windows.Forms.ToolStripMenuItem showMessagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renderPriorityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem highToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolAddJob;
        private System.Windows.Forms.ToolStripMenuItem aboveNormalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem belowNormalToolStripMenuItem;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.ImageList iconList;
        private System.Windows.Forms.DataGridView messageList;
        private System.Windows.Forms.DataGridViewImageColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMessage;
        private System.Windows.Forms.ToolStripMenuItem pauseClientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resumeClientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopAllNodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setupActivationTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolEditJob;
        private System.Windows.Forms.ToolStripButton toolDeleteJob;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolPauseNode;
        private System.Windows.Forms.ToolStripButton toolResumeNode;
        private System.Windows.Forms.ToolStripButton toolTimeActivation;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolViewMessages;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStopNode;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus;
        private System.Windows.Forms.ContextMenuStrip contextMenuRender;
        private System.Windows.Forms.ToolStripMenuItem editProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resendConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cleanClientContentDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cleanClientOutputDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cleanAllClientsContentDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cleanAllClientsOutputDirectoryToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.DataGridView FinishedProjectGrid;
        private System.Windows.Forms.ContextMenuStrip contextMenuFinished;
        private System.Windows.Forms.ToolStripMenuItem FinishedMenuEdit;
        private System.Windows.Forms.ToolStripMenuItem FinishedMenuClear;
        private System.Windows.Forms.ToolStripMenuItem FinishedMenuClearAll;
        private System.Windows.Forms.DataGridViewTextBoxColumn projId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Project;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOwner;
        private System.Windows.Forms.DataGridViewTextBoxColumn Start;
        private System.Windows.Forms.DataGridViewTextBoxColumn End;
        private System.Windows.Forms.DataGridViewTextBoxColumn NB;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn elapsedCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstimatedCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeStarted;
        private System.Windows.Forms.DataGridViewTextBoxColumn RenderTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn HostId;
        private System.Windows.Forms.DataGridViewTextBoxColumn hostName;
        private System.Windows.Forms.DataGridViewTextBoxColumn HostInstance;
        private System.Windows.Forms.DataGridViewComboBoxColumn colCurConfig;
        private System.Windows.Forms.DataGridViewImageColumn hostStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastRender;
        private System.Windows.Forms.DataGridViewTextBoxColumn hostJob;
    }
}

