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
            messageBack(0,"Downloading config " + _file);
            string baseDir = Directory.GetParent(ClientServices.ClientDir + "\\" + ClientServices.ConfigName + "\\Config\\" + _file).FullName;
            Directory.CreateDirectory(baseDir);
            if (_file.ToUpper().EndsWith("LW10-64.CFG") || _file.ToUpper().EndsWith("LW10.CFG") || _file.ToUpper().EndsWith("LW9.CFG") || _file.ToUpper().EndsWith("LW9-64.CFG") || _file.ToUpper().EndsWith("LW8.CFG") || _file.ToUpper().EndsWith("LW3.CFG") || !File.Exists(ClientServices.ClientDir + "\\" + ClientServices.ConfigName + "\\Config\\" + _file))
            {
                byte[] res = Server.GetFile(FileType.Config, _file);
                FileStream stream = File.Create(ClientServices.ClientDir + "\\" + ClientServices.ConfigName + "\\Config\\" + _file, res.Length);
                stream.Write(res, 0, res.Length);
                stream.Close();
                stream.Dispose();

                if (_file.ToUpper().EndsWith("LW10-64.CFG") || _file.ToUpper().EndsWith("LW10.CFG") || _file.ToUpper().EndsWith("LW9.CFG") || _file.ToUpper().EndsWith("LW9-64.CFG") || _file.ToUpper().EndsWith("LW8.CFG") || _file.ToUpper().EndsWith("LW3.CFG"))
                {
                    string[] lines = File.ReadAllLines(ClientServices.ClientDir + "\\" + ClientServices.ConfigName + "\\Config\\" + _file);
                    StreamWriter writer = new StreamWriter(ClientServices.ClientDir + "\\" + ClientServices.ConfigName + "\\Config\\" + _file);
                    foreach (string line in lines)
                    {
                        if (line.StartsWith("DefaultSegmentMemory"))
                            writer.WriteLine("DefaultSegmentMemory " + (ClientServices.MemorySegment * 1024 * 1024));
                        else if (line.StartsWith("RenderThreads"))
                            writer.WriteLine("RenderThreads "+ClientServices.NumThreads);
                        else
                            writer.WriteLine(line);
                    }
                    writer.Close();
                    writer.Dispose();
                }
                else if (_file.ToUpper().EndsWith("LWEXT10-64.CFG") || _file.ToUpper().EndsWith("LWEXT10.CFG") || _file.ToUpper().EndsWith("LWEXT9.CFG") || _file.ToUpper().EndsWith("LWEXT9-64.CFG") || _file.ToUpper().EndsWith("LWEXT8.CFG") || _file.ToUpper().EndsWith("LWEXT3.CFG"))
                {
                    string serverPluginPath = Server.GetPluginPath();

                    string[] lines = File.ReadAllLines(ClientServices.ClientDir + "\\" + ClientServices.ConfigName + "\\Config\\" + _file);
                    StreamWriter writer = new StreamWriter(ClientServices.ClientDir + "\\" + ClientServices.ConfigName + "\\Config\\" + _file);
                    foreach (string line in lines)
                    {
                        if (line.ToUpper().Contains("MODULE \"") || line.StartsWith("  Module \""))
                        {
                            string nline = line.Replace(@"\\", @"\");

                            if (!serverPluginPath.EndsWith("\\"))
                                serverPluginPath += "\\";
                            nline = nline.Replace(serverPluginPath, ClientServices.ClientDir + "\\" + ClientServices.ConfigName + "\\Plugin\\");
                            nline = nline.Replace(@"\", @"\\");
                            writer.WriteLine(nline);
                        }
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
