using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace RemoteExecution.Jobs
{
    class DownloadSupportJob : Job
    {
        string _file;
        bool _force;

        public DownloadSupportJob(string file, bool force)
        {
            _file = file;
            _force = force;
        }

        public override void ExecuteJob(Job.MessageBack messageBack, Queue<Job> jobs)
        {
            messageBack(0, "Downloading setup prog " + _file);

            string localFolder = Path.Combine(ClientServices.GetClientDir(), ClientServices.ConfigName);
            localFolder = Path.Combine(localFolder, "Support");
            string localFile = Path.Combine(localFolder, _file);
            string destinationFolder = Path.GetDirectoryName(localFile);

            try
            {
                Directory.CreateDirectory(localFolder);
                if (destinationFolder != null)
                    Directory.CreateDirectory(destinationFolder);

                if (_force || !File.Exists(localFile))
                {
                    byte[] res = Server.GetFile(FileType.Support, _file);
                    FileStream stream = File.Create(localFile, res.Length);
                    stream.Write(res, 0, res.Length);
                    stream.Close();
                    stream.Dispose();
                }

                messageBack(0, "Saved at " + _file);
            }
            catch (Exception e)
            {
                Debug.WriteLine("DownloadSupportJob: " + e);
            }
        }
    }
}
