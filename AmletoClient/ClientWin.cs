using System;
using System.Reflection;
using System.Windows.Forms;
using RemoteExecution;

namespace AmletoClient
{
    public partial class ClientWin : Form
    {
        private const bool IsBeta = true;

        private delegate void VoidStringParamFunction(string msg);

        private VoidStringParamFunction _messageAdder;
        private ClientServices _clientService = new ClientServices();

        public ClientWin()
        {
            InitializeComponent();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuSetup_Click(object sender, EventArgs e)
        {
            ClientSetupWin dlg = new ClientSetupWin();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ClientServices.Shutdown();
                _clientService.StartService();
            }
            dlg.Dispose();
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            AboutClient about = new AboutClient();
            about.ShowDialog();
            about.Dispose();
        }

        private void AddMessage(string msg)
        {
            if (msg != null)
                BeginInvoke(_messageAdder, new object[] {msg});
        }

        private void DoAddMessage(string msg)
        {
            if (msg == null)
                return;
            lock (_messageAdder)
            {
                string[] p = msg.Split('|');
                DataGridViewRow row = new DataGridViewRow();
                if (p.Length == 1)
                {
                    DataGridViewCell cell = new DataGridViewImageCell();
                    cell.Value = iconList.Images[0];
                    row.Cells.Add(cell);
                    cell = new DataGridViewTextBoxCell();
                    cell.Value = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                    row.Cells.Add(cell);
                    cell = new DataGridViewTextBoxCell();
                    cell.Value = p[0];
                    row.Cells.Add(cell);
                }
                else
                {
                    DataGridViewCell cell = new DataGridViewImageCell();
                    cell.Value = iconList.Images[Convert.ToInt32(p[0])];
                    row.Cells.Add(cell);
                    cell = new DataGridViewTextBoxCell();
                    cell.Value = p[1];
                    row.Cells.Add(cell);
                    cell = new DataGridViewTextBoxCell();
                    cell.Value = p[2];
                    row.Cells.Add(cell);
                }

                if (messageList.SelectedRows.Count > 0)
                    messageList.SelectedRows[0].Selected = false;
                while (messageList.Rows.Count > 3000)
                    messageList.Rows.RemoveAt(0);

                row.Height = 17;
                messageList.Rows.Add(row);
                row.Selected = true;
                messageList.FirstDisplayedScrollingRowIndex = messageList.Rows.Count - 1;
            }
        }

        private void ClientWin_Load(object sender, EventArgs e)
        {
            Text = "Amleto Client " + Assembly.GetExecutingAssembly().GetName().Version;
            if (IsBeta)
                Text = Text + " (Beta)";

            _messageAdder = DoAddMessage;
            ClientServices.MessageConsumer += AddMessage;
            _clientService.StartService();
        }

        private void ClientWin_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClientServices.Shutdown();
        }
    }
}