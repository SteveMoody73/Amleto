namespace Amleto
{
    partial class ImageViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageViewer));
            this.ImageView = new Cyotek.Windows.Forms.ImageBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ZoomLevels = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ZoomIn = new System.Windows.Forms.ToolStripButton();
            this.ZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ActualSize = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ImageView
            // 
            this.ImageView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImageView.Location = new System.Drawing.Point(0, 25);
            this.ImageView.Name = "ImageView";
            this.ImageView.Size = new System.Drawing.Size(973, 528);
            this.ImageView.TabIndex = 3;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ZoomLevels,
            this.toolStripSeparator2,
            this.ZoomIn,
            this.ZoomOut,
            this.toolStripSeparator1,
            this.ActualSize});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(973, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ZoomLevels
            // 
            this.ZoomLevels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ZoomLevels.Name = "ZoomLevels";
            this.ZoomLevels.Size = new System.Drawing.Size(140, 25);
            this.ZoomLevels.ToolTipText = "Select Zoom level";
            this.ZoomLevels.SelectedIndexChanged += new System.EventHandler(this.ZoomLevelsSelectedIndexChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // ZoomIn
            // 
            this.ZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomIn.Image = global::Amleto.Properties.Resources.magnifier_zoom_in;
            this.ZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomIn.Name = "ZoomIn";
            this.ZoomIn.Size = new System.Drawing.Size(23, 22);
            this.ZoomIn.Text = "Zoom In";
            this.ZoomIn.Click += new System.EventHandler(this.ZoomInClick);
            // 
            // ZoomOut
            // 
            this.ZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomOut.Image = global::Amleto.Properties.Resources.magnifier_zoom_out;
            this.ZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomOut.Name = "ZoomOut";
            this.ZoomOut.Size = new System.Drawing.Size(23, 22);
            this.ZoomOut.Text = "Zoom Out";
            this.ZoomOut.Click += new System.EventHandler(this.ZoomOutClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ActualSize
            // 
            this.ActualSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ActualSize.Image = global::Amleto.Properties.Resources.image_resize_actual;
            this.ActualSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ActualSize.Name = "ActualSize";
            this.ActualSize.Size = new System.Drawing.Size(23, 22);
            this.ActualSize.Text = "Actual Size";
            this.ActualSize.Click += new System.EventHandler(this.ActualSizeClick);
            // 
            // ImageViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 553);
            this.Controls.Add(this.ImageView);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImageViewer";
            this.Text = "ImageViewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageViewerFormClosing);
            this.Load += new System.EventHandler(this.ImageViewerLoad);
            this.Shown += new System.EventHandler(this.ImageViewerShown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Cyotek.Windows.Forms.ImageBox ImageView;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox ZoomLevels;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ZoomIn;
        private System.Windows.Forms.ToolStripButton ZoomOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ActualSize;
    }
}