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
        public int BitSize { get; set; }
        public int LightwaveVersion { get; set; }
        public bool DefaultConfig;

        [XmlIgnore]
        public List<string> ImageFormats { get; set; }

        public ConfigSet()
        {
            ImageFormats = new List<string>();
            BitSize = 32;
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
            get { return Name + " (" + BitSize + "-bit)"; }
        }

    }
}
