using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using NLog;

namespace RemoteExecution.Jobs
{
    [Serializable]
    public class DownloadExtraPluginsJob : Job
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        readonly string _file;
        readonly bool _force;

        public DownloadExtraPluginsJob(string file, bool force)
        {
            _file = file;
            _force = force;
        }

        public override void ExecuteJob(MessageBack messageBack, Queue<Job> jobs)
        {
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
                {
                    messageBack(0, "Downloading plugin " + Path.GetFileName(_file));
                    byte[] res = Server.GetFile(FileType.Absolute, _file);
                    Directory.CreateDirectory(Path.GetDirectoryName(localFile));
                    if (res.Length > 0)
                    {
                        FileStream stream = File.Create(localFile, res.Length);
                        stream.Write(res, 0, res.Length);
                        stream.Close();
                        stream.Dispose();
                    }
                    else
                    {
                        // Create empty file
                        FileStream stream = File.Create(localFile);
                        stream.Write(res, 0, res.Length);
                        stream.Close();
                        stream.Dispose();
                    }
                    FileInfo local = new FileInfo(localFile);
                    local.LastWriteTimeUtc = remote.LastWriteTimeUtc;
                    messageBack(0, "Saved at " + remoteFileName);
                }
            }
            catch (Exception ex)
            {
                logger.ErrorException("Unable to download plugin: " + _file, ex);
            }
        }
    }
}
