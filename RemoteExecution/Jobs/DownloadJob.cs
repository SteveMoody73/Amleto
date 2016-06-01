using System;
using System.Collections.Generic;
using System.IO;
using NLog;

namespace RemoteExecution.Jobs
{
    [Serializable]
    public class DownloadJob : Job
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        readonly string _remoteFile;
        readonly string _localFile;
        readonly long _size;
        readonly DateTime _modDate;

        public DownloadJob(string remoteFile, string localFile, long size, DateTime modDate)
        {
            _remoteFile = remoteFile;
            _localFile = localFile;
            _size = size;
            _modDate = modDate;
        }

        public override void ExecuteJob(MessageBack messageBack, Queue<Job> jobs)
        {
            string localPath = Path.Combine(ClientServices.GetClientDir(), "Content");
            string localFile = Path.Combine(localPath, _localFile);
            Directory.CreateDirectory(localPath);

            bool needToDownload = false;
            if (!File.Exists(localFile))
                needToDownload = true;
            else
            {
                FileInfo f = new FileInfo(localFile);
                if (f.LastWriteTimeUtc != _modDate || f.Length != _size)
                    needToDownload = true;
            }
            
            if (needToDownload)
            {
                try
                {
                    messageBack(0, "Downloading " + _remoteFile);
                    byte[] res = Server.GetFile(FileType.Absolute, _remoteFile);
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

                    FileInfo f = new FileInfo(localFile);
                    f.LastWriteTimeUtc = _modDate;
                    messageBack(0, "Saved at " + _localFile);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Unable to download job: " + _remoteFile);
                }
            }
        }
    }
}
