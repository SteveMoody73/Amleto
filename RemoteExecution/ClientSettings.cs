using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using NLog;

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

        [NonSerialized] private static Logger logger = LogManager.GetCurrentClassLogger();

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
            ClientDir = Path.Combine(Paths.GetLocalPath(), "Cache");

        }

        static public ClientSettings LoadSettings()
        {
            Directory.CreateDirectory(Paths.GetLocalPath());
            string settingsFile = Path.Combine(Paths.GetLocalPath(), "ClientSettings.xml");
            ClientSettings settings = new ClientSettings();

            try
            {
                if (File.Exists(settingsFile))
                {
                    XmlSerializer serializer = new XmlSerializer(settings.GetType());
                    TextReader reader = new StreamReader(settingsFile);
                    object deserialised = serializer.Deserialize(reader);
                    reader.Close();

                    settings = (ClientSettings) deserialised;
                }
            }
            catch (Exception ex)
            {
                logger.ErrorException("Unable to load ClientSettings.xml", ex);
            }
            return settings;
        }

        static public void SaveSettings(ClientSettings settings)
        {
            try
            {
                string settingsFile = Path.Combine(Paths.GetLocalPath(), "ClientSettings.xml");

                XmlSerializer seriaizer = new XmlSerializer(settings.GetType());
                TextWriter writer = new StreamWriter(settingsFile);
                seriaizer.Serialize(writer, settings);
                writer.Close();
            }
            catch (Exception ex)
            {
                logger.ErrorException("Unable to save ClientSettings.xml", ex);
            }
        }
    }
}
