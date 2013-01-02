using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace RemoteExecution.Jobs
{
    [Serializable]
    public class DownloadExtraPluginsJob : Job
    {
        string _file;
        bool _force;

        public DownloadExtraPluginsJob(string file, bool force)
        {
            _file = file;
            _force = force;
        }

        public override void ExecuteJob(MessageBack messageBack, Queue<Job> jobs)
        {
            messageBack(0, "Downloading plugin " + Path.GetFileName(_file));

            try
            {
                string remoteFileName = Path.GetFileName(_file);
                string localPath = Path.Combine(Path.Combine(ClientServices.GetClientDir(), ClientServices.ConfigName), "ExtPlugins");
                string localFile = Path.Combine(localPath, remoteFileName);
                Directory.CreateDirectory(localPath);
            
                if (_force || !File.Exists(localFile))
                {
                    byte[] res = Server.GetFile(FileType.Absolute, _file);
                    FileStream stream = File.Create(localFile, res.Length);
                    stream.Write(res, 0, res.Length);
                    stream.Close();
                    stream.Dispose();
                }
                messageBack(0, "Saved at " + remoteFileName);
            }
            catch (Exception e)
            {
                Debug.WriteLine("DownloadExtraPluginsJob: " + e);
            }
        }
    }
}
