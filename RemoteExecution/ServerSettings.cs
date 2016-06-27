using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace RemoteExecution
{
    [Serializable]
    public class ServerSettings
    {
        [XmlIgnore]
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public Size WinSize { get; set; }
        public string ToEmail { get; set; }
        public string SubjectOk { get; set; }
        public string SubjectNotOk { get; set; }
        public bool SendEmail { get; set; }
        public bool EmailIncludeActivity { get; set; }
        public int ViewerWidth { get; set; }
        public int ViewerHeight { get; set; }

        public int Port { get; set; }
        public bool AutoOfferPort { get; set; }
        public string ServerLogFile { get; set; }
        public bool ServerLogEnable { get; set; }
        public string EmailFrom { get; set; }
        public string SMTPServer { get; set; }
        public string SMTPUsername { get; set; }
        public string SMTPPassword { get; set; }
        public bool OfferWeb { get; set; }
        public int OfferWebPort { get; set; }
        public int RenderBlocks { get; set; }
        public int LightwaveConfigs { get; set; }

        [XmlArray]
        public List<ConfigSet> Configs { get; set; }
        [XmlArray]
        public List<MapDrive> MappedDrives { get; set; }
        [XmlArray]
        public List<string> StrippedMaster { get; set; }

        public static string SettingsFileName()
        {
            string settingsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Amleto");
            Directory.CreateDirectory(settingsFile);
            settingsFile = Path.Combine(settingsFile, "ServerSettings.xml");

            return settingsFile;
        }

        public ServerSettings()
        {
            WinSize = new Size(900, 750);
            ToEmail = "your@email.com";
            SubjectOk = "Render Job was completed successfully";
            SubjectNotOk = "Render job has finished with errors";
            SendEmail = false;
            EmailIncludeActivity = true;
            ViewerHeight = 600;
            ViewerWidth = 800;

            OfferWebPort = 9080;
            OfferWeb = true;
            SMTPPassword = "";
            SMTPUsername = "";
            SMTPServer = "mail.yourdomain.com";
            EmailFrom = "amleto@yourdomain.com";
            Port = 2080;
            AutoOfferPort = true;
            RenderBlocks = 5;
            Configs = new List<ConfigSet>();
            MappedDrives = new List<MapDrive>();
            StrippedMaster = new List<string>();
        }

        static public ServerSettings LoadSettings()
        {
            string settingsFile = SettingsFileName();

            ServerSettings settings = new ServerSettings();

            if (File.Exists(settingsFile))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(settings.GetType());
                    TextReader reader = new StreamReader(settingsFile);
                    object deserialised = serializer.Deserialize(reader);
                    reader.Close();

                    settings = (ServerSettings)deserialised;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Error loading settings file");
                }
            }
            return settings;
        }

        static public void SaveSettings(ServerSettings settings)
        {
            string settingsFile = SettingsFileName();

            try
            {
                XmlSerializer seriaizer = new XmlSerializer(settings.GetType());
                TextWriter writer = new StreamWriter(settingsFile);
                seriaizer.Serialize(writer, settings);
                writer.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error saving settings file");
            }
        }
    }

}
