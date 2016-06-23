using System;
using System.Collections.Generic;
using NLog;
using RemoteExecution.Jobs;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;
using System.Xml.Serialization;
using FreeImageAPI;

namespace RemoteExecution
{
    [Serializable]
    [XmlRoot("RenderProject")]
    public class RenderProject
    {
        [XmlIgnore]
        public static int NextProjectId;

        [XmlElement("ContentDir")]
        public string ContentDir = "";
        [XmlElement("SceneFile")]
        public string SceneFile = "";
        [XmlElement("OutputDir")]
        public string OutputDir = "";
        [XmlElement("Prefix")]
        public string Prefix = "img_";
        [XmlElement("AlphaPrefix")]
        public string AlphaPrefix = "img_";
        [XmlElement("StartFrame")]
        public int StartFrame = 1;
        [XmlElement("EndFrame")]
        public int EndFrame = 60;
        [XmlElement("FrameStep")]
        public int FrameSteps = 1;
        [XmlElement("ProjectId")]
        public int ProjectId = -1;
		[XmlElement("OverrideSettings")]
		public bool OverrideSettings;
        [XmlElement("OverwriteFrames")]
        public bool OverwriteFrames;
        [XmlElement("FileNameFormat")]
        public int FileNameFormat;

        [XmlIgnore] public static string[] FileNameFormats = {
                                                                 "{0}\\{1}{2:000}.", "{0}\\{1}{2:000}{3}",
                                                                 "{0}\\{1}{2:0000}.", "{0}\\{1}{2:0000}{3}",
                                                                 "{0}\\{1}_{2:000}.", "{0}\\{1}_{2:000}{3}",
                                                                 "{0}\\{1}_{2:0000}.", "{0}\\{1}_{2:0000}{3}",
                                                                 "{0}\\{1}{2:00000}.", "{0}\\{1}{2:00000}{3}",
                                                                 "{0}\\{1}{2:000000}.", "{0}\\{1}{2:000000}{3}",
                                                                 "{0}\\{1}_{2:00000}.", "{0}\\{1}_{2:00000}{3}",
                                                                 "{0}\\{1}_{2:000000}.", "{0}\\{1}_{2:000000}{3}"
                                                             };
        [XmlElement("ImageFormat")]
        public int ImageFormat;
        [XmlElement("AlphaImageFormat")]
        public int AlphaImageFormat;
        [XmlElement("SaveAlpha")] 
        public bool SaveAlpha;
        [XmlIgnore]
        private List<RenderJob> _jobs = new List<RenderJob>();
        [XmlIgnore]
        public int StartJobs;
        [XmlElement("Antialias")]
        public int Antialias;
        [XmlElement("ReconFilter")]
        public int ReconFilter;
        [XmlElement("Width")]
        public int Width = 640;
        [XmlElement("Heigth")]
        public int Height = 480;
        [XmlElement("AspectRatio")]
        public double Aspect = 1.0;
        [XmlElement("RenderEffect")]
        public int RenderEffect;
        [XmlElement("RenderMode")]
        public int RenderMode;
        [XmlElement("RecursionLimit")]
        public int RecursionLimit = 16;
        [XmlElement("RenderLine")]
        public int RenderLine = 1;
        [XmlElement("AdaptiveSampling")]
        public int AdaptiveSampling;
        [XmlElement("AdaptiveThreashold")]
        public string AdaptiveThreshold = "0.1";
        [XmlElement("FilterType")]
        public int FilterType;
        [XmlElement("EnhanceAA")]
        public int EnhanceAA;
        [XmlElement("AntialiasLevel")]
        public int AntialiasLevel = -1;
        [XmlIgnore]
        public string Owner = "";
        [XmlIgnore]
        public bool Paused;
        [XmlElement("Block")]
        public int Block = 1;
        [XmlElement("ConfigNumber")]
        public int Config;
        [XmlArray("MasterStripped")]
        [XmlArrayItem("Stripped")]
        public List<string> StrippedMaster = new List<string>();
        [XmlElement("SlicesAcross")]
        public int SlicesAcross = 1;
        [XmlElement("SlicesDown")]
        public int SlicesDown = 1;
        [XmlElement("Overlap")]
        public double Overlap = 5;
        [XmlElement("DeleteSplitFrames")]
        public bool DeleteSplitFrames;
        [XmlElement("Camera")]
        public int Camera;
        [XmlElement("SamplingPattern")]
        public int SamplingPattern;
        [XmlElement("CameraAntialias")]
        public int CameraAntialias;
        [XmlElement("MinSamples")]
        public int MinSamples;
        [XmlElement ("MaxSamples")]
        public int MaxSamples;
        [XmlElement("Radiosity")]
        public int Radiosity;
        [XmlElement("RadiosityType")]
        public int RadiosityType;
        [XmlElement("InterpolatedGI")]
        public int InterpolatedGI;
        [XmlElement("BackDropTransparency")]
        public int BackdropTranspGI;
        [XmlElement("CachedGI")]
        public int CachedGI;
        [XmlElement("VolumetricGI")]
        public int VolumetricGI;
        [XmlElement("UseAmbientGI")]
        public int UseAmbientGI;
        [XmlElement("DirectionalGI")]
        public int DirectionalGI;
        [XmlElement("IntensityGI")]
        public double IntensityGI;
        [XmlElement("ToleranceGI")]
        public double ToleranceGI;
        [XmlElement("RayGI")]
        public int RayGI;
        [XmlElement("MinEvalGI")]
        public double MinEvalGI;
        [XmlElement("MinPixelEvalGI")]
        public double MinPixelGI;
        [XmlElement("IndirectGI")]
        public int IndirectGI;
        [XmlElement("EmailNotify")]
        public bool EmailNotify;
        [XmlElement("EmailTo")]
        public string EmailTo = "";
        [XmlElement("EmailSubjectOk")]
        public string EmailSubjectOk = "";
        [XmlElement("EmailSubjectNok")]
        public string EmailSubjectNotOk = "";
        [XmlElement("EmailLog")]
        public bool EmailContainLog = true;
        [XmlElement("RenderTime")] public TimeSpan RenderTime;
        [XmlElement("RenderedFrameCount")] public int RenderedFrameCount;
        [XmlElement("FinalStatus")] public string FinalStatus;

        [XmlIgnore] public bool IsFinished;
        [XmlIgnore] public List<FinishedFrame> RenderedFrames = new List<FinishedFrame>();
        [XmlIgnore] public string EmailFrom = "amleto@yourdomain.com";
        [XmlIgnore] public string SmtpServer = "mail.yourdomain.com";
        [XmlIgnore] public string SmtpUsername = "";
        [XmlIgnore] public string SmtpPassword = "";
        [XmlIgnore] public int SmtpPort = 25;
        [XmlIgnore] public string SmtpLogin = "";
        [XmlIgnore] public string Log = "";
		[XmlIgnore] public DateTime StartTime;
        [XmlIgnore] public DateTime UpdateTime;
        [XmlIgnore] public bool StartTimeSet;
        [XmlIgnore] public bool UpdateTimeSet;
        [XmlIgnore] private static Logger logger = LogManager.GetCurrentClassLogger();

        private class MergePixel
        {
            public MergePixel(double r, double g, double b, double a) { red = r; green = g; blue = b; alpha = a; }
            public double red;
            public double green;
            public double blue;
            public double alpha;
        }


        private void CopyJobParams(RenderJob job)
        {
            job.Antialias = Antialias;
            job.ReconFilter = ReconFilter;
            job.Width = Width;
            job.Height = Height;
            job.Aspect = Aspect;
            job.RenderEffect = RenderEffect;
            job.RenderMode = RenderMode;
            job.RecursionLimit = RecursionLimit;
            job.RenderLine = RenderLine;
            job.AdaptiveSampling = AdaptiveSampling;
            job.AdaptiveThreshold = AdaptiveThreshold;
            job.FilterType = FilterType;
            job.EnhanceAA = EnhanceAA;
            job.AntialiasLevel = AntialiasLevel;
            job.SliceNumber = 0;
            job.SlicesDown = SlicesDown;
            job.SlicesAcross = SlicesAcross;
            job.Overlap = Overlap;
            job.Camera = Camera;
            job.SamplingPattern = SamplingPattern;
            job.CameraAntialias = CameraAntialias;
            job.MinSamples = MinSamples;
            job.MaxSamples = MaxSamples;
            job.Radiosity = Radiosity;
            job.RadiosityType = RadiosityType;
            job.InterpolatedGI = InterpolatedGI;
            job.InterpolatedGI = InterpolatedGI;
            job.CachedGI = CachedGI;
            job.VolumetricGI = VolumetricGI;
            job.UseAmbientGI = UseAmbientGI;
            job.DirectionalGI = DirectionalGI;
            job.IntensityGI = IntensityGI;
            job.ToleranceGI = ToleranceGI;
            job.RayGI = RayGI;
            job.MinEvalGI = MinEvalGI;
            job.MinPixelGI = MinPixelGI;
            job.IndirectGI = IndirectGI;
            job.SaveAlpha = SaveAlpha;
			job.OverrideSettings = OverrideSettings;
            job.OverwriteFrames = OverwriteFrames;
            job.FilenameFormat = FileNameFormats[FileNameFormat];

            foreach (string s in StrippedMaster)
                job.StrippedMaster.Add(s);
        }

        public void GenerateRenderJobs()
        {
            if (Log == "")
            {
                Log += "*******************************************************\n";
                Log += "Project: " + SceneFile + "\n";
                Log += "Id: " + ProjectId + "\n";
                Log += "Sent by: " + Owner + "\n";
                Log += "Sent on: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "\n";
                Log += "*******************************************************\n";
            }
            else
            {
                Log += "*******************************************************\n";
                Log += "Project modified on: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "\n";
                Log += "*******************************************************\n";
            }

            if (!ContentDir.EndsWith("\\"))
                ContentDir += "\\";

            _jobs.Clear();
			
			if (SlicesAcross > 1 || SlicesDown > 1)
			{
			    for (int i = 0; i < SlicesAcross * SlicesDown; i++)
			    {
			        RenderJob job = new RenderJob(SceneFile.Substring(ContentDir.Length), StartFrame, StartFrame, 1, 0,
			                                      ServerServices.Configs[Config].ImageFormats[ImageFormat], SaveAlpha,
			                                      ServerServices.Configs[Config].ImageFormats[AlphaImageFormat]);
			        CopyJobParams(job);
			        job.SliceNumber = i;

			        _jobs.Add(job);
			    }
			}
			else
            {
			    for (int i = StartFrame; i <= EndFrame; i += FrameSteps)
			    {
			        // If the overwrite flag isn't set
			        // Check if the frame (and alpha) to render already exists in the output folder
			        string imgfmt = ServerServices.Configs[Config].ImageFormats[ImageFormat];
			        string ext = imgfmt.Substring(imgfmt.IndexOf('(') + 1, (imgfmt.IndexOf(')') - imgfmt.IndexOf('(')) - 1);
			        string fname = string.Format(FileNameFormats[FileNameFormat], OutputDir, Prefix, i, ext);
			        bool filesExist;
			        if (SaveAlpha)
			        {
			            string aimgfmt = ServerServices.Configs[Config].ImageFormats[AlphaImageFormat];
			            string aext = aimgfmt.Substring(aimgfmt.IndexOf('(') + 1,
			                                            (aimgfmt.IndexOf(')') - aimgfmt.IndexOf('(')) - 1);
			            string aname = string.Format(FileNameFormats[FileNameFormat], OutputDir, AlphaPrefix, i, aext);
			            filesExist = File.Exists(fname) && File.Exists(aname);
			        }
			        else
			            filesExist = File.Exists(fname);

			        // If overwrite flag is set or if one or both the needed files are not in the output folder
			        if (OverwriteFrames || !filesExist)
			        {
			            RenderJob job = new RenderJob(SceneFile.Substring(ContentDir.Length), i, i, 1, 0,
			                                          ServerServices.Configs[Config].ImageFormats[ImageFormat],
			                                          SaveAlpha, ServerServices.Configs[Config].ImageFormats[AlphaImageFormat]);
			            CopyJobParams(job);
			            job.SliceNumber = 0;
			            _jobs.Add(job);
			        }
			    }
			}
            StartJobs = _jobs.Count;
        }

        public List<Job> GetContentJobs()
        {
            return GetContentJobs(ContentDir);
        }

        protected List<Job> GetContentJobs(string path)
        {
            List<Job> res = new List<Job>();

            if (!ContentDir.EndsWith("\\"))
                ContentDir += "\\";

            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                if(d.Name == "." || d.Name == "..")
                    continue;
                res.AddRange(GetContentJobs(d.FullName));
            }
            foreach (FileInfo f in dir.GetFiles())
            {
                res.Add(new DownloadJob(f.FullName, f.FullName.Substring(ContentDir.Length), f.Length, f.LastWriteTimeUtc));
            }
            return res;
        }

        public Job GetRenderJob(int clientId,int instance)
        {
            for (int i = 0; i < _jobs.Count; i++)
            {
                if (_jobs[i].Sent == false)
                {
                    _jobs[i].ClientId = clientId;
                    _jobs[i].Sent = true;
                    _jobs[i].Instance = instance;
                    _jobs[i].StartFrame = _jobs[i].EndFrame;
                    if (_jobs[i].TimeSpent == null)
                        _jobs[i].TimeSpent = new Stopwatch();
                    _jobs[i].TimeSpent.Reset();
                    _jobs[i].TimeSpent.Start();
                    if (SlicesDown < 2 && SlicesAcross < 2 && FrameSteps < 2)
                    {
                        for (int j = 1; j < Block && (j + i) < _jobs.Count; j++)
                        {
                            // Check if the next frame is not in sequence. This can occur when re-rendering 
                            // a projects with existing frames in the output directory
                            if (_jobs[i + j].StartFrame != _jobs[i + j - 1].StartFrame + FrameSteps)
                                break;

                            if (_jobs[i + j].Sent)
                                break;
                            _jobs[i + j].Sent = true;
                            _jobs[i + j].ClientId = clientId;
                            _jobs[i + j].Instance = instance;
                            _jobs[i].EndFrame = _jobs[i + j].StartFrame;
                        }
                    }
                    return _jobs[i];
                }
            }
            return null;
        }

        public bool HasFreeJobs()
        {
        	foreach (RenderJob t in _jobs)
        		if (t.Sent == false)
        			return true;
        	return false;
        }

    	public string FinishFrame(int frame, int sliceNumber, string node)
    	{
    	    bool mergeImage = false;
            bool mergeAlpha = false;

            for (int i = 0; i < _jobs.Count; i++)
            {
                if (_jobs[i].StartFrame == frame && _jobs[i].SliceNumber == sliceNumber)
                {
                    if (_jobs[i].TimeSpent != null && _jobs[i].TimeSpent.IsRunning)
                    {
                        _jobs[i].TimeSpent.Stop();
                        if (SlicesAcross > 1 || SlicesDown > 1)
                            Log += DateTime.Now.ToLongTimeString() + " Rendered job frame " + frame + " slice " + sliceNumber + "  took " + _jobs[i].TimeSpent.Elapsed.ToString() + "\n";
                        else
                            Log += DateTime.Now.ToLongTimeString() + " Rendered job frame(s) starting at frame " + frame + " to " + _jobs[i].EndFrame + " by node " + node + " took " + _jobs[i].TimeSpent.Elapsed.ToString() + "\n";
                    }
                    _jobs.RemoveAt(i);
                    break;
                }
            }

            if ((SlicesAcross > 1 || SlicesDown > 1) && _jobs.Count == 0) // We finished all the slices!
            {
                string sImageFormat = "";
                string ext = "";
                string fname = "";

                sImageFormat = ServerServices.Configs[Config].ImageFormats[ImageFormat];
                ext = sImageFormat.Substring(sImageFormat.IndexOf('(') + 1, (sImageFormat.IndexOf(')') - sImageFormat.IndexOf('(')) - 1);
                mergeImage = MergeImages(FileNameFormats[FileNameFormat], OutputDir, Prefix, ext, StartFrame);

                if (SaveAlpha)
                {
                    sImageFormat = ServerServices.Configs[Config].ImageFormats[AlphaImageFormat];
                    ext = sImageFormat.Substring(sImageFormat.IndexOf('(') + 1, (sImageFormat.IndexOf(')') - sImageFormat.IndexOf('(')) - 1);
                    mergeAlpha = MergeImages(FileNameFormats[FileNameFormat], OutputDir, AlphaPrefix, ext, StartFrame);
                }

                // Delete intermediate files if needed
                if (DeleteSplitFrames && mergeImage)
                {
                    for (int i = 0; i < SlicesAcross * SlicesDown; i++)
                    {
                        File.Delete(string.Format(FileNameFormats[FileNameFormat], OutputDir, "slice_" + i + "_" + Prefix, StartFrame, ext));
                        File.Delete(string.Format(FileNameFormats[FileNameFormat], OutputDir, "slice_" + i + "_" + AlphaPrefix, StartFrame, ext));
                    }
                }
                Log += DateTime.Now.ToLongTimeString() + " Full frame " + StartFrame + " reconstructed.\n";
                RenderedFrames.Add(new FinishedFrame(SceneId, fname));
                return fname;
            }
            return null;
        }


        private bool MergeImages(string fileNameFormat, string outputDir, string prefix, string ext, int frameNum)
        {
            FreeImageBitmap combined = null;
            string fname = "";
            bool supported = false;
            bool mergeSuccessful = true;
            FREE_IMAGE_TYPE type = FREE_IMAGE_TYPE.FIT_UNKNOWN;
            FreeImageBitmap source = null;

            string mergedFile = string.Format(fileNameFormat, outputDir, prefix, frameNum, ext);
            string tempFile = string.Format(fileNameFormat, outputDir, prefix, frameNum, "_tmp" + ext);

            // Allocate a bitmap to store the final image
            fname = string.Format(fileNameFormat, outputDir, "slice_0_" + prefix, frameNum, ext);

            try
            {
                source = new FreeImageBitmap(fname);

                if (source != null)
                {
                    type = source.ImageType;
                    switch (type)
                    {
                        case FREE_IMAGE_TYPE.FIT_BITMAP:
                            if (source.ColorDepth == 32 || source.ColorDepth == 24)
                                supported = true;
                            break;
                        case FREE_IMAGE_TYPE.FIT_RGB16:
                        case FREE_IMAGE_TYPE.FIT_RGBA16:
                        case FREE_IMAGE_TYPE.FIT_RGBAF:
                        case FREE_IMAGE_TYPE.FIT_RGBF:
                            supported = true;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error opening slice file");
            }

            if (supported == false)
            {
                Console.WriteLine("Image format not supported");
                return false;
            }

            try
            {
                // Create a new image of the input file type and the correct size
                FIBITMAP newImage = FreeImage.AllocateT(type, Width, Height, source.ColorDepth, source.RedMask, source.BlueMask, source.GreenMask);

                FreeImage.SaveEx(newImage, tempFile);
                FreeImage.UnloadEx(ref newImage);
                source.Dispose();
                source = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                combined = new FreeImageBitmap(tempFile);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error creating output file");
                mergeSuccessful = false;
            }

            for (int i = 0; i < SlicesAcross * SlicesDown; i++)
            {
                // Load the image slice
                fname = string.Format(fileNameFormat, outputDir, "slice_" + i + "_" + prefix, frameNum, ext);
                FreeImageBitmap slice = new FreeImageBitmap(fname);

                int posX;
                int posY;

                if (SlicesDown > 1 && SlicesAcross > 1)
                {
                    posX = i % SlicesAcross;
                    posY = i / SlicesAcross;
                }
                else if (SlicesDown == 1)
                {
                    posX = i;
                    posY = 0;
                }
                else
                {
                    posX = 0;
                    posY = i;
                }

                // Calculate the image slice sizes and the row/column position
                double sizeV = (1.0 / SlicesDown) * Height;
                double sizeH = (1.0 / SlicesAcross) * Width;
                double overlapV = sizeV * (Overlap / 100.0);
                double overlapH = sizeH * (Overlap / 100.0);

                double realLeft = sizeH * posX;
                double left = realLeft - overlapH;

                double realTop = sizeV * posY;
                double top = realTop - overlapV;

                // Check the sizes are within limits and adjust
                left = Math.Max(0.0, left);
                top = Math.Max(0.0, top);

                try
                {
                    switch (type)
                    {
                        case FREE_IMAGE_TYPE.FIT_BITMAP:
                            if (slice.ColorDepth == 24)
                            {
                                for (int y = 0; y < slice.Height; y++)
                                {
                                    int srcY = (slice.Height - 1) - y;
                                    int destY = (combined.Height - 1) - (srcY + (int)top);
                                    int topY = y + (int)top;
                                    Scanline<RGBTRIPLE> srcLine = (Scanline<RGBTRIPLE>)slice.GetScanline(y);
                                    Scanline<RGBTRIPLE> destLine = (Scanline<RGBTRIPLE>)combined.GetScanline(destY);

                                    for (int x = 0; x < slice.Width; x++)
                                    {
                                        int destX = x + (int)left;

                                        // Make sure it's not out of bounds
                                        if (destY >= Height || destY >= Width)
                                            continue;
                                        // Is the pixel in an overlapping Area
                                        if (topY < realTop || destX < realLeft)
                                        {
                                            MergePixel srcPixel = new MergePixel(srcLine[x].rgbtRed, srcLine[x].rgbtGreen, srcLine[x].rgbtBlue, 0);
                                            MergePixel destPixel = new MergePixel(destLine[destX].rgbtRed, destLine[destX].rgbtGreen, destLine[destX].rgbtBlue, 0);

                                            destPixel = CalculatePixelWeight(overlapV, overlapH, realLeft, left, realTop, top, y, topY, x, srcPixel, destPixel);

                                            RGBTRIPLE dest;
                                            dest.rgbtRed = destPixel.red > 255.0 ? (byte)255 : (byte)destPixel.red;
                                            dest.rgbtGreen = destPixel.green > 255.0 ? (byte)255 : (byte)destPixel.green;
                                            dest.rgbtBlue = destPixel.blue > 255.0 ? (byte)255 : (byte)destPixel.blue;
                                            destLine[destX] = dest;
                                        }
                                        else
                                            destLine[destX] = srcLine[x];
                                    }
                                }
                            }
                            else
                            {
                                for (int y = 0; y < slice.Height; y++)
                                {
                                    int srcY = (slice.Height - 1) - y;
                                    int destY = (combined.Height - 1) - (srcY + (int)top);
                                    int topY = y + (int)top;
                                    Scanline<RGBQUAD> srcLine = (Scanline<RGBQUAD>)slice.GetScanline(y);
                                    Scanline<RGBQUAD> destLine = (Scanline<RGBQUAD>)combined.GetScanline(destY);

                                    for (int x = 0; x < slice.Width; x++)
                                    {
                                        int destX = x + (int)left;

                                        // Make sure it's not out of bounds
                                        if (destY >= Height || destY >= Width)
                                            continue;
                                        // Is the pixel in an overlapping Area
                                        if (topY < realTop || destX < realLeft)
                                        {
                                            MergePixel srcPixel = new MergePixel(srcLine[x].rgbRed, srcLine[x].rgbGreen, srcLine[x].rgbBlue, destLine[destX].rgbReserved);
                                            MergePixel destPixel = new MergePixel(destLine[destX].rgbRed, destLine[destX].rgbGreen, destLine[destX].rgbBlue, destLine[destX].rgbReserved);

                                            destPixel = CalculatePixelWeight(overlapV, overlapH, realLeft, left, realTop, top, y, topY, x, srcPixel, destPixel);

                                            RGBQUAD dest = new RGBQUAD();
                                            dest.rgbRed = destPixel.red > 255.0 ? (byte)255 : (byte)destPixel.red;
                                            dest.rgbGreen = destPixel.green > 255.0 ? (byte)255 : (byte)destPixel.green;
                                            dest.rgbBlue = destPixel.blue > 255.0 ? (byte)255 : (byte)destPixel.blue;
                                            dest.rgbReserved = destPixel.alpha > 255.0 ? (byte)255 : (byte)destPixel.alpha;
                                            destLine[destX] = dest;
                                        }
                                        else
                                            destLine[destX] = srcLine[x];
                                    }
                                }
                            }
                            break;
                        case FREE_IMAGE_TYPE.FIT_RGB16:
                            for (int y = 0; y < slice.Height; y++)
                            {
                                int srcY = (slice.Height - 1) - y;
                                int destY = (combined.Height - 1) - (srcY + (int)top);
                                int topY = y + (int)top;
                                Scanline<FIRGB16> srcLine = (Scanline<FIRGB16>)slice.GetScanline(y);
                                Scanline<FIRGB16> destLine = (Scanline<FIRGB16>)combined.GetScanline(destY);

                                for (int x = 0; x < slice.Width; x++)
                                {
                                    int destX = x + (int)left;

                                    // Make sure it's not out of bounds
                                    if (destY >= Height || destY >= Width)
                                        continue;
                                    // Is the pixel in an overlapping Area
                                    if (topY < realTop || destX < realLeft)
                                    {
                                        MergePixel srcPixel = new MergePixel(srcLine[x].red, srcLine[x].green, srcLine[x].blue, 0);
                                        MergePixel destPixel = new MergePixel(destLine[destX].red, destLine[destX].green, destLine[destX].blue, 0);

                                        destPixel = CalculatePixelWeight(overlapV, overlapH, realLeft, left, realTop, top, y, topY, x, srcPixel, destPixel);

                                        FIRGB16 dest = new FIRGB16();
                                        dest.red = (ushort)destPixel.red;
                                        dest.green = (ushort)destPixel.green;
                                        dest.blue = (ushort)destPixel.blue;
                                        destLine[destX] = dest;
                                    }
                                    else
                                        destLine[destX] = srcLine[x];
                                }
                            }
                            break;
                        case FREE_IMAGE_TYPE.FIT_RGBA16:
                            for (int y = 0; y < slice.Height; y++)
                            {
                                int srcY = (slice.Height - 1) - y;
                                int destY = (combined.Height - 1) - (srcY + (int)top);
                                int topY = y + (int)top;
                                Scanline<FIRGBA16> srcLine = (Scanline<FIRGBA16>)slice.GetScanline(y);
                                Scanline<FIRGBA16> destLine = (Scanline<FIRGBA16>)combined.GetScanline(destY);

                                for (int x = 0; x < slice.Width; x++)
                                {
                                    int destX = x + (int)left;

                                    // Make sure it's not out of bounds
                                    if (destY >= Height || destY >= Width)
                                        continue;
                                    // Is the pixel in an overlapping Area
                                    if (topY < realTop || destX < realLeft)
                                    {
                                        MergePixel srcPixel = new MergePixel(srcLine[x].red, srcLine[x].green, srcLine[x].blue, srcLine[x].alpha);
                                        MergePixel destPixel = new MergePixel(destLine[destX].red, destLine[destX].green, destLine[destX].blue, destLine[destX].alpha);

                                        destPixel = CalculatePixelWeight(overlapV, overlapH, realLeft, left, realTop, top, y, topY, x, srcPixel, destPixel);

                                        FIRGBA16 dest = new FIRGBA16();
                                        dest.red = (ushort)destPixel.red;
                                        dest.green = (ushort)destPixel.green;
                                        dest.blue = (ushort)destPixel.blue;
                                        dest.alpha = (ushort)destPixel.alpha;
                                        destLine[destX] = dest;
                                    }
                                    else
                                        destLine[destX] = srcLine[x];
                                }
                            }
                            break;
                        case FREE_IMAGE_TYPE.FIT_RGBAF:
                            for (int y = 0; y < slice.Height; y++)
                            {
                                int srcY = (slice.Height - 1) - y;
                                int destY = (combined.Height - 1) - (srcY + (int)top);
                                int topY = y + (int)top;
                                Scanline<FIRGBAF> srcLine = (Scanline<FIRGBAF>)slice.GetScanline(y);
                                Scanline<FIRGBAF> destLine = (Scanline<FIRGBAF>)combined.GetScanline(destY);

                                for (int x = 0; x < slice.Width; x++)
                                {
                                    int destX = x + (int)left;

                                    // Make sure it's not out of bounds
                                    if (destY >= Height || destY >= Width)
                                        continue;
                                    // Is the pixel in an overlapping Area
                                    if (topY < realTop || destX < realLeft)
                                    {
                                        MergePixel srcPixel = new MergePixel(srcLine[x].red, srcLine[x].green, srcLine[x].blue, destLine[destX].alpha);
                                        MergePixel destPixel = new MergePixel(destLine[destX].red, destLine[destX].green, destLine[destX].blue, destLine[destX].alpha);

                                        destPixel = CalculatePixelWeight(overlapV, overlapH, realLeft, left, realTop, top, y, topY, x, srcPixel, destPixel);

                                        FIRGBAF dest = new FIRGBAF();
                                        dest.red = (float)destPixel.red;
                                        dest.green = (float)destPixel.green;
                                        dest.blue = (float)destPixel.blue;
                                        dest.alpha = (float)destPixel.alpha;
                                        destLine[destX] = dest;
                                    }
                                    else
                                        destLine[destX] = srcLine[x];
                                }
                            }
                            break;
                        case FREE_IMAGE_TYPE.FIT_RGBF:
                            for (int y = 0; y < slice.Height; y++)
                            {
                                int srcY = (slice.Height - 1) - y;
                                int destY = (combined.Height - 1) - (srcY + (int)top);
                                int topY = y + (int)top;
                                Scanline<FIRGBF> srcLine = (Scanline<FIRGBF>)slice.GetScanline(y);
                                Scanline<FIRGBF> destLine = (Scanline<FIRGBF>)combined.GetScanline(destY);

                                for (int x = 0; x < slice.Width; x++)
                                {
                                    int destX = x + (int)left;

                                    // Make sure it's not out of bounds
                                    if (destY >= Height || destY >= Width)
                                        continue;
                                    // Is the pixel in an overlapping Area
                                    if (topY < realTop || destX < realLeft)
                                    {
                                        MergePixel srcPixel = new MergePixel(srcLine[x].red, srcLine[x].green, srcLine[x].blue, 0);
                                        MergePixel destPixel = new MergePixel(destLine[destX].red, destLine[destX].green, destLine[destX].blue, 0);

                                        destPixel = CalculatePixelWeight(overlapV, overlapH, realLeft, left, realTop, top, y, topY, x, srcPixel, destPixel);

                                        FIRGBF dest = new FIRGBF();
                                        dest.red = (float)destPixel.red;
                                        dest.green = (float)destPixel.green;
                                        dest.blue = (float)destPixel.blue;
                                        destLine[destX] = dest;
                                    }
                                    else
                                        destLine[destX] = srcLine[x];
                                }
                            }
                            break;
                    }
                    slice.Dispose();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Error merging image files");
                    mergeSuccessful = false;
                }
            }
            try
            {
                if (mergeSuccessful)
                {
                    combined.Save(mergedFile);
                    combined.Dispose();
                    combined = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    Log += DateTime.Now.ToLongTimeString() + " Full frame " + frameNum + " reconstructed.\n";
                    RenderedFrames.Add(new FinishedFrame(SceneId, mergedFile));
                    File.Delete(tempFile);
                }
                else
                {
                    Log += DateTime.Now.ToLongTimeString() + " Merging frame " + frameNum + " failed.\n";
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error writing combined file");
                mergeSuccessful = false;
            }

            return mergeSuccessful;
        }

        private MergePixel CalculatePixelWeight(double overlapV, double overlapH, double realLeft, double left, double realTop, double top, int y, int topY, int x, MergePixel srcPixel, MergePixel destPixel)
        {
            double srcWeight;
            double destWeight;

            // Check if it's overlapping on the X or Y axis and generate weighting accordingly
            if (topY < realTop)
            {
                srcWeight = 1.0 - Math.Abs(realTop - (y + top)) / Math.Ceiling(overlapV);
                destWeight = 1.0 - srcWeight;
            }
            else
            {
                srcWeight = 1.0 - Math.Abs(realLeft - (x + left)) / Math.Ceiling(overlapH);
                destWeight = 1.0 - srcWeight;
            }

            destPixel.red = (srcPixel.red * srcWeight) + (srcPixel.red * destWeight);
            destPixel.green = (srcPixel.green * srcWeight) + (srcPixel.green * destWeight);
            destPixel.blue = (srcPixel.blue * srcWeight) + (srcPixel.blue * destWeight);
            destPixel.alpha = (srcPixel.alpha * srcWeight) + (srcPixel.alpha * destWeight);

            return destPixel;
        }

        public void ReleaseFrame(int frame, int sliceNumber, string node)
        {
            for (int i = 0; i < _jobs.Count; i++)
            {
                if (_jobs[i].StartFrame == frame && _jobs[i].SliceNumber == sliceNumber)
                {
                    Log += DateTime.Now.ToLongTimeString() + " Frame " + frame + " lost by node " + node;
                    _jobs[i].Retrials++;
                    if (_jobs[i].Retrials > 3)
                        _jobs.RemoveAt(i);
                    else
                    {
                        _jobs[i].ClientId = -1;
                        _jobs[i].Sent = false;
                    }
                    return;
                }
            }
        }

        public void ReleaseClientAllFrames(int clientId,string node)
        {
        	Log += DateTime.Now.ToLongTimeString() + " Lost client " + node + " all frames reset.";
        	foreach (RenderJob t in _jobs)
        	{
        		if (t.ClientId == clientId)
        		{
        			t.ClientId = -1;
        			t.Sent = false;
        			t.EndFrame = t.StartFrame;
        		}
        	}
        }

    	public int RemainingJobs()
        {
            return _jobs.Count;
        }

        public void SendFile(string basePath, string filename, byte[] file)
        {
            if (basePath == "")
                basePath = OutputDir;

            Directory.CreateDirectory(basePath);
            string outFilename = Path.Combine(basePath, filename);
            File.WriteAllBytes(outFilename, file);
        }

        public string SaveImage(int frame, int sliceNumber, byte[] img)
        {
            string sImageFormat = ServerServices.Configs[Config].ImageFormats[ImageFormat];
            string ext = sImageFormat.Substring(sImageFormat.IndexOf('(')+1, (sImageFormat.IndexOf(')') - sImageFormat.IndexOf('('))-1);
            string myPrefix = Prefix;
            
            if (SlicesDown * SlicesAcross > 1)            
                myPrefix = "slice_" + sliceNumber + "_" + Prefix;
            
            string fname = string.Format(FileNameFormats[FileNameFormat], OutputDir, myPrefix, frame, ext);

            File.WriteAllBytes(fname, img);

            RenderedFrames.Add(new FinishedFrame(SceneId,fname));          
            return fname;
        }

        public string SaveImageAlpha(int frame, int sliceNumber, byte[] img)
        {
            string sImageFormat = ServerServices.Configs[Config].ImageFormats[AlphaImageFormat];
            string ext = sImageFormat.Substring(sImageFormat.IndexOf('(') + 1, (sImageFormat.IndexOf(')') - sImageFormat.IndexOf('(')) - 1);
            string myPrefix = AlphaPrefix;

            if (SlicesDown * SlicesAcross > 1)            
                myPrefix = "slice_" + sliceNumber + "_" + AlphaPrefix;
            
            string fname = string.Format(FileNameFormats[FileNameFormat], OutputDir, myPrefix, frame, ext);

            File.WriteAllBytes(fname, img);

            RenderedFrames.Add(new FinishedFrame(SceneId, fname));
            return fname;
        }

        public string SceneId
        {
            get
            {
                return SceneFile.Substring(SceneFile.LastIndexOf('\\') + 1) + " (" + ProjectId + ")";
            }
        }

        public void RemoveAllJobs()
        {
            _jobs.Clear();
        }

        public void CloseLogs()
        {
            IsFinished = true;

            Log += "*******************************************************\n";
            Log += "Project finished on: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "\n";
            Log += "*******************************************************\n";

            if (EmailNotify)
            {
                MailMessage mail = new MailMessage(new MailAddress(EmailFrom), new MailAddress(EmailTo));
                mail.Subject = EmailSubjectOk;

                if (EmailContainLog)
                    mail.Body = Log;
                else
                    mail.Body = "Project finished.";

                SmtpClient smtp = new SmtpClient(SmtpServer);
                if (SmtpUsername != "")
                    smtp.Credentials = new NetworkCredential(SmtpUsername, SmtpPassword);
                smtp.Send(mail);
                mail.Dispose();
            }
        }

        public bool Save(string filename)
        {
            bool success;

            try
            {
                XmlSerializer s = new XmlSerializer(typeof(RenderProject));
                TextWriter w = new StreamWriter(filename);
                s.Serialize(w, this);
                w.WriteLine("");
                w.WriteLine("<!-- Project Definition - Amleto 3.3 -->");
                w.WriteLine("<!-- (c) 2014 - Virtualcoder.co.uk -->");
                w.Close();
                success = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error saving file: " + filename);
                success = false;
            }
            return success;
        }

        public RenderProject Load(string filename)
        {
            RenderProject project = new RenderProject();
            
            try
            {
                XmlSerializer s = new XmlSerializer(typeof(RenderProject));
                TextReader r = new StreamReader(filename);
                project = (RenderProject)s.Deserialize(r);
                r.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error loading file: " + filename);
			}
            return project;
        }
    }
}
