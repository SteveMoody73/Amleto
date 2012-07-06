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
        Config
    }

    public class ServerServices : MarshalByRefObject
    {
		public delegate void StatusClientChange(List<ClientConnection> clients);
		public delegate void StatusProjectChange(List<RenderProject> projects);
		public delegate void StatusStringChange(string msg);
		public delegate void StatusStringStringChange(string op, string msg);
		public delegate void StatusFinishedFrameChange(FinishedFrame frame);

		public static event StatusClientChange ClientStatus;
		public static event StatusProjectChange ProjectStatus;
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

        public void RegisterClient(string hostName, string ipAddress, ProcessPriorityClass priority, int ptrSize)
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

                _currConnection = new ClientConnection(hostName, ipAddress, nextInstance, priority, ptrSize);
                _clients.Add(_currConnection);
            }
            AddMessage(0, "Node " + _currConnection.HostName + " (" + ipAddress + ")" + ":" + _currConnection.Instance + " connected (" + (ptrSize * 8) + "-bit).");
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
                        foreach (RenderProject t in Projects)
                        {
                        	if (t.Paused == false && t.Config == _currConnection.Config && t.HasFreeJobs())
                        	{
								if (!t.StartTimeSet)
								{
									t.StartTimeSet = true;
									t.StartTime = DateTime.Now;
								}
                        		_currConnection.CurrentRender = t;
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
                    }
                    else if (_currConnection.CurrentRender != null && _currConnection.Jobs.Count == 0)
                    {
                        if (_currConnection.CurrentRender.Paused == false && _currConnection.CurrentRender.HasFreeJobs())
                        {
                            AddMessage(4, "Node " + _currConnection.HostName + " (" + _currConnection.IPAddress + ")" + ":" + _currConnection.Instance + " getting frame(s) job");
                            _currConnection.Jobs.Add(_currConnection.CurrentRender.GetRenderJob(_currConnection.Id, _currConnection.Instance));
                        }
                    }
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

        public byte[] GetFile(FileType t, string filename)
        {
            if (_currConnection == null)
                return new byte[] { };
            string path = "";
            switch (t)
            {
                case FileType.Absolute:
                    path = "";
                    break;
                case FileType.Config:
                    path = Configs[_currConnection.Config].ServerConfigPath + "\\";
                    break;
                case FileType.Program:
                    path = Configs[_currConnection.Config].ServerProgramPath + "\\";
                    break;
                case FileType.Plugin:
                    path = Configs[_currConnection.Config].ServerPluginPath + "\\";
                    break;
            }

            if (!File.Exists(path + filename))
                return null;
            return File.ReadAllBytes(path + filename);
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
                catch
                {
                    return;
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
                    if (Configs[newConfig].PtrSize > node.PtrSize)
                        return;
                    node.Config = newConfig;
                    node.Jobs.Add(new ChangeConfigJob(Configs[newConfig].Name));
                }
                catch
                {
                    return;
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
                catch
                {
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

            DirectoryInfo dir = new DirectoryInfo(Configs[_currConnection.Config].ServerProgramPath);
            try
            {
                foreach (FileInfo f in dir.GetFiles())
                {
                    if (f.Name.ToLower() == "hub.exe" ||
                        f.Name.ToLower() == "license.key" ||
                        f.Name.ToLower() == "lsed.exe" ||
                        f.Name.ToLower() == "lsid.exe" ||
                        f.Name.ToLower() == "modeler.exe" ||
                        f.Name.ToLower() == "startlwsn_node.bat" ||
                        f.Name.ToLower() == "stoplwsn_node.bat")
                        continue;
                    res.Add(f.Name);
                }
            }
			catch (Exception ex)
			{
				Debug.WriteLine("Getting setup files: " + ex);
			}


            // LW 10? Then we must also copy the support directory
            if (Directory.Exists(Configs[_currConnection.Config].ServerProgramPath + "\\..\\Support"))
            {
                int prefixLength=(Configs[_currConnection.Config].ServerProgramPath + "\\").Length;
                Queue<string> dirs = new Queue<string>();
                dirs.Enqueue(Configs[_currConnection.Config].ServerProgramPath + "\\..\\Support");
                while (dirs.Count > 0)
                {
                    string currentDir=dirs.Dequeue();
                    dir = new DirectoryInfo(currentDir);
                    foreach (var i in dir.GetDirectories())
                        dirs.Enqueue(currentDir + "\\" + i.Name);
                    foreach (var i in dir.GetFiles())
                    {
                        string file = currentDir + "\\" + i.Name;
                        file = file.Substring(prefixLength);
                        res.Add(file);
                    }
                }
            }
            return res;
        }

        public List<string> GetPluginFileList()
        {
            return GetPluginFileList(Configs[_currConnection.Config].ServerPluginPath);
        }

        private List<string> GetPluginFileList(string path)
        {
            List<string> res = new List<string>();

            if (_currConnection == null)
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
                    res.Add(f.FullName.Substring(Configs[_currConnection.Config].ServerPluginPath.Length));
            }
			catch (Exception ex)
			{
				Debug.WriteLine("Getting plugin file list: " + ex);
			}


            return res;
        }

        public List<string> GetConfigFileList()
        {
            List<string> res = new List<string>();

            if (_currConnection == null)
                return res;

            DirectoryInfo dir = new DirectoryInfo(Configs[_currConnection.Config].ServerConfigPath);
            foreach (FileInfo f in dir.GetFiles("*.CFG"))
                res.Add(f.FullName.Substring(Configs[_currConnection.Config].ServerConfigPath.Length));

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
            return Configs[_currConnection.Config].ServerPluginPath;
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
                    catch
                    {
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
        }

        private static void RemoveProject(RenderProject project)
        {
            while (FinishedProjects.Count > 50)
                FinishedProjects.RemoveAt(0);
            GC.Collect();
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
                Projects.Add(project);
            }
            CallUpdateProjectList();
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
                    Projects.Add(project);
                }
            }
            CallUpdateProjectList();
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
                    catch
                    {
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

            while (_oldMessages.Count > 300)
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
                catch
                {
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
                    catch
                    {
                        ProjectStatus -= (StatusProjectChange)d;
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
                    FinishedProjects.Add(proj);
                }
            }
            if (proj != null)
                CallUpdateProjectList();
        }

        public static void PauseClient(int id)
        {
            lock (_clients)
            {
                try
                {
                    ReturnClient(id).Paused = true;
                }
                catch
                {
                    return;
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
                catch
                {
                    return;
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
                catch
                {
                    return false;
                }
            }
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
                catch
                {
                    return null;
                }
            }
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
    }
}
