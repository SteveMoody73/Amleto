using System;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace RemoteExecution
{
    [Serializable]
    public class ServerSettings
    {
        public Size WinSize { get; set; }
        public string ToEmail { get; set; }
        public string SubjectOk { get; set; }
        public string SubjectNotOk { get; set; }
        public bool SendEmail { get; set; }
        public bool EmailIncludeActivity { get; set; }
        public int ViewerWidth { get; set; }
        public int ViewerHeight { get; set; }


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
        }

        static public ServerSettings LoadSettings()
        {
            string settingsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Amleto");
            Directory.CreateDirectory(settingsFile);
            settingsFile = Path.Combine(settingsFile, "ServerSettings.xml");
            ServerSettings settings = new ServerSettings();

            if (File.Exists(settingsFile))
            {
                XmlSerializer serializer = new XmlSerializer(settings.GetType());
                TextReader reader = new StreamReader(settingsFile);
                object deserialised = serializer.Deserialize(reader);
                reader.Close();

                settings = (ServerSettings)deserialised;
            }
            return settings;
        }

        static public void SaveSettings(ServerSettings settings)
        {
            string settingsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Amleto");
            settingsFile = Path.Combine(settingsFile, "ServerSettings.xml");

            XmlSerializer seriaizer = new XmlSerializer(settings.GetType());
            TextWriter writer = new StreamWriter(settingsFile);
            seriaizer.Serialize(writer, settings);
            writer.Close();
        }
    }

}
