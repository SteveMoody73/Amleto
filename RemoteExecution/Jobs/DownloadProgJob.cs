using System;
using System.Collections.Generic;
using System.IO;

namespace RemoteExecution.Jobs
{
    [Serializable]
    public class DownloadProgJob : Job
    {
        string _file;
        bool _force;

        public DownloadProgJob(string file,bool force)
        {
            _file = file;
            _force = force;
        }

        public override void ExecuteJob(MessageBack messageBack, Queue<Job> jobs)
        {
            messageBack(0,"Downloading setup prog " + _file);

            Directory.CreateDirectory(ClientServices.ClientDir + "\\" + ClientServices.ConfigName + "\\Program\\");
            if (_file.StartsWith("..")) // A relative path!
            {
                int p = _file.LastIndexOf("\\");
                string d = ClientServices.ClientDir + "\\" + ClientServices.ConfigName + "\\Program\\" + _file.Substring(0, p);
                Directory.CreateDirectory(d);
            }
            if (_force || !File.Exists(ClientServices.ClientDir + "\\" + ClientServices.ConfigName + "\\Program\\" + _file))
            {
                byte[] res = Server.GetFile(FileType.Program,_file);
                FileStream stream = File.Create(ClientServices.ClientDir + "\\" + ClientServices.ConfigName + "\\Program\\" + _file, res.Length);
                stream.Write(res, 0, res.Length);
                stream.Close();
                stream.Dispose();
            }

            messageBack(0,"Saved at " + _file);
        }
    }
}
