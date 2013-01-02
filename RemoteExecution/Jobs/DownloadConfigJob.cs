using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                try
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
				    else if (localFile.ToUpper().Contains("LWEXT"))
				    {
				        string[] lines = File.ReadAllLines(localFile);
                        StreamWriter writer = new StreamWriter(localFile);
                        foreach (string line in lines)
                        {
                            if (line.ToUpper().Contains("MODULE \"") || line.StartsWith("  Module \""))
                            {
                                // Get the original plugin filename path and replace it with the new path
                                string originalPlugin = line.Replace(@"\\", @"\").Trim();
                                originalPlugin = originalPlugin.Substring(originalPlugin.IndexOf("\"") + 1);
                                originalPlugin = originalPlugin.Replace('\"', ' ').Trim();
                                string name = Path.GetFileName(originalPlugin);
                                string newPlugin = Path.Combine(ClientServices.GetClientDir(), ClientServices.ConfigName);
                                newPlugin = Path.Combine(Path.Combine(newPlugin, "ExtPlugins"), name);
                                writer.WriteLine("  Module \"" + newPlugin.Replace(@"\", @"\\") + "\"");
                            }
                            else
                                writer.WriteLine(line);
                        }				        
				    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("DownloadConfigJob: " + e);
                }
            }
            messageBack(0,"Saved at " + _file);
        }
    }
}
