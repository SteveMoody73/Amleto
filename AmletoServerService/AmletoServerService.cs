using System;
using System.ServiceProcess;
using RemoteExecution;

namespace AmletoServerService
{
    public partial class AmletoServerService : ServiceBase
    {
        MasterServer _masterServer = new MasterServer(Environment.UserName);

        public AmletoServerService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //System.Diagnostics.Debugger.Launch();
            _masterServer.RestoreSettings();
            _masterServer.Startup();
        }

        protected override void OnStop()
        {
            _masterServer.Shutdown();
        }
    }
}
