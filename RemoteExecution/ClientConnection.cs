using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Win32;
using RemoteExecution.Jobs;

namespace RemoteExecution
{
    [Serializable]
    public class ClientConnection
    {
        private static int _nextClientId = 1;

    	[NonSerialized]
        private Stopwatch _lastCall = new Stopwatch();

		public bool IsOnline { get; set; }
		public int Id { get; private set; }
		public string HostName { get; private set; }
        public string IPAddress { get; private set; }
		public int Instance { get; private set; }
		public List<Job> Jobs { get; set; }
		public List<Job> PriorityJobs { get; set; }
		public bool IsReady { get; set; }
		public string CurrentJob { get; set; }
		public RenderProject CurrentRender { get; set; }
		public ProcessPriorityClass Priority { get; set; }
		public bool Paused { get; set; }
		public int Config { get; set; }
		public int BitSize { get; set; }
		public string ActiveHours { get; set; }

		public ClientConnection(string hostName, string ipAddress, int instance, ProcessPriorityClass priority, int bitSize)
        {
			IsOnline = true;
			Jobs = new List<Job>();
			PriorityJobs = new List<Job>();
			CurrentJob = "";
			ActiveHours = "YYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY";
			Id = (_nextClientId++);
            HostName = hostName;
		    IPAddress = ipAddress;
            Instance = instance;
            Priority = priority;
            BitSize = bitSize;
            _lastCall.Reset();
            _lastCall.Start();

            RestoreSettings();
        }


    	public void KeepAlive()
        {
            _lastCall.Reset();
            _lastCall.Start();            
        }

        public long ElapsedTime
        {
            get { return _lastCall.ElapsedMilliseconds; }
        }

        public void StoreSettings()
        {
        	var openSubKey = Registry.LocalMachine.OpenSubKey("SOFTWARE", true);
        	if (openSubKey != null)
        	{
        		var registryKey = openSubKey.CreateSubKey("Amleto3");
        		if (registryKey != null)
        		{
        			var subKey = registryKey.CreateSubKey("ClientsConfig");
        			if (subKey != null)
        			{
        				RegistryKey key =
        					subKey.
        						CreateSubKey(HostName + "_" + Instance);
        				if (key != null)
        				{
        					key.SetValue("Config", Config);
        					key.SetValue("ActiveHours", ActiveHours);
        					key.Close();
        				}
        			}
        		}
        	}
        }

    	public void RestoreSettings()
        {
			var openSubKey = Registry.LocalMachine.OpenSubKey("SOFTWARE", true);
			if (openSubKey != null)
			{
				var registryKey = openSubKey.CreateSubKey("Amleto3");
				if (registryKey != null)
				{
					var subKey = registryKey.CreateSubKey("ClientsConfig");
					if (subKey != null)
					{
						RegistryKey key =
							subKey.
								CreateSubKey(HostName + "_" + Instance);
						Config = 0;
						if (key != null && key.GetValue("Config") != null)
							Config = (int) key.GetValue("Config");
						if (Config >= ServerServices.Configs.Count)
							Config = 0;
						if (key != null && key.GetValue("ActiveHours") != null)
							ActiveHours = (string) key.GetValue("ActiveHours");
						else
							ActiveHours = "YYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY";
						if (key != null) key.Close();
					}
				}
			}
        }

        public bool CanBeUsed
        {
            get
            {
                if (Paused)
                    return false;
                int t = DateTime.Now.Hour * 2 + (DateTime.Now.Minute < 30 ? 0 : 1);
                if (ActiveHours[t] == 'N')
                    return false;
                return true;
            }
        }
    }
}
