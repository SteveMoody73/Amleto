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
            string localPath = Path.Combine(Path.Combine(ClientServices.GetClientDir(), ClientServices.ConfigName), "Plugins");
            string localFile = Path.Combine(localPath, _file);
            Directory.CreateDirectory(localPath);

            try
            {
                FileInfo remote = Server.GetFileInfo(FileType.Plugin, _file);

                bool needToDownload = false;
                if (!File.Exists(localFile) || _force)
                    needToDownload = true;
                else
                {
                    FileInfo local = new FileInfo(localFile);
                    if (remote.LastWriteTimeUtc != local.LastWriteTimeUtc || remote.Length != local.Length)
                        needToDownload = true;
                }

                if (needToDownload)
                {
                    messageBack(0, "Downloading plugin " + _file);
                    byte[] res = Server.GetFile(FileType.Plugin, _file);
                    FileStream stream = File.Create(localFile, res.Length);
                    stream.Write(res, 0, res.Length);
                    stream.Close();
                    stream.Dispose();
                    FileInfo local = new FileInfo(localFile);
                    local.LastWriteTimeUtc = remote.LastWriteTimeUtc;
                    messageBack(0, "Saved at " + _file);
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine("DownloadPluginJob: " + e);
            }
        }
    }
}
