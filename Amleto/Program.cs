using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using NLog;
using NLog.Targets;
using RemoteExecution;

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

            string logFile = String.Format("AmletoServer_{0:yyyy_MM_dd}.log", DateTime.Now);
            String logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Amleto");
            logPath = Path.Combine(logPath, "Logs");
            logPath = Path.Combine(logPath, logFile);
            FileTarget target = LogManager.Configuration.FindTargetByName("logFile") as FileTarget;
            if (target != null)
                target.FileName = logPath;

            if (!System.Diagnostics.Debugger.IsAttached)
            {
                Application.ThreadException += Application_ThreadException;
            }

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = "'";
            
            Application.Run(new ServerWin());
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message + "\n\r" + e.Exception.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}