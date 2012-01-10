using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using System.IO;
using System.Collections;
using System.Diagnostics;
using Microsoft.Win32;
using RemoteExecution.Jobs;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;

namespace RemoteExecution
{
    public class MasterServer : MarshalByRefObject
    {
    	private ServerServices.StatusClientChange _clientStatus;
        private ServerServices.StatusProjectChange _projectStatus;
        private ServerServices.StatusFinishedFrameChange _imagePreview;
        private ServerServices.StatusStringChange _messageConsumer;
        private List<MapDrive> _mappedDrives = new List<MapDrive>();
        private List<string> _strippedMaster = new List<string>();

		public bool AutoOfferPort { get; set; }
		public int Port { get; set; }
		public string EmailFrom { get; set; }
		public string SmtpServer { get; set; }
		public string SmtpUsername { get; set; }
		public string SmtpPassword { get; set; }
		public bool OfferWeb { get; set; }
		public int OfferWebPort { get; set; }
		public TcpChannel Channel { get; set; }
		public BroadcastListener AutoSetupServer { get; set; }
		public string User { get; set; }
		public Thread CheckTimeoutThread { get; set; }
		public WebServer WebServer { get; set; }


        public MasterServer(string user)
        {
        	OfferWebPort = 9080;
        	OfferWeb = true;
        	SmtpPassword = "";
        	SmtpUsername = "";
        	SmtpServer = "mail.yourdomain.com";
        	EmailFrom = "amleto@yourdomain.com";
        	Port = 2080;
        	AutoOfferPort = true;
        	User = user;
        }

    	public void Startup()
        {
            ServerServices.AddMessage(0, "Unlimited nodes.");

            CheckTimeoutThread = new Thread(CheckTimeout);
            CheckTimeoutThread.IsBackground = true;
            CheckTimeoutThread.Start();

            ServerServices.AddMessage(0, "Starting up...");
            ServerServices.AddMessage(0, "Finding LW supported file formats...");
            LoadFileFormats();
            ServerServices.AddMessage(0, "Setting up the server...");
            SetupService();
            AutoSetupServer = new BroadcastListener(Port);
            ServerServices.AddMessage(0, "Autodiscovery server running at UDP port 61111");

            if (OfferWeb)
                WebServer = new WebServer(this, OfferWebPort);

            foreach (MapDrive mapDrive in _mappedDrives)
                mapDrive.Mount();
        }

        private void CheckTimeout()
        {
            while (true)
            {
                Thread.Sleep(300);
                ServerServices.CheckTimedOutClients();
            }
        }

        public void LoadFileFormats()
        {
            foreach (ConfigSet config in ServerServices.Configs)
            {
                string[] lines;
                try
                {
                    if (File.Exists(config.ServerConfigPath + "\\LW10-64.CFG") || File.Exists(config.ServerConfigPath + "\\LW10.CFG"))
                    {
                        config.ImageFormats = new List<string>(LW10.FileFormat);
                        continue;
                    }
                    if (config.PtrSize == 8)
                        lines = File.ReadAllLines(config.ServerConfigPath + "\\LWEXT9-64.CFG");
                    else if (File.Exists(config.ServerConfigPath + "\\LWEXT9.CFG"))
                        lines = File.ReadAllLines(config.ServerConfigPath + "\\LWEXT9.CFG");
                    else if (File.Exists(config.ServerConfigPath + "\\LWEXT8.CFG"))
                        lines = File.ReadAllLines(config.ServerConfigPath + "\\LWEXT8.CFG");
                    else if (File.Exists(config.ServerConfigPath + "\\LWEXT3.CFG"))
                        lines = File.ReadAllLines(config.ServerConfigPath + "\\LWEXT3.CFG");
                    else
                        continue;
                }
                catch
                {
                    continue;
                }
                List<string> formats = new List<string>();
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (line.Contains("Class \"ImageSaver\""))
                    {
                        i++;
                        string[] p = lines[i].Trim().Replace("\"", "").Split(' ');
                        formats.Add(p[1]);
                    }
                }
                config.ImageFormats = formats;
            }
        }

        private void SetupService()
        {
            if (AutoOfferPort)
            {
                for (Port = 2080; Port < 60000; Port++)
                {
                    try
                    {
                        BinaryServerFormatterSinkProvider serverProv = new BinaryServerFormatterSinkProvider();
                        serverProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
                        BinaryClientFormatterSinkProvider clientProv = new BinaryClientFormatterSinkProvider();
                        IDictionary props = new Hashtable();

                        props["port"] = Port;
                        Channel = new TcpChannel(props, clientProv, serverProv);
                        break;
                    }
                    catch (Exception ex)
                    {
						Debug.WriteLine("Setting up service: " + ex);
                    }
                }
            }
            else
            {
                try
                {
                    BinaryServerFormatterSinkProvider serverProv = new BinaryServerFormatterSinkProvider();
                    serverProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
                    BinaryClientFormatterSinkProvider clientProv = new BinaryClientFormatterSinkProvider();
                    IDictionary props = new Hashtable();

                    props["port"] = Port;
                    Channel = new TcpChannel(props, clientProv, serverProv);
                }
                catch (Exception ex)
                {
                    ServerServices.AddMessage(1, "Cannot use port " + Port);
					Debug.WriteLine("Cannot use port: " + ex);
					return;
                }
            }

            ChannelServices.RegisterChannel(Channel, false);
            // Register ServerServices
            if (!RemotingConfiguration.IsActivationAllowed(typeof(ServerServices)))
            {
                RemotingConfiguration.RegisterActivatedServiceType(typeof(ServerServices));
            }
            // Register MasterServer
            if (!RemotingConfiguration.IsActivationAllowed(typeof(MasterServer)))
            {
                RemotingConfiguration.RegisterActivatedServiceType(typeof(MasterServer));
            }
            ServerServices.AddMessage(0, "Server running at port " + Port);
        }

        public void ResetPort(bool autoOfferPort, int port)
        {
            AutoOfferPort = autoOfferPort;
            Port = port;
            ChannelServices.UnregisterChannel(Channel);
            Channel.StopListening(null);
            SetupService();
            AutoSetupServer.Port = port;
        }

        public void AddPriorityJob(int clientId, Job j)
        {
            ServerServices.AddPriorityJob(clientId, j);
        }

        public RenderProject GetProject(int projectId)
        {
            return ServerServices.GetProject(projectId);
        }

        public void AddProject(RenderProject project)
        {
            project.ProjectId = (RenderProject.NextProjectId++);
            project.Owner = User;
            project.GenerateRenderJobs();
            ServerServices.AddProject(project);
        }

        public void ReplaceProject(int projectId, RenderProject project)
        {
            project.GenerateRenderJobs();
            ServerServices.ReplaceProject(projectId, project);
        }

        public List<string> ImageFormats(int config)
        {
            return ServerServices.Configs[config].ImageFormats;
        }

        public void AddClientStatus(ServerServices.StatusClientChange clientStatus)
        {
            _clientStatus = clientStatus;
            ServerServices.ClientStatus += clientStatus;
        }

        public void AddProjectStatus(ServerServices.StatusProjectChange projectStatus)
        {
            _projectStatus = projectStatus;
            ServerServices.ProjectStatus += projectStatus;
        }

        public void AddImagePreview(ServerServices.StatusFinishedFrameChange imagePreview)
        {
            _imagePreview = imagePreview;
            ServerServices.ImagePreviewStatus += imagePreview;
        }

        public void AddMessageConsumer(ServerServices.StatusStringChange messageConsumer)
        {
            _messageConsumer = messageConsumer;
            ServerServices.MessageConsumer += messageConsumer;
        }

        public void AddMessage(int icon, string msg)
        {
            ServerServices.AddMessage(icon, msg);
        }

        public void Shutdown()
        {
            foreach (MapDrive mapDrive in _mappedDrives)
                mapDrive.Unmount();
            ServerServices.SaveAllNodeConfig();
            ChannelServices.UnregisterChannel(Channel);
            AddMessage(0, "Shutting down...");
        }

        public void KeepAlive()
        {
        }

        public void Disconnect()
        {
            ServerServices.MessageConsumer -= _messageConsumer;
            ServerServices.ImagePreviewStatus -= _imagePreview;
            ServerServices.ProjectStatus -= _projectStatus;
            ServerServices.ClientStatus -= _clientStatus;
        }

        public List<string> GetOldMessages()
        {
            return ServerServices.GetOldMessages();
        }

        public List<FinishedFrame> GetOldFrames()
        {
            return ServerServices.GetOldFrames();
        }

        public List<ClientConnection> GetConnectedHosts()
        {
            return ServerServices.GetConnectedHosts();
        }

        public List<RenderProject> GetProjects()
        {
            return ServerServices.GetProjects();
        }

        /*public List<ConfigSet> Configs
        {
            get { return ServerServices.configs; }
        }*/

        public int NbConfigs
        {
            get { return ServerServices.Configs.Count; }
        }

        public List<string> ConfigNames
        {
            get
            {
                List<string> res = new List<string>();
                foreach (ConfigSet c in ServerServices.Configs)
                    res.Add(c.Name + " (" + (c.PtrSize * 8) + "-bit)");
                return res;
            }
        }

        public void ReplaceConfigs(List<ConfigSet> configs)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, configs);
            stream.Position = 0;
            List<ConfigSet> newConfigs = (List<ConfigSet>)formatter.Deserialize(stream);
            stream.Dispose();

            ServerServices.Configs = newConfigs;
            ServerServices.CheckClientConfigs();
        }

        public List<ConfigSet> GetConfigs()
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, ServerServices.Configs);
            stream.Position = 0;
            List<ConfigSet> newConfigs = (List<ConfigSet>)formatter.Deserialize(stream);
            stream.Dispose();

            return newConfigs;
        }

        public bool RestoreSettings()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = "'";

            RegistryKey key = null;
            try
            {
            	var openSubKey = Registry.CurrentUser.OpenSubKey("SOFTWARE", true);
            	if (openSubKey != null)
            		key = openSubKey.CreateSubKey("Amleto3");
            }
            catch
            {
                return false;
            }

            if (key == null)
                return false;

            object o = key.GetValue("Port");
            if (o == null)
                return false;
            
			Port = 2080;
            Port = Convert.ToInt32(o);

            o = key.GetValue("AutoPort");
            AutoOfferPort = true;
            if (o != null)
                AutoOfferPort = Convert.ToBoolean(o);

            RegistryKey configKey = key.CreateSubKey("Configs");

        	if (configKey != null)
        	{
        		foreach (string s in configKey.GetSubKeyNames())
        		{
        			ConfigSet c = new ConfigSet();
        			RegistryKey subkey = configKey.OpenSubKey(s);
        			c.Name = s;
        			if (subkey != null)
        			{
        				c.ServerProgramPath = (string)subkey.GetValue("ProgPath");
        				c.ServerConfigPath = (string)subkey.GetValue("ConfigPath");
        				c.ServerPluginPath = (string)subkey.GetValue("PluginPath");
        				if (subkey.GetValue("PtrSize") != null)
        					c.PtrSize = (int)subkey.GetValue("PtrSize");
        				else
        					c.PtrSize = 4;
        				subkey.Close();
        			}

        			// Insert the main config at the top
        			if (s == "Main Config")
        				ServerServices.Configs.Insert(0, c);
        			else
        				ServerServices.Configs.Add(c);
        		}
        		configKey.Close();
        	}

        	_mappedDrives = MapDrive.ReadMapDrives(key);

            _strippedMaster.Clear();
            RegistryKey maskerKey = key.CreateSubKey("StripMaster");
        	if (maskerKey != null)
        	{
        		foreach (string s in maskerKey.GetValueNames())
        		{
        			_strippedMaster.Add(s);
        		}
        		maskerKey.Close();
        	}

        	if (ServerServices.Configs.Count == 0 && key.GetValue("ProgPath") != null)
            {
                ConfigSet c = new ConfigSet();
                c.Name = "Main Config";
                c.ServerProgramPath = (string)key.GetValue("ProgPath");
                c.ServerConfigPath = (string)key.GetValue("ConfigPath");
                c.ServerPluginPath = (string)key.GetValue("PluginPath");
                c.PtrSize = 4;
                ServerServices.Configs.Add(c);
            }

            ServerServices.LogFile = "";
            if (key.GetValue("ServerLogFile") != null)
                LogFile = (string)key.GetValue("ServerLogFile");

            if (key.GetValue("ServerLogEnable") != null)
                LogEnabled = key.GetValue("ServerLogEnable").ToString() == "True";

            if (key.GetValue("EmailFrom") != null)
                EmailFrom = (string)key.GetValue("EmailFrom");
            if (key.GetValue("SMTPServer") != null)
                SmtpServer = (string)key.GetValue("SMTPServer");
            if (key.GetValue("SMTPUsername") != null)
                SmtpUsername = (string)key.GetValue("SMTPUsername");
            if (key.GetValue("SMTPPassword") != null)
                SmtpPassword = (string)key.GetValue("SMTPPassword");

            if (key.GetValue("OfferWeb") != null && (string)key.GetValue("OfferWeb") == "True")
                OfferWeb = true;
            else
                OfferWeb = false;
            if (key.GetValue("OfferWebPort") != null)
                OfferWebPort = (int)key.GetValue("OfferWebPort");
            else
                OfferWebPort = 9080;

            return true;
        }

        public void SaveSettings()
        {
        	RegistryKey openSubKey = Registry.CurrentUser.OpenSubKey("Software", true);
        	if (openSubKey != null)
        	{
        		RegistryKey key = openSubKey.CreateSubKey("Amleto3");

        		if (key != null)
        		{
        			key.SetValue("Port", Port);
        			key.SetValue("AutoPort", AutoOfferPort);
        			key.SetValue("ServerLogFile", ServerServices.LogFile);
        			key.SetValue("ServerLogEnable", ServerServices.LogEnabled);

        			key.SetValue("EmailFrom", EmailFrom);
        			key.SetValue("SMTPServer", SmtpServer);
        			key.SetValue("SMTPUsername", SmtpUsername);
        			key.SetValue("SMTPPassword", SmtpPassword);

        			key.SetValue("OfferWeb", OfferWeb);
        			key.SetValue("OfferWebPort", OfferWebPort);

        			// Remove all sub keys
        			RegistryKey configKey = key.CreateSubKey("Configs");
        			if (configKey != null)
        				foreach (string s in configKey.GetSubKeyNames())
        					configKey.DeleteSubKey(s);

        			// Recreate all sub keys
        			foreach (ConfigSet c in ServerServices.Configs)
        			{
        				if (configKey != null)
        				{
        					RegistryKey subkey = configKey.CreateSubKey(c.Name);
        					if (subkey != null)
        					{
        						subkey.SetValue("ProgPath", c.ServerProgramPath);
        						subkey.SetValue("ConfigPath", c.ServerConfigPath);
        						subkey.SetValue("PluginPath", c.ServerPluginPath);
        						subkey.SetValue("PtrSize", c.PtrSize);
        						subkey.Close();
        					}
        				}
        			}
        			if (configKey != null) configKey.Close();
        		}

        		MapDrive.StoreMapDrives(key, _mappedDrives);

        		if (key != null) key.Close();
        	}
        }

    	public bool IsStripped(string s)
        {
            return _strippedMaster.Contains(s);
        }

        public void StrippedMasterAdd(string s)
        {
            if (_strippedMaster.Contains(s))
                return;
            _strippedMaster.Add(s);
        	RegistryKey openSubKey = Registry.CurrentUser.OpenSubKey("SOFTWARE", true);
        	if (openSubKey != null)
        	{
        		RegistryKey registryKey = openSubKey.CreateSubKey("Amleto3");
        		if (registryKey != null)
        		{
        			RegistryKey key = registryKey.CreateSubKey("StripMaster");
        			if (key != null)
        			{
        				key.SetValue(s, "");
        				key.Close();
        			}
        		}
        	}
        }

        public void StrippedMasterRemove(string s)
        {
            if (!_strippedMaster.Contains(s))
                return;
            _strippedMaster.Remove(s);
        	RegistryKey openSubKey = Registry.CurrentUser.OpenSubKey("SOFTWARE", true);
        	if (openSubKey != null)
        	{
        		RegistryKey registryKey = openSubKey.CreateSubKey("Amleto3");
        		if (registryKey != null)
        		{
        			RegistryKey key = registryKey.CreateSubKey("StripMaster");
        			if (key != null)
        			{
        				key.DeleteValue(s);
        				key.Close();
        			}
        		}
        	}
        }

        public List<string> DriveList()
        {
            List<string> res = new List<string>();
            foreach (string s in Environment.GetLogicalDrives())
            {
                try
                {
                    DriveInfo d = new DriveInfo(s);
                    res.Add(s + " (" + d.VolumeLabel + ")");
                }
                catch (Exception ex)
                {
					Debug.WriteLine("Listing logical drives: " + ex);
                }
            }
            return res;
        }

        public List<string> DirList(string path)
        {
            List<string> res = new List<string>();

        	try
            {
            	DirectoryInfo dir = new DirectoryInfo(path);
            	foreach (DirectoryInfo d in dir.GetDirectories())
                {
                    if (d.Name == ".")
                        continue;
                    res.Add(d.Name);
                }
            }
            catch (Exception ex)
            {
				Debug.WriteLine("Listing drives: " + ex);
                return res;
            }

            return res;
        }

        public List<string> FileList(string path, string pattern)
        {
            List<string> res = new List<string>();

        	try
            {
            	DirectoryInfo dir = new DirectoryInfo(path);
            	foreach (FileInfo f in dir.GetFiles(pattern))
                {
                    res.Add(f.Name);
                }
            }
            catch (Exception ex)
            {
				Debug.WriteLine("Listing files: " + ex);
				return res;
            }
            return res;
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public string[] FileReadAllLines(string path)
        {
            return File.ReadAllLines(path);
        }

        public byte[] FileReadAllBytes(string path)
        {
            return File.ReadAllBytes(path);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public void PauseProject(int id)
        {
            ServerServices.PauseProject(id);
        }

        public void ResumeProject(int id)
        {
            ServerServices.ResumeProject(id);
        }

        public void StopProject(int id)
        {
            ServerServices.StopProject(id);
        }

        public void SetClientPriority(int id, ProcessPriorityClass priority)
        {
            ServerServices.SetClientPriority(id, priority);
        }

        public ProcessPriorityClass GetClientPriority(int id)
        {
            return ServerServices.GetClientPriority(id);
        }

        public bool IsProjectPaused(int id)
        {
            return ServerServices.IsProjectPaused(id);
        }

        private static int Asciitonum(string str)
        {
        	const string ascii = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";

        	return ascii.IndexOf(str[0]) + ascii.IndexOf(str[1]) * 32;
        }

    	private static bool CheckCrc(string key)
        {
            int crc = 0;
            int keycrc = Asciitonum(key.Substring(key.Length - 2, 2));

            for (int i = 0; i < key.Length - 2; )
            {
                crc ^= Asciitonum(key.Substring(i, 2));
                i += 2;
                if (i >= key.Length - 2)
                    break;
                crc ^= Asciitonum(key.Substring(i, 2));
                i += 3;
            }
            return crc == keycrc;
        }

        public void PauseClient(int id)
        {
            ServerServices.PauseClient(id);
        }

        public void ResumeClient(int id)
        {
            ServerServices.ResumeClient(id);
        }

        public bool IsClientPaused(int id)
        {
            return ServerServices.IsClientPaused(id);
        }

        public string LogFile
        {
            get
            {
                return ServerServices.LogFile;
            }
            set
            {
                try
                {
                    if (value != "")
                        Directory.GetParent(value).Create();
                }
                catch (Exception ex)
                {
					Debug.WriteLine("Setting Log File: " + ex);
                }
                ServerServices.LogFile = value;
            }
        }

        public bool LogEnabled
        {
            get
            {
                return ServerServices.LogEnabled;
            }
            set
            {
                ServerServices.LogEnabled = value;
            }
        }



    	public void SetClientConfig(int id, int newConfig)
        {
            ServerServices.SetClientConfig(id, newConfig);
        }

        public string GetClientActivationTime(int id)
        {
            return ServerServices.GetClientActivationTime(id);
        }

        public void SetClientActivationTime(int id, string activationTime)
        {
            ServerServices.SetClientActivationTime(id, activationTime);
        }

        public List<MapDrive> GetMappedDrives()
        {
            return _mappedDrives;
        }

        public string SetMappedDrives(List<MapDrive> mapDrives)
        {
            string res = "";

            foreach (MapDrive mapDrive in _mappedDrives)
                mapDrive.Unmount();

            _mappedDrives = mapDrives;
            foreach (MapDrive mapDrive in _mappedDrives)
            {
                string response = mapDrive.Mount();
                if (response != "")
                {
                    if (res != "")
                        res += "\n";
                    res += "Error while mounting \"" + mapDrive.Drive + "\": " + response;
                }
            }
            return res;
        }

        public string GetUser()
        {
            return Environment.UserName;
        }

        public void ClearRenderedList()
        {
            ServerServices.FinishedProjects.Clear();
        }
    }
}
