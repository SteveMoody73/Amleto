using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using RemoteExecution;
using System.IO;

namespace AmletoClient
{
    public partial class ClientSetupWin : Form
    {
        public ClientSetupWin()
        {
            InitializeComponent();
            autoSearch.Checked = ClientServices.AutoServerFinder;
            serverAddress.Text = ClientServices.ServerHost;
            serverPort.Text = "" + ClientServices.ServerPort;
            localScratch.Text = ClientServices.ClientDir;
            logFile.Text = ClientServices.LogFile;
            nbThreads.SelectedItem = "" + ClientServices.NumThreads;
            memorySegment.Text = "" + ClientServices.MemorySegment;

            renderPriority.Items.AddRange(new[] { "High", "AboveNormal", "Normal", "BelowNormal", "Idle" });
            for (int i = 0; i < renderPriority.Items.Count; i++)
            {
                if ((string)renderPriority.Items[i] == ClientServices.RenderPriority.ToString())
                {
                    renderPriority.SelectedIndex = i;
                    break;
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ClientServices.AutoServerFinder = autoSearch.Checked;
            ClientServices.ServerHost = serverAddress.Text;
            ClientServices.ServerPort = Convert.ToInt32(serverPort.Text);
            ClientServices.ClientDir = localScratch.Text;
            ClientServices.RenderPriority = (ProcessPriorityClass)Enum.Parse(typeof(ProcessPriorityClass), (string)renderPriority.SelectedItem);
            try
            {
                if (logFile.Text != "" && !Directory.GetParent(logFile.Text).Exists)
                    Directory.GetParent(logFile.Text).Create();
            }
			catch (Exception ex)
			{
				Debug.WriteLine("Error creating log file " + ex);
			}


            ClientServices.LogFile = logFile.Text;
            ClientServices.NumThreads = Convert.ToInt32((string)nbThreads.SelectedItem);
            ClientServices.MemorySegment = Convert.ToInt32(memorySegment.Text);
            ClientServices.ChangePriority();
            ClientServices.SaveSettings();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void autoSearch_CheckedChanged(object sender, EventArgs e)
        {
            lblName.Enabled = !autoSearch.Checked;
            lblPort.Enabled = !autoSearch.Checked;
            serverAddress.Enabled = !autoSearch.Checked;
            serverPort.Enabled = !autoSearch.Checked;
        }

        private void ClientSetupWin_Load(object sender, EventArgs e)
        {

        }

        private void serverPort_Validating(object sender, CancelEventArgs e)
        {
            int a;
            if (int.TryParse(((TextBox)sender).Text, out a) == false)
            {
                errorProvider.SetError((TextBox)sender, "Must be a number");
                e.Cancel = true;
                return;
            }
            if (a < 1 || a > 62000)
            {
                errorProvider.SetError((TextBox)sender, "Must be a number between 1 and 62000");
                e.Cancel = true;
                return;
            }
            errorProvider.SetError((TextBox)sender, "");
        }

        private void memorySegment_Validating(object sender, CancelEventArgs e)
        {
            int a;
            if (int.TryParse(((TextBox)sender).Text, out a) == false)
            {
                errorProvider.SetError((TextBox)sender, "Must be a number");
                e.Cancel = true;
                return;
            }
            if (a < 16 || a > 32000)
            {
                errorProvider.SetError((TextBox)sender, "Must be a number between 16 and 32000");
                e.Cancel = true;
                return;
            }
            errorProvider.SetError((TextBox)sender, "");
        }
    }
}