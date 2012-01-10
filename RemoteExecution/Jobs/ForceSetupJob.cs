using System;
using System.Collections.Generic;


namespace RemoteExecution.Jobs
{
    [Serializable]
    public class ForceSetupJob : Job
    {
    	public override void ExecuteJob(MessageBack messageBack,Queue<Job> jobs)
        {
            messageBack(0,"Redo-setup...");
            Server.SetCurrentJob("Setting up client.");
            
			List<string> progs=Server.GetSetupFileList();
            List<string> plugins = Server.GetPluginFileList();
            List<string> config = Server.GetConfigFileList();

            lock (jobs)
            {
                foreach (string s in progs)
                    jobs.Enqueue(new DownloadProgJob(s, true));
                foreach (string s in plugins)
                    jobs.Enqueue(new DownloadPluginJob(s, true));
                foreach (string s in config)
                    jobs.Enqueue(new DownloadConfigJob(s));
                jobs.Enqueue(new ClientReadyJob());
            }
        }
    }
}
