namespace AmletoClientService
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
            this.serviceProcessAmletoClientInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceAmletoClientInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceProcessAmletoClientInstaller
            // 
            this.serviceProcessAmletoClientInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessAmletoClientInstaller.Password = null;
            this.serviceProcessAmletoClientInstaller.Username = null;
            // 
            // serviceAmletoClientInstaller
            // 
            this.serviceAmletoClientInstaller.Description = "Render node service for the Amleto distributed rendering solution.";
            this.serviceAmletoClientInstaller.DisplayName = "AmletoClient";
            this.serviceAmletoClientInstaller.ServiceName = "AmletoClient";
            this.serviceAmletoClientInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessAmletoClientInstaller,
            this.serviceAmletoClientInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller serviceProcessAmletoClientInstaller;
        private System.ServiceProcess.ServiceInstaller serviceAmletoClientInstaller;
    }
}