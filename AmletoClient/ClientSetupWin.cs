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
            autoSearch.Checked = ClientServices.Settings.AutoServerFinder;
            serverAddress.Text = ClientServices.Settings.ServerHost;
            serverPort.Text = ClientServices.Settings.ServerPort.ToString();
            localScratch.Text = ClientServices.Settings.ClientDir;
            logFile.Text = ClientServices.Settings.LogFile;
            if (ClientServices.Settings.NumThreads == 0)
                nbThreads.SelectedItem = "Automatic";
            else
                nbThreads.SelectedItem = ClientServices.Settings.NumThreads.ToString();
            memorySegment.Text = ClientServices.Settings.MemorySegment.ToString();
            SaveLog.Checked = ClientServices.Settings.SaveToLog;

            renderPriority.Items.AddRange(new[] { "High", "AboveNormal", "Normal", "BelowNormal", "Idle" });
            for (int i = 0; i < renderPriority.Items.Count; i++)
            {
                if ((string)renderPriority.Items[i] == ClientServices.Settings.RenderPriority.ToString())
                {
                    renderPriority.SelectedIndex = i;
                    break;
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ClientServices.Settings.AutoServerFinder = autoSearch.Checked;
            ClientServices.Settings.ServerHost = serverAddress.Text;
            ClientServices.Settings.ServerPort = Convert.ToInt32(serverPort.Text);
            ClientServices.Settings.ClientDir = localScratch.Text;
            ClientServices.Settings.RenderPriority = (ProcessPriorityClass)Enum.Parse(typeof(ProcessPriorityClass), (string)renderPriority.SelectedItem);
            
            try
            {
                if (logFile.Text != "" && !Directory.GetParent(logFile.Text).Exists)
                    Directory.GetParent(logFile.Text).Create();
            }
			catch (Exception ex)
			{
                Tracer.Exception(ex);
            }


            ClientServices.Settings.LogFile = logFile.Text;
            ClientServices.Settings.SaveToLog = SaveLog.Checked;
            
            if ((string) nbThreads.SelectedItem == "Automatic")
                ClientServices.Settings.NumThreads = 0;
            else
                ClientServices.Settings.NumThreads = Convert.ToInt32((string)nbThreads.SelectedItem);
            
            ClientServices.Settings.MemorySegment = Convert.ToInt32(memorySegment.Text);
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