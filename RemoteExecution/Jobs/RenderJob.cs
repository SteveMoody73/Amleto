using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace RemoteExecution.Jobs
{
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
        public int TotSlices = 1;
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
                    string fname = string.Format(ClientServices.ClientDir + "\\Output\\{0}_{1:0000}{2}", Instance, i, ext);
                    if (File.Exists(fname))
                        File.Delete(fname);

                    if (SaveAlpha)
                    {
                        ext = AlphaImageFormat.Substring(AlphaImageFormat.IndexOf('(') + 1, (AlphaImageFormat.IndexOf(')') - AlphaImageFormat.IndexOf('(')) - 1);
                        fname = string.Format(ClientServices.ClientDir + "\\Output\\{0}_a{1:0000}{2}", Instance, i, ext);
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
                Directory.CreateDirectory(ClientServices.ClientDir+"\\Output");

                string[] lines = File.ReadAllLines(ClientServices.ClientDir+"\\Content\\" + _file);
                StreamWriter writer = new StreamWriter(ClientServices.ClientDir+"\\Content\\render_" + Instance + ".aml");
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
                writer.WriteLine("SaveRGBImagesPrefix "+ClientServices.ClientDir+"\\Output\\" + Instance + "_");
                writer.WriteLine("SaveAlphaImagesPrefix " + ClientServices.ClientDir + "\\Output\\" + Instance + "_a");
                writer.WriteLine("RGBImageSaver " + ImageFormat);
                writer.WriteLine("AlphaImageSaver " + AlphaImageFormat);

                double topLine = (double)SliceNumber * (double)Height / (double)TotSlices;
                double sliceHeight = (double)1 * (double)Height / (double)TotSlices;

                topLine -= (double)Height * Overlap / 200.0;
                if(Overlap > 0)
                    sliceHeight += (double)Height * Overlap / 100.0;
                else
                    sliceHeight +=1.0;

                if (topLine < 0.0)
                    topLine = 0;
                if ((int)topLine + (int)sliceHeight >= Height)
                    sliceHeight = (int)(Height - topLine);

                bool inMaster = false;
                int lnNb = 0;
                foreach (string line in lines)
                {
                    // Skip the header
                    lnNb++;
                    if (lnNb < 4)
                        continue;
                    /*if (line == "LWSC" || line == "1" || line == "2" || line == "3" || line == "4")
                        continue;*/
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
                        string[] p=line.Split(' ');
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

					if (line.StartsWith("GlobalMaskPosition "))
					{
						if (TotSlices == 1)
							writer.WriteLine(line);
						else
							writer.WriteLine("GlobalMaskPosition 0 " + (int)(topLine) + " " + Width + " " + (int)(Math.Ceiling(sliceHeight)));
                        continue;
					}
					if (line.StartsWith("MaskPosition "))
					{
						if (TotSlices == 1)
						{
							writer.WriteLine("CameraMask 0");
							writer.WriteLine("MaskPosition 0 0 " + Width + " " + Height);
						}
						else
						{
							writer.WriteLine("CameraMask 1");
							writer.WriteLine("MaskPosition 0 " + (int)(topLine) + " " + Width + " " + (int)(Math.Ceiling(sliceHeight)));
							writer.WriteLine("MaskColor 0 0 0");
							writer.WriteLine("UseGlobalMask 1");
						}
                        continue;
					}
					if (line.StartsWith("CameraMask ") && (TotSlices != 1))
						continue;
					if (line.StartsWith("UseGlobalMask ") && (TotSlices != 1))
						continue;


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
							//writer.WriteLine("AASamples " + cameraAntialias);
							continue;
						else if (line.StartsWith("Sampler "))
							continue;
						//writer.WriteLine("Sampler " + samplingPattern);
						else
							writer.WriteLine(line);
					}
                    else
                        writer.WriteLine(line);
                }
                writer.Close();
                writer.Dispose();

                Process process = new Process();
                process.StartInfo.FileName = ClientServices.ClientDir + "\\" + ClientServices.ConfigName + "\\Program\\lwsn.exe";
                process.StartInfo.Arguments = "-3 \"-c" + ClientServices.ClientDir + "\\" + ClientServices.ConfigName + "\\Config\" \"-d" + ClientServices.ClientDir + "\\CONTENT\" \"" + ClientServices.ClientDir + "\\CONTENT\\render_" + Instance + ".aml\" " + StartFrame + " " + EndFrame + " " + Step;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.WorkingDirectory = ClientServices.ClientDir + "\\" + ClientServices.ConfigName + "\\Program\\";
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.OutputDataReceived += ProcessOutputDataReceived;
                ClientServices.SetRenderProcess(process);
                process.Start();
                process.PriorityClass = ClientServices.RenderPriority;
                process.BeginOutputReadLine();
                process.WaitForExit();
                ClientServices.SetRenderProcess(null);
                process.Close();
                process.Dispose();
            }
            catch (Exception ex)
            {
				Debug.WriteLine("Error executing render job: " + ex);
			}

            Server.SetCurrentJob("Uploading back images");

            for (int i = StartFrame; i <= EndFrame; i += Step)
            {
                string fname = string.Format(ClientServices.ClientDir+"\\Output\\{0}_{1:0000}{2}", Instance, i, ext);
                string afname = string.Format(ClientServices.ClientDir + "\\Output\\{0}_a{1:0000}{2}", Instance, i, ext);
                if (File.Exists(fname))
                {
                    try
                    {
                        messageBack(0,"Frame " + i + " rendered successfully");
                        Server.SendImage(i, SliceNumber, File.ReadAllBytes(fname));
                        if (!SaveAlpha)
                            Server.FrameFinished(i, SliceNumber);
                    }
                    catch
                    {
                        messageBack(1, "Error while uploading frame "+i+" back.");
                        Server.FrameLost(i, SliceNumber);
                    }
                }
                else
                {
                    messageBack(1,"Frame " + i + " didn't render.");
                    Server.FrameLost(i, SliceNumber);
                }
                
                if (SaveAlpha && File.Exists(afname))
                {
                    try
                    {
                        messageBack(0, "Frame " + i + " alpha rendered successfully");
                        Server.SendImageAlpha(i, SliceNumber, File.ReadAllBytes(afname));
                        Server.FrameFinished(i, SliceNumber);
                    }
                    catch
                    {
                        messageBack(1, "Error while uploading frame " + i + " Alpha back.");
                        Server.FrameLost(i, SliceNumber);
                    }
                }
            }
            Server.SetCurrentJob("");
            GC.Collect();
        }

        void ProcessOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Data))
                _messageBack(2,e.Data);
        }
    }
}