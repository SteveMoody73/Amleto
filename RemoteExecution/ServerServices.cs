using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using RemoteExecution.Jobs;

namespace RemoteExecution
{
    public enum FileType
    {
        Absolute,
        Program,
        Plugin,
        Config,
        Support
    }

    public class ServerServices : MarshalByRefObject
    {
		public delegate void StatusClientChange(List<ClientConnection> clients);
		public delegate void StatusProjectChange(List<RenderProject> projects);
        public delegate void StatusFinishedChange(List<RenderProject> finished);
        public delegate void StatusStringChange(string msg);
		public delegate void StatusStringStringChange(string op, string msg);
		public delegate void StatusFinishedFrameChange(FinishedFrame frame);

		public static event StatusClientChange ClientStatus;
		public static event StatusProjectChange ProjectStatus;
        public static event StatusFinishedChange FinishedStatus;
        public static event StatusFinishedFrameChange ImagePreviewStatus;
		public static event StatusStringChange MessageConsumer;

		private static List<ClientConnection> _clients = new List<ClientConnection>();
		private static List<string> _oldMessages = new List<string>();
		private ClientConnection _currConnection;
        
        public static string LogFile = "";
        public static bool LogEnabled;

        public static List<RenderProject> Projects = new List<RenderProject>();
        public static List<RenderProject> FinishedProjects = new List<RenderProject>();
		public static List<ConfigSet> Configs = new List<ConfigSet>();

        public void RegisterClient(string hostName, string ipAddress, ProcessPriorityClass priority, int bitSize)
        {
            lock (_clients)
            {
                int nextInstance = 1;
                bool foundInstance = false;
                while (!foundInstance)
                {
                    foundInstance = true;
                    foreach (ClientConnection c in _clients)
                    {
                        if (c.HostName == hostName && c.IPAddress == ipAddress && c.Instance == nextInstance)
                        {
                            nextInstance++;
                            foundInstance = false;
                            break;
                        }
                    }
                }

                _currConnection = new ClientConnection(hostName, ipAddress, nextInstance, priority, bitSize);
                _clients.Add(_currConnection);
            }
            AddMessage(0, "Node " + _currConnection.HostName + " (" + ipAddress + ")" + ":" + _currConnection.Instance + " connected (" + bitSize + "-bit).");
            CallUpdateClientList();
        }

        public static void CheckTimedOutClients()
        {
            bool removedSome = false;
            lock (_clients)
            {
                for (int i = 0; i < _clients.Count; )
                {
                    if (_clients[i].ElapsedTime > 10000)
                    {
                        AddMessage(1, "Node " + _clients[i].HostName + ":" + " (" + _clients[i].IPAddress + ")" + _clients[i].Instance + " timed out and removed.");
                        if (_clients[i].CurrentRender != null)
                            _clients[i].CurrentRender.ReleaseClientAllFrames(_clients[i].Id, _clients[i].HostName + " (" + _clients[i].IPAddress + ")" + ":" + _clients[i].Instance);
                        _clients[i].StoreSettings();
                        _clients[i].IsOnline = false;
                        _clients.RemoveAt(i);
                        removedSome = true;
                    }
                    else
                        i++;
                }
            }
            if (removedSome)
                CallUpdateClientList();
        }

        public bool IsWorking()
        {
            return true;
        }

        public void Unregister()
        {
            if (_currConnection == null)
                return;
            AddMessage(0, "Node " + _currConnection.HostName + " (" + _currConnection.IPAddress + ")" + ":" + _currConnection.Instance + " disconnected.");
            lock (_clients)
            {
                if (_currConnection.CurrentRender != null)
                    _currConnection.CurrentRender.ReleaseClientAllFrames(_currConnection.Id, _currConnection.HostName + " (" + _currConnection.IPAddress + ")" + ":" + _currConnection.Instance);
                _currConnection.StoreSettings();
                _clients.Remove(_currConnection);
            }
            CallUpdateClientList();
        }

        public bool HasPriorityJobs()
        {
            if (_currConnection == null)
                return false;
            if (!_currConnection.IsOnline)
                return false;
            lock (_clients)
            {
                if (_currConnection.PriorityJobs.Count > 0)
                    return true;
            }
            return false;
        }

        public List<Job> GetPriorityJobs()
        {
            if (_currConnection == null)
                return null;
            if (!_currConnection.IsOnline)
                return null;
            List<Job> oldJobs = _currConnection.PriorityJobs;
            _currConnection.PriorityJobs = new List<Job>();
            return oldJobs;
        }

        public bool HasJobs()
        {
            if (_currConnection == null)
                return false;
            if (!_currConnection.IsOnline)
                return false;
            lock (_clients)
            {
                lock (Projects)
                {
                    _currConnection.KeepAlive();
                    // Either paused or activation time disable it...
                    if (!_currConnection.CanBeUsed)
                        return false;

                    if (_currConnection.CurrentRender == null && Projects.Count > 0)
                    {
                        RenderProject project = null;
                        foreach (RenderProject t in Projects)
                        {
                            if (t.Paused == false && t.Config == _currConnection.Config && t.HasFreeJobs())
                            {
                                project = t;
                                break;
                            }
                        }

                        if (project != null)
                        {
                            if (!project.StartTimeSet)
                            {
                                project.StartTimeSet = true;
                                project.StartTime = DateTime.Now;
                            }

                            _currConnection.CurrentRender = project;
                        	if (IsFirstClient())
                        	{
                                AddMessage(2, "Sending scene " + _currConnection.CurrentRender.SceneId + " to " + _currConnection.HostName + " (" + _currConnection.IPAddress + ")");
                        		ChangeCurrentJobLabel("Downloading content for " + _currConnection.CurrentRender.SceneId);
                        		_currConnection.IsReady = false;
                        		_currConnection.Jobs.AddRange(_currConnection.CurrentRender.GetContentJobs());
                        		_currConnection.Jobs.Add(new ClientReadyJob());
                        	}
                        	else
                        	{
                        		ChangeCurrentJobLabel("Project " + _currConnection.CurrentRender.SceneId + " assigned. Waiting for first node on host.");
                        		_currConnection.Jobs.Add(new WaitFirstJob());
                            }
                        }
                    }
                    else if (_currConnection.CurrentRender != null && _currConnection.Jobs.Count == 0)
                    {
                        if (_currConnection.CurrentRender.Paused == false && _currConnection.CurrentRender.HasFreeJobs())
                        {
                            AddMessage(4, "Node " + _currConnection.HostName + " (" + _currConnection.IPAddress + ")" + ":" + _currConnection.Instance + " getting frame(s) job");
                            _currConnection.Jobs.Add(_currConnection.CurrentRender.GetRenderJob(_currConnection.Id, _currConnection.Instance));
                        }
                    }

                    CallUpdateProjectList();

                    if (_currConnection.Jobs.Count > 0)
                        return true;

                    _currConnection.CurrentRender = null;
                    ChangeCurrentJobLabel("");
                    return false;
                }
            }
        }

        public bool KeepAlive()
        {
            if (_currConnection == null)
                return false;
            if (!_currConnection.IsOnline)
                return false;
            _currConnection.KeepAlive();
            return true;
        }

        public bool IsFirstClient()
        {
            if (_currConnection == null)
                return false;
            if (!_currConnection.IsOnline)
                return false;
            lock (_clients)
            {
            	foreach (ClientConnection t in _clients)
            	{
            		if (t == _currConnection)
            			return true;
            		if (t.HostName == _currConnection.HostName && t.IPAddress == _currConnection.IPAddress)
            			return false;
            	}
            	// ??? We should  not reach this...
                return false;
            }
        }

        public bool IsFirstClientReady()
        {
            if (_currConnection == null)
                return false;
            if (!_currConnection.IsOnline)
                return false;
            lock (_clients)
            {
            	foreach (ClientConnection t in _clients)
            	{
            		if (t.HostName == _currConnection.HostName && t.IPAddress == _currConnection.IPAddress)
            			return t.IsReady;
            	}
            	// ??? We should  not reach this...
                return false;
            }
        }

        public static List<ClientConnection> GetConnectedHosts()
        {
        	MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, _clients);
            stream.Position = 0;
            List<ClientConnection> res = (List<ClientConnection>)formatter.Deserialize(stream);
            stream.Dispose();
            return res;
        }

        private string GetFullFilename(FileType t, string filename)
        {
            string file = "";

            switch (t)
            {
                case FileType.Absolute:
                    file = filename;
                    break;
                case FileType.Config:
                    file = Path.Combine(Configs[_currConnection.Config].ConfigPath, filename);
                    break;
                case FileType.Program:
                    file = Path.Combine(Configs[_currConnection.Config].ProgramPath, filename);
                    break;
                case FileType.Plugin:
                    file = Path.Combine(Configs[_currConnection.Config].PluginPath, filename);
                    break;
                case FileType.Support:
                    file = Path.Combine(Configs[_currConnection.Config].SupportPath, filename);
                    break;
            }

            return file;
        }

        public FileInfo GetFileInfo(FileType t, string filename)
        {
            if (_currConnection == null)
                return null;

            string file = GetFullFilename(t, filename);

            if (!File.Exists(file))
                return null;

            return new FileInfo(file);
        }
        
        public byte[] GetFile(FileType t, string filename)
        {
            if (_currConnection == null)
                return new byte[] { };
            string file = GetFullFilename(t, filename);

            if (!File.Exists(file))
                return new byte[] { }; 

            return File.ReadAllBytes(file);
        }

        private static ClientConnection ReturnClient(int id)
        {
        	foreach (ClientConnection t in _clients)
        	{
        		if (t.Id == id)
        			return t;
        	}
        	return null;
        }

    	public string GetConfigName()
        {
            if (_currConnection == null)
                return "";
            return Configs[_currConnection.Config].Name;
        }

        public static void SetClientPriority(int id, ProcessPriorityClass priority)
        {
            lock (_clients)
            {
                try
                {
                    ReturnClient(id).Priority = priority;
                    ReturnClient(id).PriorityJobs.Add(new SetPriorityJob(priority));
                }
                catch (Exception e)
                {
                    Debug.WriteLine("SetClientPriority: " + e);
                }
            }
        }

        public static void SetClientConfig(int id, int newConfig)
        {
            lock (_clients)
            {
                try
                {
                    ClientConnection node = ReturnClient(id);
                    // Check if it's possible!
                    if (Configs[newConfig].BitSize > node.BitSize)
                        return;
                    node.Config = newConfig;
                    node.Jobs.Add(new ChangeConfigJob(Configs[newConfig].Name));
                }
                catch (Exception e)
                {
                    Debug.WriteLine("SetClientConfig: " + e);
                }
            }
        }

        public static ProcessPriorityClass GetClientPriority(int id)
        {
            lock (_clients)
            {
                try
                {
                    return ReturnClient(id).Priority;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("GetClientPriority: " + e);
                    return ProcessPriorityClass.Normal;
                }
            }
        }

        public void ChangeClientPriority(ProcessPriorityClass priority)
        {
            if (_currConnection == null)
                return;

            lock (_clients)
            {
                _currConnection.Priority = priority;
            }
        }

        public static void AddPriorityJob(int id, Job job)
        {
            lock (_clients)
            {
                try
                {
                    ReturnClient(id).PriorityJobs.Add(job);
                }
				catch (Exception ex)
				{
					Debug.WriteLine("Adding Priority Job: " + ex);
				}

            }
        }

        public void AddJob(int id, Job job)
        {
            lock (_clients)
            {
                try
                {
                    ReturnClient(id).Jobs.Add(job);
                }
				catch (Exception ex)
				{
					Debug.WriteLine("Adding Job: " + ex);
				}
			}
        }

        public void FrameFinished(int frame, int sliceNumber)
        {
			if (_currConnection.CurrentRender == null)
                return;

			bool hasSlices = false;
			string fname;
            string sceneId = null;

            lock (_clients)
            {
                lock (Projects)
                {
                    if (_currConnection.CurrentRender.SlicesDown > 1 || _currConnection.CurrentRender.SlicesAcross > 1)
                        hasSlices = true;

                    fname = _currConnection.CurrentRender.FinishFrame(frame, sliceNumber, _currConnection.HostName + " (" + _currConnection.IPAddress + ")" + ":" + _currConnection.Instance);
                    if (fname != null)
                        sceneId = _currConnection.CurrentRender.SceneId;

                    _currConnection.CurrentRender.UpdateTime = DateTime.Now;
                    _currConnection.CurrentRender.UpdateTimeSet = true;

                    if (_currConnection.CurrentRender.NbRemainingJobs() == 0)
                    {
                        _currConnection.CurrentRender.CloseLogs();
                        RemoveProject(_currConnection.CurrentRender, false);
                    }
                }
            }

            if (hasSlices)
                AddMessage(4, "Node " + _currConnection.HostName + " (" + _currConnection.IPAddress + ")" + ":" + _currConnection.Instance + " uploaded slice " + sliceNumber + " of frame " + frame + ".");
            else
                AddMessage(4, "Node " + _currConnection.HostName + " (" + _currConnection.IPAddress + ")" + ":" + _currConnection.Instance + " uploaded frame " + frame + ".");
            
            if (fname != null)
            {
                AddMessage(4, "Frame " + frame + " rebuilt.");
                CallUpdateImagesPreview(sceneId, fname);
            }            
            CallUpdateProjectList();
        }

        public void FrameLost(int frame, int sliceNumber)
        {
            if (_currConnection == null)
                return;
            if (_currConnection.CurrentRender == null)
                return;
            lock (_clients)
            {
                lock (Projects)
                {
                    _currConnection.CurrentRender.ReleaseFrame(frame, sliceNumber, _currConnection.HostName + " (" + _currConnection.IPAddress + ")" + ":" + _currConnection.Instance);
                    _currConnection.CurrentRender.UpdateTime = DateTime.Now;
                    _currConnection.CurrentRender.UpdateTimeSet = true;
                }
            }
            AddMessage(1, "Node " + _currConnection.HostName + " (" + _currConnection.IPAddress + ") " + _currConnection.Instance + " lost frame " + frame + ".");
        }

        public List<Job> GetJobs()
        {
            if (_currConnection == null)
                return new List<Job>();

            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, _currConnection.Jobs);
            stream.Position = 0;
            List<Job> newJobs = (List<Job>)formatter.Deserialize(stream);
            stream.Dispose();
            _currConnection.Jobs.Clear();
            return newJobs;
        }

        public List<string> GetSetupFileList()
        {
            List<string> res = new List<string>();

            if (_currConnection == null)
                return res;

            DirectoryInfo dir = new DirectoryInfo(Configs[_currConnection.Config].ProgramPath);
            try
            {
                Queue<string> dirs = new Queue<string>();
                dirs.Enqueue(Configs[_currConnection.Config].ProgramPath);
                int prefixLength = (Configs[_currConnection.Config].ProgramPath + "\\").Length;

                while (dirs.Count > 0)
                {
                    string currentDir = dirs.Dequeue();
                    dir = new DirectoryInfo(currentDir);

                    foreach (var i in dir.GetDirectories())
                        dirs.Enqueue(Path.Combine(currentDir, i.Name));

                    foreach (var f in dir.GetFiles())
                    {
                        string file = Path.Combine(currentDir, f.Name).Substring(prefixLength);
                        if (f.Name.ToLower() == "hub.exe" ||
                            f.Name.ToLower() == "license.key" ||
                            f.Name.ToLower() == "lsed.exe" ||
                            f.Name.ToLower() == "lsid.exe" ||
                            f.Name.ToLower() == "modeler.exe" ||
                            f.Name.ToLower() == "startlwsn_node.bat" ||
                            f.Name.ToLower() == "stoplwsn_node.bat")
                            continue;
                        res.Add(file);
                    }
                }
            }
			catch (Exception ex)
			{
				Debug.WriteLine("Getting setup files: " + ex);
			}

            return res;
        }

        public List<string> GetSupportFileList()
        {
            List<string> res = new List<string>();

            if (_currConnection == null)
                return res;

            if (Configs[_currConnection.Config].SupportPath != "")
            {
                DirectoryInfo dir = new DirectoryInfo(Configs[_currConnection.Config].SupportPath);
                int prefixLength = (Configs[_currConnection.Config].SupportPath + "\\").Length;

                Queue<string> dirs = new Queue<string>();
                dirs.Enqueue(Configs[_currConnection.Config].SupportPath);
                while (dirs.Count > 0)
                {
                    string currentDir = dirs.Dequeue();
                    dir = new DirectoryInfo(currentDir);

                    foreach (var i in dir.GetDirectories())
                        dirs.Enqueue(Path.Combine(currentDir, i.Name));

                    foreach (var i in dir.GetFiles())
                    {
                        string file = Path.Combine(currentDir, i.Name).Substring(prefixLength);
                        res.Add(file);
                    }
                }
            }
            return res;
        }


        public List<string> GetPluginFileList()
        {
            return GetPluginFileList(Configs[_currConnection.Config].PluginPath);
        }

        private List<string> GetPluginFileList(string path)
        {
            List<string> res = new List<string>();

            if (_currConnection == null || path == "")
                return res;

            DirectoryInfo dir = new DirectoryInfo(path);
            // Visit all directories
            try
            {
                foreach (DirectoryInfo d in dir.GetDirectories())
                {
                    if (d.Name == "." || d.Name == "..")
                        continue;
                    res.AddRange(GetPluginFileList(d.FullName));
                }
                // Retreive all files
                foreach (FileInfo f in dir.GetFiles())
                    res.Add(f.FullName.Substring(Configs[_currConnection.Config].PluginPath.Length));
            }
			catch (Exception ex)
			{
				Debug.WriteLine("Getting plugin file list: " + ex);
			}


            return res;
        }

        public List<string> GetExtPluginFileList()
        {
            string lwExtFile = Path.GetFileName(Configs[_currConnection.Config].ConfigFile).ToUpper().Replace("LW", "LWEXT");
            lwExtFile = Path.Combine(Configs[_currConnection.Config].ConfigPath, lwExtFile);
           
            List<string> res = new List<string>();

            if (_currConnection == null || lwExtFile == "")
                return new List<string>();

            try
            {
                string[] lines = File.ReadAllLines(lwExtFile);
                foreach (string line in lines)
                {
                    if (line.ToUpper().Contains("MODULE \"") || line.StartsWith("  Module \""))
                    {
                        string plugin = line.Replace(@"\\", @"\").Trim();
                        plugin = plugin.Substring(plugin.IndexOf("\""));
                        plugin = plugin.Replace('\"', ' ').Trim();
                        res.Add(plugin);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Getting additional plugin file list: " + ex);
            }

            return res;
        }

        public List<string> GetConfigFileList()
        {
            List<string> res = new List<string>();

            if (_currConnection == null)
                return res;

            DirectoryInfo dir = new DirectoryInfo(Configs[_currConnection.Config].ConfigPath);
            foreach (FileInfo f in dir.GetFiles("*.CFG"))
                res.Add(f.FullName.Substring((Configs[_currConnection.Config].ConfigPath + "\\").Length));

            return res;
        }

        public void SetReady()
        {
            lock (_clients)
            {
                _currConnection.IsReady = true;
            }
            ChangeCurrentJobLabel("");
        }

        public string GetPluginPath()
        {
            if (_currConnection == null)
                return "";
            return Configs[_currConnection.Config].PluginPath;
        }

        public void SetCurrentJob(string currentJob)
        {
            ChangeCurrentJobLabel(currentJob);
        }

        private static void CallUpdateClientList()
        {
            if (ClientStatus != null)
            {
                for (int i = 0; i < ClientStatus.GetInvocationList().Length; )
                {
                    Delegate d = ClientStatus.GetInvocationList()[i];
                    try
                    {
                        d.DynamicInvoke(new object[] { GetConnectedHosts() });
                        i++;
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("CallUpdateClientList: " + e);
                        ClientStatus -= (StatusClientChange)d;
                    }
                }
            }
        }

        private void ChangeCurrentJobLabel(string currentJob)
        {
            if (_currConnection == null)
                return;
            if (_currConnection.CurrentJob == currentJob)
                return;
            _currConnection.CurrentJob = currentJob;
            CallUpdateClientList();
        }

        public void RemoveProject(RenderProject project, bool needToLock)
        {
            if (needToLock)
            {
                lock (_clients)
                {
                    lock (Projects)
                    {
                        RemoveProject(project);
                    }
                }
            }
            else
                RemoveProject(project);
            CallUpdateProjectList();
            CallUpdateFinishedList();
        }

        private static void RemoveProject(RenderProject project)
        {
            while (FinishedProjects.Count > 50)
                FinishedProjects.RemoveAt(0);
            GC.Collect();

            project.RenderedFrameCount = project.RenderedFrames.Count;
            project.RenderTime = DateTime.Now - project.StartTime;
            project.FinalStatus = "Finished";

            FinishedProjects.Add(project);
            Projects.Remove(project);
            
            foreach (ClientConnection t in _clients)
            {
            	if (t.CurrentRender == project)
            		t.CurrentRender = null;
            }
        }

        public static void AddProject(RenderProject project)
        {
            lock (Projects)
            {
                if (project.StartJobs > 0)
                {
                    Projects.Add(project);
                }
                else
                {
                    project.FinalStatus = "No Frames to Render";
                    project.IsFinished = true;
                    FinishedProjects.Add(project);
                }
            }
            CallUpdateProjectList();
            CallUpdateFinishedList();
        }

        public static void ReplaceProject(int projectId, RenderProject project)
        {
            lock (_clients)
            {
                lock (Projects)
                {
                    RenderProject oldproject = null;
                    foreach (RenderProject p in Projects)
                    {
                        if (p.ProjectId == projectId)
                        {
                            oldproject = p;
                            break;
                        }
                    }
                    Projects.Remove(oldproject);
                    foreach (ClientConnection c in _clients)
                    {
                        if (c.CurrentRender == oldproject)
                        {
                            c.CurrentRender = null;
                            c.PriorityJobs.Add(new KillJob());
                        }
                    }

                    project.IsFinished = false;
                    project.StartTimeSet = false;
                    project.UpdateTimeSet = false;

                    if (project.StartJobs > 0)
                    {
                        Projects.Add(project);
                        // Check if project is in the Finished list and remove it
                        foreach (RenderProject p in Projects)
                        {
                            if (p.ProjectId == project.ProjectId)
                                FinishedProjects.Remove(p);
                        }

                    }
                    else
                    {
                        project.FinalStatus = "No Frames to Render";
                        project.IsFinished = true;
                        FinishedProjects.Add(project);
                    }
                }
            }
            CallUpdateProjectList();
            CallUpdateFinishedList();
        }

        public static RenderProject GetProject(int projectId)
        {
            lock (Projects)
            {
                foreach (RenderProject p in Projects)
                {
                    if (p.ProjectId == projectId)
                        return p;
                }
                foreach (RenderProject p in FinishedProjects)
                {
                    if (p.ProjectId == projectId)
                        return p;
                }
            }
            return null;
        }

        public static List<RenderProject> GetProjects()
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, Projects);
            stream.Position = 0;
            List<RenderProject> res = (List<RenderProject>)formatter.Deserialize(stream);
            stream.Dispose();
            return res;
        }

        public static List<RenderProject> GetFinishedProjects()
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, FinishedProjects);
            stream.Position = 0;
            List<RenderProject> res = (List<RenderProject>)formatter.Deserialize(stream);
            stream.Dispose();
            return res;
        }

        public void SendFile(string basePath, string filename, byte[] file)
        {
            if (_currConnection == null)
                return;

            if (_currConnection.CurrentRender == null)
                return;

            lock (_clients)
            {
                lock (Projects)
                {
                    _currConnection.CurrentRender.SendFile(basePath, filename, file);
                }
            }
        }

        public void SendImage(int frame, int sliceNumber, byte[] img)
        {
            if (_currConnection == null)
                return;
            if (_currConnection.CurrentRender == null)
                return;
            string fname;
            lock (_clients)
            {
                lock (Projects)
                {
                    fname = _currConnection.CurrentRender.SaveImage(frame, sliceNumber, img);
                    
                }
            }
            CallUpdateImagesPreview(_currConnection.CurrentRender.SceneId, fname);
        }

        public void SendImageAlpha(int frame, int sliceNumber, byte[] img)
        {
            if (_currConnection == null)
                return;
            if (_currConnection.CurrentRender == null)
                return;

			lock (_clients)
            {
                lock (Projects)
                {
                    _currConnection.CurrentRender.SaveImageAlpha(frame, sliceNumber, img);
                }
            }
        }

        public static void CallUpdateImagesPreview(string sceneId, string fname)
        {
            if (ImagePreviewStatus != null)
            {
                for (int i = 0; i < ImagePreviewStatus.GetInvocationList().Length; )
                {
                    Delegate d = ImagePreviewStatus.GetInvocationList()[i];
                    try
                    {
                        d.DynamicInvoke(new object[] { new FinishedFrame(sceneId, fname) });
                        i++;
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("CallUpdateImagesPreview: " + e);
                        ImagePreviewStatus -= (StatusFinishedFrameChange)d;
                    }
                }
            }
        }

        public static void AddMessage(int icon, string msg)
        {
            string fullMsg = icon + "|" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "|" + msg;
            if (LogFile != "" && LogEnabled)
            {
                try
                {
                    File.AppendAllText(LogFile, fullMsg + "\r\n");
                }
				catch (Exception ex)
				{
					Debug.WriteLine("Adding Message: " + ex);
				}

            }

            while (_oldMessages.Count > 3000)
                _oldMessages.RemoveAt(0);
            
			_oldMessages.Add(fullMsg);
            
			if (MessageConsumer == null)
                return;
            
			for (int i = 0; i < MessageConsumer.GetInvocationList().Length; )
            {
                Delegate d = MessageConsumer.GetInvocationList()[i];
                try
                {
                    d.DynamicInvoke(new object[] { fullMsg });
                    i++;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("AddMessage: " + e);
                    MessageConsumer -= (StatusStringChange)d;
                }
            }
        }

        public static List<string> GetOldMessages()
        {
            return _oldMessages;
        }

        private static void CallUpdateProjectList()
        {
            if (ProjectStatus != null)
            {
                for (int i = 0; i < ProjectStatus.GetInvocationList().Length; )
                {
                    Delegate d = ProjectStatus.GetInvocationList()[i];
                    try
                    {
                        d.DynamicInvoke(new object[] { GetProjects() });
                        i++;
                    }
                    catch(Exception e)
                    {
                        Debug.WriteLine("CallUpdateProjectList: " + e);
                        ProjectStatus -= (StatusProjectChange)d;
                    }
                }
            }
        }

        private static void CallUpdateFinishedList()
        {
            if (FinishedStatus != null)
            {
                for (int i = 0; i < FinishedStatus.GetInvocationList().Length; )
                {
                    Delegate d = FinishedStatus.GetInvocationList()[i];
                    try
                    {
                        d.DynamicInvoke(new object[] { GetFinishedProjects() });
                        i++;
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("CallUpdateFinshedList: " + e);
                        FinishedStatus -= (StatusFinishedChange)d;
                    }
                }
            }
        }

        public static bool IsProjectPaused(int id)
        {
            lock (Projects)
            {
                foreach (RenderProject t in Projects)
                {
                	if (t.ProjectId == id)
                		return t.Paused;
                }
            }
            return false;
        }

        public static void PauseProject(int id)
        {
            lock (Projects)
            {
                foreach (RenderProject t in Projects)
                {
                	if (t.ProjectId == id)
                	{
                		t.Paused = true;
                		CallUpdateProjectList();
                		return;
                	}
                }
            }
        }

        public static void ResumeProject(int id)
        {
            lock (Projects)
            {
                foreach (RenderProject t in Projects)
                {
                	if (t.ProjectId == id)
                	{
                		t.Paused = false;
                		CallUpdateProjectList();
                		return;
                	}
                }
            }
        }

        public static void StopProject(int id)
        {
            RenderProject proj = null;
            lock (_clients)
            {
                lock (Projects)
                {
                    foreach (RenderProject t in Projects)
                    {
                    	if (t.ProjectId == id)
                    	{
                    		proj = t;
                    		t.RemoveAllJobs();
                    		break;
                    	}
                    }
                }

                // Check all the clients if any use the current project, 
                // if yes set as null and kill any jobs.
                if (proj != null)
                {
                    foreach (ClientConnection t in _clients)
                    {
                    	if (t.CurrentRender == proj)
                    	{
                    		t.CurrentRender = null;
                    		t.PriorityJobs.Add(new KillJob());
                    	}
                    }
                	Projects.Remove(proj);
                    proj.RenderedFrameCount = proj.RenderedFrames.Count;
                    proj.FinalStatus = "Stopped";
                    if (proj.StartTimeSet && proj.UpdateTimeSet)
                        proj.RenderTime = proj.UpdateTime - proj.StartTime;
                    proj.IsFinished = true;
                    FinishedProjects.Add(proj);
                }
            }
            CallUpdateProjectList();
            CallUpdateFinishedList();
        }

        public static void PauseClient(int id)
        {
            lock (_clients)
            {
                try
                {
                    ReturnClient(id).Paused = true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("PauseClient: " + e);
                }
            }
            CallUpdateClientList();
        }

        public static void ResumeClient(int id)
        {
            lock (_clients)
            {
                try
                {
                    ReturnClient(id).Paused = false;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("ResumeClient: " + e);
                }
            }
            CallUpdateClientList();
        }

        public static bool IsClientPaused(int id)
        {
            lock (_clients)
            {
                try
                {
                    return ReturnClient(id).Paused;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("IsClientPaused: " + e);                    
                }
            }
            return false;
        }

        public static void SaveAllNodeConfig()
        {
            lock (_clients)
            {
                foreach (ClientConnection c in _clients)
                    c.StoreSettings();
            }
        }

        public static void CheckClientConfigs()
        {
            lock (_clients)
            {
                foreach (ClientConnection c in _clients)
                {
                    if (c.Config >= Configs.Count)
                        c.Config = 0;
                    c.Jobs.Add(new ChangeConfigJob(Configs[0].Name));
                }
            }
        }

        public static List<FinishedFrame> GetOldFrames()
        {
            List<FinishedFrame> res = new List<FinishedFrame>();
            lock (Projects)
            {
            	foreach (RenderProject t in FinishedProjects)
            		res.AddRange(t.RenderedFrames);
            	foreach (RenderProject t in Projects)
            		res.AddRange(t.RenderedFrames);
            }
        	return res;
        }

        public static string GetClientActivationTime(int id)
        {
            lock (_clients)
            {
                try
                {
                    return ReturnClient(id).ActiveHours;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("GetClientActivationTime: " + e);
                }
            }
            return "";
        }

        public static void SetClientActivationTime(int id, string activationTime)
        {
            lock (_clients)
            {
                try
                {
                    ReturnClient(id).ActiveHours = activationTime;
                }
				catch (Exception ex)
				{
					Debug.WriteLine("Setting client activation time: " + ex);
				}

            }
        }

        public static void RemoveFinished(RenderProject project, bool needToLock)
        {
            if (needToLock)
            {
                lock (_clients)
                {
                    lock (Projects)
                    {
                        FinishedProjects.Remove(project);
                    }
                }
            }
            else
                FinishedProjects.Remove(project);
            CallUpdateFinishedList();
        }

        public static void RemoveAllFinished(bool needToLock)
        {
            if (needToLock)
            {
                lock (_clients)
                {
                    lock (Projects)
                    {
                        FinishedProjects.Clear();
                    }
                }
            }
            else
                FinishedProjects.Clear();
            CallUpdateFinishedList();
        }

        public void SetLastRenderTime(TimeSpan timeSpent)
        {
            if (_currConnection == null)
                return;

            _currConnection.LastFrameTime = timeSpent;
            _currConnection.LastFrameTimeSet = true;
            CallUpdateClientList();
        }
    }
}
