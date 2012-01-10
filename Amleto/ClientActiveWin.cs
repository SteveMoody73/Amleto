using System;
using System.Windows.Forms;

namespace Amleto
{
    public partial class ClientActiveWin : Form
    {
        CheckBox[] _hours;
        public string ActiveHours = "YYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY";

        public ClientActiveWin()
        {
            InitializeComponent();
            _hours = new CheckBox[] { h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16, h17, h18, h19, h20, h21, h22, h23, h24, h25, h26, h27, h28, h29, h30, h31, h32, h33, h34, h35, h36, h37, h38, h39, h40, h41, h42, h43, h44, h45, h46, h47, h48 };

        }

        private void ClientActiveWinLoad(object sender, EventArgs e)
        {
            for (int i = 0; i < 48; i++)
            {
                if (ActiveHours[i] == 'N')
                    _hours[i].Checked = false;
                else
                    _hours[i].Checked = true;
            }
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            ActiveHours="";
            for (int i = 0; i < 48; i++)
                ActiveHours += _hours[i].Checked ? 'Y' : 'N';

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}