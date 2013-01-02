using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace RemoteExecution.Jobs
{
    [Serializable]
    public class DownloadPluginJob : Job
    {
        string _file;
        bool _force;

        public DownloadPluginJob(string file,bool force)
        {
            _file = file;
            _force = force;
        }

        public override void ExecuteJob(MessageBack messageBack, Queue<Job> jobs)
        {
            messageBack(0,"Downloading plugin " + _file);
            string localPath = Path.Combine(Path.Combine(ClientServices.GetClientDir(), ClientServices.ConfigName), "Plugin");
            string localFile = Path.Combine(localPath, _file);
            Directory.CreateDirectory(localPath);

            try
            {
                if (_force || !File.Exists(localFile))
                {
                    byte[] res = Server.GetFile(FileType.Plugin, _file);
                    FileStream stream = File.Create(localFile, res.Length);
                    stream.Write(res, 0, res.Length);
                    stream.Close();
                    stream.Dispose();
                }
                messageBack(0, "Saved at " + _file);
            }
            catch(Exception e)
            {
                Debug.WriteLine("DownloadPluginJob: " + e);
            }
        }
    }
}
