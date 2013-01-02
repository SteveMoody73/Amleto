using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace RemoteExecution
{
    [Serializable]
    public class ClientSettings
    {
        public bool AutoServerFinder { get; set; }
        public string ServerHost { get; set; }
        public int ServerPort { get; set; }
        public string ClientDir { get; set; }
        public ProcessPriorityClass RenderPriority { get; set; }
        public string LogFile { get; set; }
        public bool SaveToLog { get; set; }
        public int NumThreads { get; set; }
        public int MemorySegment { get; set; }

        public ClientSettings()
        {
    		RenderPriority = ProcessPriorityClass.Normal;
    		ServerHost = "localhost";
    		LogFile = "";
            SaveToLog = false;
    		AutoServerFinder = true;
    		ServerPort = 2080;
            NumThreads = Environment.ProcessorCount;
    		MemorySegment = 128;
            ClientDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Amleto");
            ClientDir = Path.Combine(ClientDir, "Cache");

        }

        static public ClientSettings LoadSettings()
        {
            string settingsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Amleto");
            settingsFile = Path.Combine(settingsFile, "ClientSettings.xml");
            ClientSettings settings = new ClientSettings();

            if (File.Exists(settingsFile))
            {
                XmlSerializer serializer = new XmlSerializer(settings.GetType());
                TextReader reader = new StreamReader(settingsFile);
                object deserialised = serializer.Deserialize(reader);
                reader.Close();

                settings = (ClientSettings) deserialised;
            }
            return settings;
        }

        static public void SaveSettings(ClientSettings settings)
        {
            string settingsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Amleto");
            settingsFile = Path.Combine(settingsFile, "ClientSettings.xml");

            XmlSerializer seriaizer = new XmlSerializer(settings.GetType());
            TextWriter writer = new StreamWriter(settingsFile);
            seriaizer.Serialize(writer, settings);
            writer.Close();
        }
    }
}
