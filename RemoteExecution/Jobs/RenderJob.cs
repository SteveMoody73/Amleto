using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace RemoteExecution.Jobs
{
    [Serializable]
    public class OutputPlugins
    {
        public string BaseFilename;
        public string BasePath;
        public string FileExtension;
        public string PluginType;
    }

    [Serializable]
    public class RenderJob : Job
    {
        string _file;
		MessageBack _messageBack;
		
		public int StartFrame;
        public int EndFrame;
        public int Step;
		public bool OverrideSettings;
        public bool OverwriteFrames;
        public string ImageFormat;
        public bool SaveAlpha;
        public string AlphaImageFormat;
        public int Instance;
        public bool Sent;
        public int Retrials;
        public int Antialias;
    	public int ReconFilter;
        public int Width = 640;
        public int Height = 480;
        public double Aspect = 1.0;
        public int RenderEffect;
        public int RenderMode ;
        public int RecursionLimit = 16;
        public int RenderLine = 1;
        public int AdaptiveSampling;
        public double AdaptiveThreshold = 0.1;
        public int FilterType;
        public int EnhanceAA;
        public int AntialiasLevel = -1;
        public int ClientId = -1;
        public List<string> StrippedMaster = new List<string>();
        public int SliceNumber = 1;
        public int SlicesDown = 1;
        public int SlicesAcross = 1;
        public double Overlap = 5;
        public int Camera;
        public int SamplingPattern;
        public int CameraAntialias = 1;
        public int Radiosity;
        public int RadiosityType;
        public int InterpolatedGI;
        public int BackdropTranspGI;
        public int CachedGI;
        public int VolumetricGI;
        public int UseAmbientGI;
        public int DirectionalGI;
        public double IntensityGI;
        public double ToleranceGI;
        public int RayGI;
        public double MinEvalGI;
        public double MinPixelGI;
        public int IndirectGI;
        public List<OutputPlugins> Plugins;
        public string FilenameFormat;

        [NonSerialized]
        public Stopwatch TimeSpent;

		public RenderJob(string file, int startFrame, int endFrame, int step, int instance, string imageFormat, bool saveAlpha, string alphaFormat)
        {
            _file = file;
            StartFrame = startFrame;
            EndFrame = endFrame;
            Step = step;
            Instance = instance;
            ImageFormat = imageFormat;
            SaveAlpha = saveAlpha;
            AlphaImageFormat = alphaFormat;
            Plugins = new List<OutputPlugins>();
        }

        public override void ExecuteJob(MessageBack messageBack, Queue<Job> jobs)
        {
            _messageBack = messageBack;
            Server.SetCurrentJob("Render " + _file + " frame(s) " + StartFrame + " to " + EndFrame);

            string ext = ImageFormat.Substring(ImageFormat.IndexOf('(') + 1, (ImageFormat.IndexOf(')') - ImageFormat.IndexOf('(')) - 1);

            try
            {
                for (int i = StartFrame; i <= EndFrame; i += Step)
                {
                    string outputPath = Path.Combine(ClientServices.GetClientDir(), "Output");
                    string fname = string.Format( outputPath + Path.DirectorySeparatorChar + "{0}_{1:0000}{2}", Instance, i, ext);
                    
                    if (File.Exists(fname))
                        File.Delete(fname);

                    if (SaveAlpha)
                    {
                        ext = AlphaImageFormat.Substring(AlphaImageFormat.IndexOf('(') + 1,
                                                         (AlphaImageFormat.IndexOf(')') - AlphaImageFormat.IndexOf('(')) -
                                                         1);
                        fname = string.Format(outputPath + Path.DirectorySeparatorChar + "{0}_a{1:0000}{2}", Instance, i, ext);
                        if (File.Exists(fname))
                            File.Delete(fname);
                    }
                }
            }
            catch (Exception ex)
            {
				Debug.WriteLine("Error executing render job: " + ex);
            }

            try
            {
                messageBack(0,"Rendering file " + _file + " frame(s) " + StartFrame + " to " + EndFrame);
                string contentPath = Path.Combine(ClientServices.GetClientDir(), "Content");
                string outputPath = Path.Combine(ClientServices.GetClientDir(), "Output");
                
                Directory.CreateDirectory(outputPath);
                string[] lines = File.ReadAllLines(Path.Combine(contentPath, _file));
                StreamWriter writer = new StreamWriter(Path.Combine(contentPath, "render_" + Instance + ".aml"));
                writer.WriteLine("LWSC");
                writer.WriteLine(lines[1]);
                writer.WriteLine("");
                writer.WriteLine("RenderRangeType 0");
                writer.WriteLine("FirstFrame " + StartFrame);
                writer.WriteLine("LastFrame " + EndFrame);
                writer.WriteLine("FrameStep " + Step);
                
				if (SaveAlpha)
                    writer.WriteLine("SaveAlpha 1");
                else
                    writer.WriteLine("SaveAlpha 0");
                               
                writer.WriteLine("SaveRGB 1");
                writer.WriteLine("OutputFilenameFormat 3");
                writer.WriteLine("SaveRGBImagesPrefix " + Path.Combine(outputPath, Instance + "_"));
                writer.WriteLine("SaveAlphaImagesPrefix " + Path.Combine(outputPath, Instance + "_a"));
                writer.WriteLine("RGBImageSaver " + ImageFormat);
                writer.WriteLine("AlphaImageSaver " + AlphaImageFormat);

                bool inMaster = false;
                int lineNumber = 0;
                bool inPlugin = false;
                string prevLine = "";

                foreach (string line in lines)
                {
                    // Skip the header
                    lineNumber++;
                    if (lineNumber < 4)
                        continue;

                    if (inPlugin)
                    {
                        if (prevLine.Contains("{ Options"))
                        {

                            OutputPlugins plugin = Plugins[Plugins.Count - 1];
                            if (plugin.PluginType == "BufferExport")
                            {
                                // current line should be the path
                                string baseName = line.Replace('\"', ' ').Trim();
                                baseName = baseName.Replace(@"\\", @"\");
                                string filename = Path.GetFileNameWithoutExtension(baseName);
                                string output = Path.GetDirectoryName(baseName);
                                string extension = Path.GetExtension(baseName);
                                if (output == null)
                                    output = "";
                                plugin.BaseFilename = filename;
                                plugin.BasePath = output;
                                plugin.FileExtension = extension;

                                // write the new path
                                string newfile = Path.Combine(outputPath, filename + extension);
                                writer.WriteLine("    \"" + newfile.Replace(@"\", @"\\") + "\"");
                            }
                        }
                        else
                        {
                            if (line.Contains("EndPlugin"))
                                inPlugin = false;
                            writer.WriteLine(line);
                        }
                    }
                    else
                    {
                        if (line.StartsWith("Plugin "))
                        {
                            if (line.Contains("LW_CompositeBuffer"))
                            {
                                OutputPlugins plugin = new OutputPlugins();
                                plugin.PluginType = "BufferExport";
                                Plugins.Add(plugin);
                                inPlugin = true;
                                writer.WriteLine();
                            }
                        }
                        // Skip the render range / format definition
                        if (line.StartsWith("FirstFrame "))
                            continue;
                        if (line.StartsWith("LastFrame "))
                            continue;
                        if (line.StartsWith("FrameStep "))
                            continue;
                        if (line.StartsWith("RenderRangeType "))
                            continue;
                        if (line.StartsWith("RenderRangeArbitrary "))
                            continue;
                        if (line.StartsWith("Plugin MasterHandler") && StrippedMaster.Count > 0)
                        {
                            string[] p = line.Split(' ');
                            if (StrippedMaster.Exists(delegate(string x) { return x == p[3]; }))
                            {
                                inMaster = true;
                                continue;
                            }
                        }
                        if (inMaster && line == "EndPlugin") // Strip until EndPlugin
                        {
                            inMaster = false;
                            continue;
                        }
                        if (inMaster) // We are in a master plugin
                            continue;
                        if (line.StartsWith("OutputFilenameFormat "))
                            continue;
                        if (line.StartsWith("SaveRGB "))
                            continue;
                        if (line.StartsWith("SaveRGBImagesPrefix "))
                            continue;
                        if (line.StartsWith("RGBImageSaver "))
                            continue;
                        if (line.StartsWith("SaveAlphaImagesPrefix "))
                            continue;
                        if (line.StartsWith("AlphaImageSaver "))
                            continue;
                        if (line.StartsWith("SaveAlpha "))
                            continue;

                        if (line.StartsWith("LimitedRegion "))
                        {
                            // If not split rendering, leave the current settings
                            if (SlicesDown == 1 && SlicesAcross == 1)
                                writer.WriteLine(line);

                            continue;
                        }
                        if (line.StartsWith("RegionLimits "))
                        {
                            if (SlicesDown == 1 && SlicesAcross == 1)
                                writer.WriteLine(line);

                            continue;
                        }

                        if (line.StartsWith("Antialiasing "))
                        {
                            if (SlicesDown > 1 || SlicesAcross > 1)
                            {
                                writer.WriteLine("LimitedRegion 2");

                                int posX;
                                int posY;

                                if (SlicesDown > 1 && SlicesAcross > 1)
                                {
                                    posX = SliceNumber%SlicesAcross;
                                    posY = SliceNumber/SlicesAcross;
                                }
                                else if (SlicesDown == 1)
                                {
                                    posX = SliceNumber;
                                    posY = 0;
                                }
                                else
                                {
                                    posX = 0;
                                    posY = SliceNumber;
                                }

                                double sizeV = 1.0/SlicesDown;
                                double sizeH = 1.0/SlicesAcross;
                                double overlapV = sizeV*(Overlap/100.0);
                                double overlapH = sizeH*(Overlap/100.0);

                                double sliceLeft = sizeH*posX - overlapH;
                                double sliceRight = sizeH*posX + sizeH + overlapH;
                                double sliceTop = sizeV*posY - overlapV;
                                double sliceBottom = sizeV*posY + sizeV + overlapV;

                                sliceTop = Math.Max(0.0, sliceTop);
                                sliceBottom = Math.Min(1.0, sliceBottom);
                                sliceLeft = Math.Max(0.0, sliceLeft);
                                sliceRight = Math.Min(1.0, sliceRight);

                                writer.WriteLine("RegionLimits " +
                                                 sliceLeft.ToString("F3") + " " +
                                                 sliceRight.ToString("F3") + " " +
                                                 sliceTop.ToString("F3") + " " +
                                                 sliceBottom.ToString("F3"));
                                Debug.WriteLine("RegionLimits " + SliceNumber + " " + posX + "," + posY + " " +
                                                sliceLeft.ToString("F3") + " " +
                                                sliceRight.ToString("F3") + " " +
                                                sliceTop.ToString("F3") + " " +
                                                sliceBottom.ToString("F3"));
                            }
                        }

                        if (OverrideSettings)
                        {
                            if (line.StartsWith("RenderMode "))
                                writer.WriteLine("RenderMode " + RenderMode);
                            else if (line.StartsWith("RayTraceEffects "))
                                writer.WriteLine("RayTraceEffects " + RenderEffect);
                            else if (line.StartsWith("EnableRadiosity"))
                            {
                                writer.WriteLine("EnableRadiosity " + Radiosity);
                                writer.WriteLine("RadiosityType " + RadiosityType);
                                writer.WriteLine("RadiosityInterpolated " + InterpolatedGI);
                                writer.WriteLine("RadiosityTransparency " + BackdropTranspGI);
                                writer.WriteLine("CacheRadiosity " + CachedGI);
                                writer.WriteLine("RadiosityIntensity " + IntensityGI);
                                writer.WriteLine("RadiosityTolerance " + ToleranceGI);
                                writer.WriteLine("RadiosityRays " + RayGI);
                                writer.WriteLine("RadiosityMinSpacing " + MinEvalGI);
                                writer.WriteLine("RadiosityMinPixelSpacing " + MinPixelGI);
                                writer.WriteLine("IndirectBounces " + IndirectGI);
                                writer.WriteLine("RadiosityUseAmbient " + UseAmbientGI);
                                writer.WriteLine("VolumetricRadiosity " + VolumetricGI);
                                writer.WriteLine("RadiosityDirectionalRays " + DirectionalGI);
                            }
                            else if (line.StartsWith("RadiosityType "))
                                continue;
                            else if (line.StartsWith("RadiosityInterpolated "))
                                continue;
                            else if (line.StartsWith("RadiosityTransparency "))
                                continue;
                            else if (line.StartsWith("CacheRadiosity "))
                                continue;
                            else if (line.StartsWith("RadiosityIntensity "))
                                continue;
                            else if (line.StartsWith("RadiosityTolerance "))
                                continue;
                            else if (line.StartsWith("RadiosityRays "))
                                continue;
                            else if (line.StartsWith("RadiosityMinSpacing "))
                                continue;
                            else if (line.StartsWith("RadiosityMinPixelSpacing "))
                                continue;
                            else if (line.StartsWith("IndirectBounces "))
                                continue;
                            else if (line.StartsWith("RadiosityUseAmbient "))
                                continue;
                            else if (line.StartsWith("VolumetricRadiosity "))
                                continue;
                            else if (line.StartsWith("RadiosityDirectionalRays "))
                                continue;
                            else if (line.StartsWith("GlobalFrameSize "))
                                writer.WriteLine("GlobalFrameSize " + Width + " " + Height);
                            else if (line.StartsWith("FrameSize "))
                                writer.WriteLine("FrameSize " + Width + " " + Height);
                            else if (line.StartsWith("GlobalPixelAspect "))
                                writer.WriteLine("GlobalPixelAspect " + Aspect);
                            else if (line.StartsWith("PixelAspect "))
                                writer.WriteLine("PixelAspect " + Aspect);
                            else if (line.StartsWith("RayRecursionLimit "))
                                writer.WriteLine("RayRecursionLimit " + RecursionLimit);
                            else if (line.StartsWith("RenderLines "))
                                writer.WriteLine("RenderLines " + RenderLine);
                            else if (line.StartsWith("Antialiasing ")) // Antialiasing block
                            {
                                writer.WriteLine("Antialiasing " + Antialias);
                                writer.WriteLine("AntiAliasingLevel " + AntialiasLevel);
                                writer.WriteLine("EnhancedAA " + EnhanceAA);
                                writer.WriteLine("AdaptiveSampling " + AdaptiveSampling);
                                writer.WriteLine("AdaptiveThreshold " + AdaptiveThreshold);
                                writer.WriteLine("FilterType " + FilterType);
                                writer.WriteLine("ReconstructionFilter " + EnhanceAA);
                            }
                            else if (line.StartsWith("AdaptiveSampling "))
                                continue;
                            else if (line.StartsWith("AdaptiveThreshold "))
                                continue;
                            else if (line.StartsWith("FilterType "))
                                continue;
                            else if (line.StartsWith("EnhancedAA "))
                                continue;
                            else if (line.StartsWith("AntiAliasingLevel "))
                                continue;
                            else if (line.StartsWith("ReconstructionFilter "))
                                continue;
                            else if (line.StartsWith("CurrentCamera "))
                                writer.WriteLine("CurrentCamera " + Camera);
                            else if (line.StartsWith("CameraName "))
                            {
                                writer.WriteLine(line);
                                writer.WriteLine("AASamples " + CameraAntialias);
                                writer.WriteLine("Sampler " + SamplingPattern);
                            }
                            else if (line.StartsWith("AASamples "))
                                continue;
                            else if (line.StartsWith("Sampler "))
                                continue;
                            else
                                writer.WriteLine(line);
                        }
                        else
                            writer.WriteLine(line);
                        
                    }
                    prevLine = line;
                }

                writer.Close();
                writer.Dispose();

                TimeSpent = new Stopwatch();
                TimeSpent.Start();
                
                Process process = new Process();
                string configPath = Path.Combine(ClientServices.GetClientDir(), ClientServices.ConfigName);
                string programPath = Path.Combine(configPath, "Program");

                process.StartInfo.FileName = Path.Combine(programPath, "lwsn.exe");
                process.StartInfo.Arguments = "-3 \"-c" + configPath + "\" \"-d" + contentPath + "\" \"" +
                                              Path.Combine(contentPath, "render_" + Instance + ".aml") + "\" " +
                                              StartFrame + " " + EndFrame + " " + Step;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.WorkingDirectory = programPath;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.OutputDataReceived += ProcessOutputDataReceived;
                ClientServices.SetRenderProcess(process);
                process.Start();
                process.PriorityClass = ClientServices.Settings.RenderPriority;
                process.BeginOutputReadLine();
                process.WaitForExit();
                ClientServices.SetRenderProcess(null);
                process.Close();
                process.Dispose();

                TimeSpent.Stop();
            }
            catch (Exception ex)
            {
				Debug.WriteLine("Error executing render job: " + ex);
			}

            Server.SetCurrentJob("Uploading back images");

            for (int i = StartFrame; i <= EndFrame; i += Step)
            {
                bool uploadError = false;

                string outputPath = Path.Combine(ClientServices.GetClientDir(), "Output");
                string fname = string.Format(outputPath + Path.DirectorySeparatorChar + "{0}_{1:0000}{2}", Instance, i, ext);
                string afname = string.Format(outputPath + Path.DirectorySeparatorChar + "{0}_a{1:0000}{2}", Instance, i, ext);

                // Process any files from image saver plugins
                foreach (OutputPlugins plugin in Plugins)
                {
                    if (plugin.PluginType == "BufferExport")
                    {
                        DirectoryInfo bufferPath = new DirectoryInfo(outputPath);
                        foreach (FileInfo file in bufferPath.GetFiles(plugin.BaseFilename + "*"))
                        {
                            string nameFormat = FilenameFormat.Substring(FilenameFormat.IndexOf("\\") + 1);
                            string fileNumber = string.Format(nameFormat, "", "", i, plugin.FileExtension);
                            if (file.FullName.Contains(fileNumber))
                            {
                                try
                                {
                                    Server.SendFile(plugin.BasePath, Path.GetFileName(file.FullName), File.ReadAllBytes(file.FullName));
                                    messageBack(0, "Frame " + i + " buffer " + Path.GetFileName(file.FullName) + " rendered successfully");
                                }
                                catch (Exception e)
                                {
                                    messageBack(1, "Error while uploading frame " + i);
                                    Debug.WriteLine("Error sending file :" + e);
                                    uploadError = true;
                                }
                            }
                        }
                    }
                }

                if (File.Exists(fname))
                {
                    try
                    {
                        messageBack(0,"Frame " + i + " rendered successfully");
                        Server.SendImage(i, SliceNumber, File.ReadAllBytes(fname));
                    }
                    catch (Exception e)
                    {
                        messageBack(1, "Error while uploading frame " + i);
                        Debug.WriteLine("Error sending file :" + e);
                        uploadError = true;
                    }
                }
                else
                {
                    messageBack(1, "Frame " + i + " failed to render");
                    Server.FrameLost(i, SliceNumber);
                }
                
                if (SaveAlpha && File.Exists(afname))
                {
                    try
                    {
                        messageBack(0, "Frame " + i + " alpha rendered successfully");
                        Server.SendImageAlpha(i, SliceNumber, File.ReadAllBytes(afname));
                    }
                    catch (Exception e)
                    {
                        messageBack(1, "Error while uploading frame " + i + " Alpha");
                        Debug.WriteLine("Error sending file :" + e);
                        uploadError = true;
                    }
                }
                if (uploadError)
                    Server.FrameLost(i, SliceNumber);
                else
                    Server.FrameFinished(i, SliceNumber);
            }
            Server.SetCurrentJob("");
            Server.SetLastRenderTime(TimeSpent.Elapsed);
            GC.Collect();
        }

        void ProcessOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Data))
                _messageBack(2,e.Data);
        }
    }
}