using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Remoting.Channels.Tcp;
using Microsoft.Win32;
using RemoteExecution.Jobs;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Activation;
using System.IO;

namespace RemoteExecution
{
    public class ClientServices
    {
		public delegate void StatusStringChange(string msg);
		public static event StatusStringChange MessageConsumer;

		private static bool _isWorking;
        private static bool _isService;

    	public static Process CurrentRenderProcess { get; set; }
		public static ClientServices CurrentInstance { get; set; }
		public static List<string> OldMessages { get; set; }

		public static ProcessPriorityClass RenderPriority { get; set; }
		public static string ClientDir { get; set; }
		public static string ServerHost { get; set; }
		public static string LogFile { get; set; }
		public static string ConfigName { get; set; }
		public static bool AutoServerFinder { get; set; }
		public static bool IsRunning { get; set; }
		public static int ServerPort { get; set; }
		public static int NumThreads { get; set; }
		public static int MemorySegment { get; set; }

        ServerServices _server;
        Queue<Job> _jobs = new Queue<Job>();
        Thread _jobConsumer;
        Thread _jobPumper;
        bool _setupToDo = true;
        bool _readyToStart;
        Stopwatch _setupTime = new Stopwatch();
        TcpChannel _channel;
        object _lockCheckWorking = new object();

        public ClientServices()
        {
            CurrentInstance = this;
        }

    	static ClientServices()
    	{
    		OldMessages = new List<string>();
    		RenderPriority = ProcessPriorityClass.Normal;
    		ServerHost = "localhost";
    		LogFile = "";
    		ConfigName = "";
    		AutoServerFinder = true;
    		IsRunning = true;
    		ServerPort = 2080;
            NumThreads = Environment.ProcessorCount;
    		MemorySegment = 128;
    		CurrentInstance = null;
    		CurrentRenderProcess = null;
    	}

    	public void StartService()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = "'";

    	    _isService = true;

            RestoreSettings();

            IsRunning = true;
            IsWorking = false;

            _setupToDo = true;
            _readyToStart = false;

            _setupTime.Reset();
            _setupTime.Start();

            if (_jobConsumer != null)
                _jobConsumer.Abort();
			_jobConsumer = new Thread(JobsExecuter);
            _jobConsumer.IsBackground = true;
            _jobConsumer.Start();

            if (_jobPumper != null)
                _jobPumper.Abort();
            _jobPumper = new Thread(JobsPump);
            _jobPumper.IsBackground = true;
            _jobPumper.Start();

            if (AutoServerFinder)
            {
                AddMessage(0,"Searching for Amleto server...");
                ThreadPool.QueueUserWorkItem(SearchServer);
            }
            else
            {
                AddMessage(0,"Trying to connect to server " + ServerHost + " on port " + ServerPort);
                ThreadPool.QueueUserWorkItem(ConnectToServer);
            }
        }

		/// <summary>
		/// Gets the configuration settings from the registry
		/// </summary>
        public static void RestoreSettings()
        {
            try
            {
                if (_isService)
                    ClientDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Amleto\\Cache";
                else
                    ClientDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Amleto\\Cache";

            	var openSubKey = Registry.CurrentUser.OpenSubKey("Software");
            	if (openSubKey != null)
            	{
            		RegistryKey key = openSubKey.OpenSubKey("Amleto3");
            
            		if (key == null)
            			return;
                
            		if (key.GetValue("ClientDir") != null)
            			ClientDir = (string)key.GetValue("ClientDir");
                
            		AutoServerFinder = true;
                
            		if (key.GetValue("AutoServerFinder") != null && ((string)key.GetValue("AutoServerFinder")) == "False")
            			AutoServerFinder = false;

            		ServerHost = "localhost";
            		ServerPort = 2080;
                
            		if (key.GetValue("ServerHost") != null)
            			ServerHost = (string) key.GetValue("ServerHost");
                
            		if (key.GetValue("ServerPort") != null)
            			ServerPort = (int)key.GetValue("ServerPort");
                
            		RenderPriority = ProcessPriorityClass.Normal;
                
            		if(key.GetValue("RenderPriority") != null)
            			RenderPriority = (ProcessPriorityClass)Enum.Parse(typeof(ProcessPriorityClass), (string)key.GetValue("RenderPriority"));
                
            		if (key.GetValue("ClientLogFile") != null)
            			LogFile = (string)key.GetValue("ClientLogFile");
                
            		if (key.GetValue("NumberThreads") != null)
            			NumThreads = (int)key.GetValue("NumberThreads");
                
            		if (key.GetValue("MemorySegment") != null)
            			MemorySegment = (int)key.GetValue("MemorySegment");
                
            		key.Close();
            	}
            }
            catch (Exception ex)
            {
				Debug.WriteLine("Error while restoring settings: " + ex);
            }
        }

        /// <summary>
        /// Saves the configuration too the registry 
        /// </summary>
        public static void SaveSettings()
        {
            try
            {
            	var openSubKey = Registry.CurrentUser.OpenSubKey("Software", true);
            	if (openSubKey != null)
            	{
            		RegistryKey key = openSubKey.CreateSubKey("Amleto3");
            		if (key != null)
            		{
            			key.SetValue("ClientDir", ClientDir);
            			key.SetValue("AutoServerFinder", AutoServerFinder);
            			key.SetValue("ServerHost", ServerHost);
            			key.SetValue("ServerPort", ServerPort);
            			key.SetValue("RenderPriority", RenderPriority.ToString());
            			key.SetValue("ClientLogFile", LogFile);
            			key.SetValue("NumberThreads", NumThreads);
            			key.SetValue("MemorySegment", MemorySegment);
            			key.Close();
            		}
            	}
            }
            catch (Exception ex)
			{
				Debug.WriteLine("Error while saving settings: " + ex);
			}
		}


        public static void AddMessage(int icon,string msg)
        {
            string fullMsg = icon + "|" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "|" + msg;
            if (LogFile != "")
            {
                try
                {
                    File.AppendAllText(LogFile, fullMsg+"\r\n");
                }
                catch (Exception ex)
                {
					Debug.WriteLine("Unable to write to log file: " + ex);
                }
            }

			while (OldMessages.Count > 300)
                OldMessages.RemoveAt(0);
            
			OldMessages.Add(fullMsg);
            
			if (MessageConsumer != null)
            {
                foreach (Delegate d in MessageConsumer.GetInvocationList())
                {
                    try
                    {
                        d.DynamicInvoke(new object[] { fullMsg });
                    }
					catch (Exception ex)
					{
						Debug.WriteLine("Delegate error: " + ex);
					}
                }
            }
        }

        static public void SetRenderProcess(Process p)
        {
            CurrentRenderProcess = p;
        }

        static public void KillRenderProcess()
        {
            try
            {
                if (CurrentRenderProcess != null)
                    CurrentRenderProcess.Kill();
            }
			catch (Exception ex)
			{
				Debug.WriteLine("Error Killing render process: " + ex);
			}

            try
            {
                CurrentRenderProcess = null;
            }
			catch (Exception ex)
			{
				Debug.WriteLine("Error setting null render process: "  + ex);
			}
        }

        static public void SetRenderProcessPriority(ProcessPriorityClass newPriority)
        {
            if (CurrentRenderProcess != null)
                CurrentRenderProcess.PriorityClass = newPriority;
            RenderPriority = newPriority;

            // Now set as default
            try
            {
            	var openSubKey = Registry.CurrentUser.OpenSubKey("Software", true);
            	if (openSubKey != null)
            	{
            		RegistryKey key = openSubKey.CreateSubKey("Amleto3");
            		if (key != null)
            		{
   						key.SetValue("RenderPriority", RenderPriority.ToString());
            			key.Close();
            		}
            	}
            }
            catch (Exception ex)
			{
				Debug.WriteLine("Error setting render process priority: " + ex);
			}

        }

        private void SearchServer(object obj)
        {
            BroadcastFinder finder = new BroadcastFinder();
            ServerPort = finder.Port;
            ServerHost = finder.Server;
            if (ServerHost == "")
            {
                AddMessage(1, "Amleto server has not been found.");
                AddMessage(1, "Check that your firewall allows access to port 61111 on the server");
                AddMessage(1, "and the server is currently running");
            }
            else
                AddMessage(0,"Found Amleto server at " + ServerHost + " on port " + ServerPort);

            AddMessage(0,"Trying to connect to the server.");
            ThreadPool.QueueUserWorkItem(ConnectToServer);
        }

        public bool IsWorking
        {
            get
            {
                lock (_lockCheckWorking)
                {
                    return _isWorking;
                }
            }

            set
            {
                lock (_lockCheckWorking)
                {
                    _isWorking = value;
                }
            }
        }

        private string GetIP()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }

	   	private void ConnectToServer(object obj)
        {
            if (ServerHost == "" || AutoServerFinder)
            {
                BroadcastFinder finder = new BroadcastFinder();
                ServerPort = finder.Port;
                ServerHost = finder.Server;
            }

            if (_channel == null)
            {
                _channel = new TcpChannel();
                ChannelServices.RegisterChannel(_channel, false);
            }

			for (int i = 0; i < 10; i++)
            {
                try
                {
                    _server = (ServerServices)Activator.CreateInstance(typeof(ServerServices), null, new object[] { new UrlAttribute("tcp://" + ServerHost + ":" + ServerPort) });
                    if (_server.IsWorking())
                        break;
                    Thread.Sleep(100);
                }
                catch(Exception ex)
                {
                    _server = null;
					Debug.WriteLine("Could not connect to server: " + ex);
                }
            }

			if (_server == null)
            {
                if (_setupToDo)
                    AddMessage(1,"Cannot connect to the server. Will retry in 5 sec.");
                
				Thread.Sleep(5000);
                ThreadPool.QueueUserWorkItem(ConnectToServer);
            }
            else
            {
                _server.RegisterClient(Environment.MachineName, GetIP(), RenderPriority, IntPtr.Size);
                AddMessage(0,"Connected to the server " + ServerHost + " on port " + ServerPort + ".");
                try
                {
                    if (_setupToDo)
                        _server.SetCurrentJob("Starting up");
                }
                catch
                {
                    _server = null;
                    if (_setupToDo)
                        AddMessage(1, "Cannot connect to the server. Will retry in 5 sec.");
                    Thread.Sleep(5000);
                    ThreadPool.QueueUserWorkItem(ConnectToServer);
                }
            }
        }

        private void JobsPump()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = "'";

            while (IsRunning)
            {
                RetreiveJobs();
                Thread.Sleep(500);
            }
        }

        private void RetreiveJobs()
        {
            if (_server == null)
                return;
            try
            {
                if (_server.KeepAlive() == false)
                {
                    try
                    {
                        _server.Unregister();
                    }
                    catch (Exception ex)
                    {
						Debug.WriteLine("Error Unregistering server: " + ex);
                    }

                    _server = null;
                    ThreadPool.QueueUserWorkItem(ConnectToServer);
                    return;
                }

                // Checks if we have any priority jobs to execute. If yes do it ASAP
                if (_server.HasPriorityJobs())
                {
                    List<Job> newJobs = _server.GetPriorityJobs();
                    foreach (Job j in newJobs)
                    {
                        j.SetServer(_server);
                        j.ExecuteJob(AddMessage, _jobs);
                    }
                }

                lock (_jobs)
                {
                    if (_setupToDo)
                        DoSetup();
                    else if (!_readyToStart && !_setupToDo && _jobs.Count == 0)
                    {
                        AddMessage(0,"Client ready.");
                        _setupTime.Stop();
                        AddMessage(0,"Starup and setup took " + _setupTime.Elapsed.TotalSeconds + " sec(s).");
                        _readyToStart = true;
                        //server.SetCurrentJob("");
                    }
                    else if (_jobs.Count > 0)
                        return;
                }

                if (IsWorking)
                    return;
                // Checks if we have new jobs to do
                if (_server.HasJobs())
                {
                    // If yes, lock the jobs queue and add the new one
                    lock (_jobs)
                    {
                        List<Job> newJobs = _server.GetJobs();
                        foreach (Job j in newJobs)
                            _jobs.Enqueue(j);
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Retrieve Jobs: " + ex);
                try
                {
                	if (_server != null) _server.KeepAlive();
                }
                catch
                {
                    _server = null;
                    AddMessage(1,"Server disapeared");
                    ThreadPool.QueueUserWorkItem(ConnectToServer);
                }
            }
        }

        private void JobsExecuter()
        {
            bool needToSleep = true;

        	Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = "'";

            while (IsRunning)
            {
                if (needToSleep)
                    Thread.Sleep(100);
                needToSleep = false;
               
				Job jobToDo = null;
                lock (_jobs)
                {
                    if (_jobs.Count > 0)
                        jobToDo = _jobs.Dequeue();
                    else
                        needToSleep = true;
                }
                if (jobToDo != null)
                {
                    IsWorking = true;
                    try
                    {
                        jobToDo.SetServer(_server);
                        jobToDo.ExecuteJob(AddMessage,_jobs);

                        lock (_jobs)
                        {
                            // Checks if we have new jobs to do
                            if (_jobs.Count == 0 && _server.HasJobs())
                            {
                            // If yes, lock the jobs queue and add the new one
                                List<Job> newJobs = _server.GetJobs();
                                foreach (Job j in newJobs)
                                    _jobs.Enqueue(j);
                            }

                            if (_jobs.Count == 0)
                                IsWorking = false;
                        }
                    }
                    catch
                    {
                        IsWorking = false;
                    }
                }
            }
        }

        public static void ReDoSetup()
        {
            if (CurrentInstance._server.IsFirstClient())
                CurrentInstance._jobs.Enqueue(new SetupJob());
            else // Wait the first client...
                CurrentInstance._jobs.Enqueue(new WaitFirstJob());
        }

        private void DoSetup()
        {
            ConfigName = _server.GetConfigName();
            _setupToDo = false;
            if (_server.IsFirstClient())
                _jobs.Enqueue(new SetupJob());
            else // Wait the first client...
                _jobs.Enqueue(new WaitFirstJob());
        }

        public static void Shutdown()
        {
            IsRunning = false;
            KillRenderProcess();
            Thread.Sleep(600);
            SaveSettings();
            try
            {
                CurrentInstance._server.Unregister();
            }
            catch (Exception ex)
            {
				Debug.WriteLine("Error Unregistering server: " + ex);
			}
        }

        public static void ChangePriority()
        {
            if (CurrentInstance._server != null)
                CurrentInstance._server.ChangeClientPriority(RenderPriority);
        }
    }
}
