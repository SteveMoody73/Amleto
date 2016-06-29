using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace RemoteExecution
{
    [Serializable]
    public class ConfigSet
    {
        public string ProgramPath { get; set; }
        public string PluginPath { get; set; }
        public string SupportPath { get; set; }
        public string ConfigPath { get; set; }
        public string ConfigFile { get; set; }
        public string Name { get; set; }
        public bool Has64Bit { get; set; }
        public bool Has32Bit { get; set; }
        public int LightwaveVersion { get; set; }
        public bool DefaultConfig;

        [XmlIgnore]
        public List<string> ImageFormats { get; set; }

        public ConfigSet()
        {
            ImageFormats = new List<string>();
            Has32Bit = false;
            Has64Bit = false;
            Name = "";
            ConfigPath = "";
            ConfigFile = "";
            PluginPath = "";
            ProgramPath = "";
            SupportPath = "";
            DefaultConfig = false;
        }

        public string ConfigLabel
        {
            get
            {
                string bitString = string.Empty;
                if (Has64Bit)
                    bitString = "64";
                if (Has32Bit)
                {
                    if (string.IsNullOrEmpty(bitString))
                        bitString = "32";
                    else
                        bitString += ",32";
                }
                return Name + " (" + bitString +"-bit)";
            }
        }

    }
}
