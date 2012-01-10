using System;
using System.Collections.Generic;
using System.IO;

namespace RemoteExecution.Jobs
{
    [Serializable]
    public class DownloadJob : Job
    {
        string _remoteFile;
        string _localFile;
        long _size;
        DateTime _modDate;

        public DownloadJob(string remoteFile, string localFile, long size, DateTime modDate)
        {
            _remoteFile = remoteFile;
            _localFile = localFile;
            _size = size;
            _modDate = modDate;
        }

        public override void ExecuteJob(MessageBack messageBack, Queue<Job> jobs)
        {
            string baseDir = "" + Directory.GetParent(ClientServices.ClientDir + "\\Content\\" + _localFile);
            Directory.CreateDirectory(baseDir);

            bool needToDownload = false;
            if (!File.Exists(ClientServices.ClientDir + "\\Content\\" + _localFile))
                needToDownload = true;
            else
            {
                FileInfo f = new FileInfo(ClientServices.ClientDir + "\\Content\\" + _localFile);
                if (f.LastWriteTimeUtc != _modDate || f.Length != _size)
                    needToDownload = true;
            }
            
            if(needToDownload)
            {
                messageBack(0,"Downloading " + _remoteFile);
                byte[] res = Server.GetFile(FileType.Absolute, _remoteFile);
                FileStream stream = File.Create(ClientServices.ClientDir + "\\Content\\" + _localFile, res.Length);
                stream.Write(res, 0, res.Length);
                stream.Close();
                stream.Dispose();
                FileInfo f = new FileInfo(ClientServices.ClientDir + "\\Content\\" + _localFile);
                f.LastWriteTimeUtc=_modDate;
                messageBack(0,"Saved at " + _localFile);
            }
        }
    }
}
