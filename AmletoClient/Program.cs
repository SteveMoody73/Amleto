using System;
using System.IO;
using System.Windows.Forms;
using NLog;
using NLog.Targets;
using RemoteExecution;

namespace AmletoClient
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

            string logFile = String.Format("AmletoClient_{0:yyyy_MM_dd}.log", DateTime.Now);
            String logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Amleto");
            logPath = Path.Combine(logPath, "Logs");
            logPath = Path.Combine(logPath, logFile);
            FileTarget target = LogManager.Configuration.FindTargetByName("logFile") as FileTarget;
            if (target != null)
                target.FileName = logPath;

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = "'";

            Application.Run(new ClientWin());
        }
    }
}