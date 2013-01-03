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

                FileInfo remote = Server.GetFileInfo(FileType.Absolute, _file);

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
            
                if (_force || !File.Exists(localFile))
                {
                    byte[] res = Server.GetFile(FileType.Absolute, _file);
                    FileStream stream = File.Create(localFile, res.Length);
                    stream.Write(res, 0, res.Length);
                    stream.Close();
                    stream.Dispose();
                    FileInfo local = new FileInfo(localFile);
                    local.LastWriteTimeUtc = remote.LastWriteTimeUtc;
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
