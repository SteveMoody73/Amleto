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
            List<string> support = Server.GetSupportFileList();
            List<string> plugins = Server.GetPluginFileList();
            List<string> config = Server.GetConfigFileList();
    	    List<string> extPlugins = Server.GetExtPluginFileList();

            lock (jobs)
            {
                string localPath = Path.Combine(ClientServices.GetClientDir(), ClientServices.ConfigName);
                foreach (string s in progs)
                {
                    if (!File.Exists(Path.Combine(Path.Combine(localPath, "Program"), s)))
                        jobs.Enqueue(new DownloadProgJob(s,false));
                }
                foreach (string s in support)
                {
                    if (!File.Exists(Path.Combine(Path.Combine(localPath, "Support"), s)))
                        jobs.Enqueue(new DownloadSupportJob(s, false));
                }
                foreach (string s in plugins)
                {
                    if (!File.Exists(Path.Combine(Path.Combine(localPath, "Plugins"), s)))
                        jobs.Enqueue(new DownloadPluginJob(s, false));
                }
                foreach (string s in extPlugins)
                {
                    string file = Path.GetFileName(s);
                    if (!File.Exists(Path.Combine(Path.Combine(localPath, "ExtPlugins"), file)))
                        jobs.Enqueue(new DownloadExtraPluginsJob(s, false));
                }
                foreach (string s in config)
                {
                    if (!File.Exists(Path.Combine(Path.Combine(localPath, "Config"), s)))
                        jobs.Enqueue(new DownloadConfigJob(s));
                }
                jobs.Enqueue(new ClientReadyJob());
            }
        }
    }
}
