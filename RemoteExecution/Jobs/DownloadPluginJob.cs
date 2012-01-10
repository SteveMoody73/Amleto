using System;
using System.Collections.Generic;
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
            string baseDir = Directory.GetParent(ClientServices.ClientDir + "\\" + ClientServices.ConfigName + "\\Plugin\\" + _file).FullName;
            Directory.CreateDirectory(baseDir);
            if (_force || !File.Exists(ClientServices.ClientDir + "\\" + ClientServices.ConfigName + "\\Plugin\\" + _file))
            {
                byte[] res = Server.GetFile(FileType.Plugin, _file);
                FileStream stream = File.Create(ClientServices.ClientDir + "\\" + ClientServices.ConfigName + "\\Plugin\\" + _file, res.Length);
                stream.Write(res, 0, res.Length);
                stream.Close();
                stream.Dispose();
            }
            messageBack(0,"Saved at " + _file);
        }
    }
}
