using System;
using System.Windows.Forms;

namespace Amleto
{
    public partial class PromptNewConfigWin : Form
    {
        public string ConfigName = "";

        public PromptNewConfigWin()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ConfigName = textConfigName.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}