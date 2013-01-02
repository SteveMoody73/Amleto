using System;
using System.Collections.Generic;
using System.IO;

namespace RemoteExecution.Jobs
{
    [Serializable]
    public class DownloadConfigJob : Job
    {
        string _file;

        public DownloadConfigJob(string file)
        {
            _file = file;
        }

        public override void ExecuteJob(MessageBack messageBack, Queue<Job> jobs)
        {
            messageBack(0, "Downloading config " + _file);
            string localPath = Path.Combine(Path.Combine(ClientServices.GetClientDir(), ClientServices.ConfigName), "Config");
            string localFile = Path.Combine(localPath, _file);

            Directory.CreateDirectory(localPath);
			if (!File.Exists(localFile))
            {
                byte[] res = Server.GetFile(FileType.Config, _file);
                FileStream stream = File.Create(localFile, res.Length);
                stream.Write(res, 0, res.Length);
                stream.Close();
                stream.Dispose();

				if (ClientServices.IsMainConfig(localFile))
                {
                    string[] lines = File.ReadAllLines(localFile);
                    StreamWriter writer = new StreamWriter(localFile);
                    foreach (string line in lines)
                    {
                        if (line.StartsWith("DefaultSegmentMemory"))
                            writer.WriteLine("DefaultSegmentMemory " + (ClientServices.Settings.MemorySegment * 1024 * 1024));
                        else if (line.StartsWith("RenderThreads"))
                            writer.WriteLine("RenderThreads "+ClientServices.Settings.NumThreads);
                        else
                            writer.WriteLine(line);
                    }
                    writer.Close();
                    writer.Dispose();
                }
            }
            messageBack(0,"Saved at " + _file);
        }
    }
}
