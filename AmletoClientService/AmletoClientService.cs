using System.ServiceProcess;
using RemoteExecution;

namespace AmletoClientService
{
    public partial class AmletoClientService : ServiceBase
    {
        ClientServices _clientService = new ClientServices();

        public AmletoClientService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _clientService.StartService();
        }

        protected override void OnStop()
        {
            ClientServices.Shutdown();
        }
    }
}
