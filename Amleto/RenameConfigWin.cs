using System;
using System.Windows.Forms;

namespace Amleto
{
    public partial class RenameConfigWin : Form
    {
        public RenameConfigWin()
        {
            InitializeComponent();
        }

        public string ConfigName
        {
            get { return configName.Text; }
            set { configName.Text = value; }
        }

        private void configName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == Convert.ToChar(Keys.Enter) || e.KeyValue == Convert.ToChar(Keys.Return))
                DialogResult = DialogResult.OK;
        }
    }
}
