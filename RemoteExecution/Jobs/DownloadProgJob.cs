using System;
using System.Collections.Generic;
using System.IO;
using NLog;

namespace RemoteExecution.Jobs
{
    [Serializable]
    public class DownloadProgJob : Job
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        readonly string _file;
        readonly bool _force;

        public DownloadProgJob(string file,bool force)
        {
            _file = file;
            _force = force;
        }

        public override void ExecuteJob(MessageBack messageBack, Queue<Job> jobs)
        {
            string localFolder = Path.Combine(ClientServices.GetClientDir(), ClientServices.ConfigName);
            localFolder = Path.Combine(localFolder, "Program");
            string localFile = Path.Combine(localFolder, _file);
            string destinationFolder = Path.GetDirectoryName(localFile);

            try
            {
                Directory.CreateDirectory(localFolder);
                if (destinationFolder != null)
                    Directory.CreateDirectory(destinationFolder);

                FileInfo remote = Server.GetFileInfo(FileType.Program, _file);

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
                    messageBack(0, "Downloading setup prog " + _file);
                    byte[] res = Server.GetFile(FileType.Program, _file);
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
                    messageBack(0, "Saved at " + _file);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Unable to download file: " + _file);
            }
        }
    }
}
