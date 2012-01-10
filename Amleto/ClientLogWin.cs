using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Amleto
{
    public partial class ClientLogWin : Form
    {
        List<string> _log;

        public ClientLogWin(List<string> log)
        {
            InitializeComponent();
            _log = log;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ClientLogWin_Load(object sender, EventArgs e)
        {
            foreach (string msg in _log)
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
                row.Height = 17;
                messageList.Rows.Add(row);
                messageList.FirstDisplayedScrollingRowIndex = messageList.Rows.Count - 1;
            }
        }
    }
}