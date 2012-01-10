namespace AmletoServerService
{
    partial class ProjectInstaller
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.serviceProcessServerInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceServerInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceProcessServerInstaller
            // 
            this.serviceProcessServerInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessServerInstaller.Password = null;
            this.serviceProcessServerInstaller.Username = null;
            // 
            // serviceServerInstaller
            // 
            this.serviceServerInstaller.Description = "Node controller and job dispatcher for the Amleto distributed rendering solution." +
                "";
            this.serviceServerInstaller.DisplayName = "Amleto Server";
            this.serviceServerInstaller.ServiceName = "Amleto Server";
            this.serviceServerInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessServerInstaller,
            this.serviceServerInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller serviceProcessServerInstaller;
        private System.ServiceProcess.ServiceInstaller serviceServerInstaller;
    }
}