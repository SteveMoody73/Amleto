using System;
using System.Collections.Generic;
using System.IO;

namespace RemoteExecution.Jobs
{
    [Serializable]
    public class SetupJob : Job
    {
        private bool _forceDownload; 

        public SetupJob()
        {
            _forceDownload = false;
        }

        public SetupJob(bool force)
        {
            _forceDownload = force;
        }

        public override void ExecuteJob(MessageBack messageBack, Queue<Job> jobs)
        {
            messageBack(0, "Checking the LightWave setup on the Client");
            Server.SetCurrentJob("Setting up client");
            
            List<string> progs = Server.GetSetupFileList();
            List<string> support = Server.GetSupportFileList();
            List<string> plugins = Server.GetPluginFileList();
            List<string> config = Server.GetConfigFileList();
    	    List<string> extPlugins = Server.GetExtPluginFileList();

            lock (jobs)
            {
                foreach (string s in progs)
                    jobs.Enqueue(new DownloadProgJob(s, _forceDownload));

                foreach (string s in support)
                    jobs.Enqueue(new DownloadSupportJob(s, _forceDownload));

                foreach (string s in plugins)
                    jobs.Enqueue(new DownloadPluginJob(s, _forceDownload));
                
                foreach (string s in extPlugins)
                    jobs.Enqueue(new DownloadExtraPluginsJob(s, _forceDownload));
                
                foreach (string s in config)
                    jobs.Enqueue(new DownloadConfigJob(s));
                
                jobs.Enqueue(new ClientReadyJob());
            }
        }
    }
}
