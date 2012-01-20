using System;
using System.Collections.Generic;
using System.IO;

namespace RemoteExecution.Jobs
{
    [Serializable]
    public class SetupJob : Job
    {
    	public override void ExecuteJob(MessageBack messageBack, Queue<Job> jobs)
        {
            messageBack(0,"Check files to setup...");
            Server.SetCurrentJob("Setting up client.");
            List<string> progs = Server.GetSetupFileList();
            List<string> plugins = Server.GetPluginFileList();
            List<string> config = Server.GetConfigFileList();
            lock (jobs)
            {
                foreach (string s in progs)
                {
                    if (!File.Exists(ClientServices.ClientDir + "\\" + ClientServices.ConfigName + "\\Program\\" + s))
                        jobs.Enqueue(new DownloadProgJob(s,false));
                }
                foreach (string s in plugins)
                {
                    if (!File.Exists(ClientServices.ClientDir + "\\" + ClientServices.ConfigName + "\\Plugin\\" + s))
                        jobs.Enqueue(new DownloadPluginJob(s, false));
                }
                foreach (string s in config)
                {
					if (s.ToUpper().EndsWith("LW11-64.CFG") ||
						s.ToUpper().EndsWith("LW11.CFG") || 
						s.ToUpper().EndsWith("LW10-64.CFG") ||
						s.ToUpper().EndsWith("LW10.CFG") || 						
						s.ToUpper().EndsWith("LW9.CFG") || 
						s.ToUpper().EndsWith("LW9-64.CFG") || 
						s.ToUpper().EndsWith("LW8.CFG") || 
						s.ToUpper().EndsWith("LW3.CFG") || 
						!File.Exists(ClientServices.ClientDir + "\\" + ClientServices.ConfigName + "\\Config\\" + s))
                        jobs.Enqueue(new DownloadConfigJob(s));
                }
                jobs.Enqueue(new ClientReadyJob());
            }
        }
    }
}
