using System;
using System.Windows.Forms;

namespace Amleto
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            }

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = "'";
            
            Application.Run(new ServerWin());
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message + "\n\r" + e.Exception.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}