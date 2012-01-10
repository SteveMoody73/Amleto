using System;
using System.Collections.Generic;

namespace RemoteExecution
{
    [Serializable]
    public class ConfigSet
    {
		public string ServerProgramPath { get; set; }
		public string ServerPluginPath { get; set; }
		public string ServerConfigPath { get; set; }
		public string Name { get; set; }
		public int PtrSize { get; set; }
		public List<string> ImageFormats { get; set; }
		
		public ConfigSet()
    	{
    		ImageFormats = new List<string>();
    		PtrSize = 4;
    		Name = "";
    		ServerConfigPath = "";
    		ServerPluginPath = "";
    		ServerProgramPath = "";
    	}

    	public string ConfigLabel
        {
            get { return Name + " (" + (PtrSize * 8) + "-bit)"; }
        }

    }
}
