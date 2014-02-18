using System;
using System.IO;
using System.Threading;
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
            string logPath = Path.Combine(Paths.GetLocalPath(), "Logs");
            Directory.CreateDirectory(logPath);
            logPath = Path.Combine(logPath, logFile);
            FileTarget target = LogManager.Configuration.FindTargetByName("logFile") as FileTarget;
            if (target != null)
                target.FileName = logPath;

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = "'";

            Application.Run(new ClientWin());
        }
    }
}