using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using NLog;

namespace RemoteExecution.Jobs
{
    [Serializable]
    public class DownloadConfigJob : Job
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        readonly string _file;

        public DownloadConfigJob(string file)
        {
            _file = file;
        }

        public override void ExecuteJob(MessageBack messageBack, Queue<Job> jobs)
        {
            string localPath = Path.Combine(Path.Combine(ClientServices.GetClientDir(), ClientServices.ConfigName), "Config");
            string localFile = Path.Combine(localPath, _file);

            Directory.CreateDirectory(localPath);
            FileInfo remote = Server.GetFileInfo(FileType.Config, _file);

            bool needToDownload = false;
            if (!File.Exists(localFile))
                needToDownload = true;
            else
            {
                FileInfo local = new FileInfo(localFile);
                if (remote.LastWriteTimeUtc != local.LastWriteTimeUtc || remote.Length != local.Length)
                    needToDownload = true;
            }

            if (needToDownload)
            {
                try
                {
                    messageBack(0, "Downloading config " + _file);

                    byte[] res = Server.GetFile(FileType.Config, _file);
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

				    if (ClientServices.IsMainConfig(localFile))
                    {
                        string[] lines = File.ReadAllLines(localFile);
                        StreamWriter writer = new StreamWriter(localFile);
                        foreach (string line in lines)
                        {
                            if (line.StartsWith("DefaultSegmentMemory"))
                                writer.WriteLine("DefaultSegmentMemory " + (ClientServices.Settings.MemorySegment * 1024 * 1024));
                            else if (line.StartsWith("RenderThreads"))
                                writer.WriteLine("RenderThreads " + ClientServices.Settings.NumThreads);
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
                        writer.Close();
                        writer.Dispose();
                    }
                    FileInfo local = new FileInfo(localFile);
                    local.LastWriteTimeUtc = remote.LastWriteTimeUtc;
                    messageBack(0, "Saved at " + _file);
                }
                catch (Exception ex)
                {
                    logger.ErrorException("Unable to save config file: " + _file, ex);
                }
            }
        }
    }
}
