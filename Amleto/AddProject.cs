using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using RemoteExecution;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Amleto
{
    public partial class AddProject : Form
    {
        MasterServer _server;
        RenderProject _project;

        List<string> _cameraTypes = new List<string>();
        List<CameraSettings> _cameras = new List<CameraSettings>();
		bool _shouldClose = true;
		string _currentFileName = "";
		int _projectId = -1;

		public AddProject(MasterServer server)
        {
            _server = server;
            InitializeComponent();

            foreach (string s in server.ImageFormats(0))
            {
                listImageFormat.Items.Add(s);
                AlphaImageFormat.Items.Add(s);
            }
            try
            {
                listImageFormat.SelectedIndex = 0;
                AlphaImageFormat.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
				Debug.WriteLine("Error selecting the image file format" + ex);
                MessageBox.Show("Error while selecting the image file format.");
            }

            foreach (string s in RenderProject.FileNameFormats)
                listOuputFormat.Items.Add(string.Format(s, "outputDir", "prefix", 1, ".ext"));
            try
            {
                listOuputFormat.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
				Debug.WriteLine("Error selecting output name format: " + ex);
                MessageBox.Show("Error while selecting the file output name format.");
            }

            foreach (string s in server.ConfigNames)
                listConfig.Items.Add(s);

            try
            {
                listConfig.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
				Debug.WriteLine("Error selecting config: " + ex);
                MessageBox.Show("Error while selecting the config.");
            }

            try
            {
                renderMode.SelectedIndex = 2;
                classicAntialias.SelectedIndex = 0;
                reconFilter.SelectedIndex = 0;
                radiosityType.SelectedIndex = 0;
                samplingPattern.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
				Debug.WriteLine("Error selecting defaults: " + ex);
                MessageBox.Show("Error while selecting some defaults.");
            }

            emailNotify.Checked = Properties.Settings.Default.SendEmail;
            emailTo.Text = Properties.Settings.Default.ToEmail;
            emailSubjectOk.Text = Properties.Settings.Default.SubjectOk;
            emailSubjectNotOk.Text = Properties.Settings.Default.SubjectNotOk;

            renderSetupTabs.TabPages.Remove(logTab);
			renderSetupTabs.TabPages.Remove(qualityTab);
			renderSetupTabs.TabPages.Remove(cameraTab);
			renderSetupTabs.TabPages.Remove(giTab);
        }

        public AddProject(MasterServer server, RenderProject prj) 
            : this(server)
        {
            Text = "Edit project";
            if (prj.IsFinished)
            {
                btnAdd.Text = "Restart";
                textViewLog.Text = prj.Log.Replace("\n", "\r\n");
                renderSetupTabs.TabPages.Add(logTab);
            }
            else
            {
                btnAdd.Text = "Save";
            }
            btnAdd.Enabled = true;
            btnLoadProject.Enabled = false;

            _projectId = prj.ProjectId;
            _project = prj;

            textScene.Enabled = false;
            btnSelectScene.Enabled = false;

            RestoreProjectInfo(_project);
        }

        private void RestoreProjectInfo(RenderProject project)
        {
			OverrideSettings.Checked = project.OverrideSettings;
            OverwriteFrames.Checked = project.OverwriteFrames;
            textScene.Text = project.SceneFile;
            textContentDir.Text = project.ContentDir;
            textOutputDir.Text = project.OutputDir;
            listConfig.SelectedIndex = project.Config;
            textPrefix.Text = project.Prefix;
            listImageFormat.SelectedIndex = project.ImageFormat;
            AlphaImageFormat.SelectedIndex = project.AlphaImageFormat;
            listOuputFormat.SelectedIndex = project.FileNameFormat;

            textStartFrame.Text = project.StartFrame.ToString();
            textEndFrame.Text = project.EndFrame.ToString();
            textFrameStep.Text = project.FrameSteps.ToString();
            imageWidth.Text = project.Width.ToString();
            imageHeight.Text = project.Height.ToString();
            renderBlock.Text = project.Block.ToString();
            imageAspect.Text = project.Aspect.ToString();
            slicesDown.Text = project.SlicesDown.ToString();
            slicesAcross.Text = project.SlicesAcross.ToString();
            slicesOverlap.Text = project.Overlap.ToString();
            deleteSplitFrames.Checked = project.DeleteSplitFrames;

            renderMode.SelectedIndex = project.RenderMode;
            traceShadow.Checked = (project.RenderEffect & 1) != 0;
            traceReflection.Checked = (project.RenderEffect & 2) != 0;
            traceRefraction.Checked = (project.RenderEffect & 4) != 0;
            traceTransp.Checked = (project.RenderEffect & 8) != 0;
            traceOcclusion.Checked = (project.RenderEffect & 16) != 0;

            recusionLimit.Value = project.RecursionLimit;
            renderLine.Checked = (project.RenderLine == 1);
            adaptiveThreshold.Text = "" + project.AdaptiveThreshold;
            adaptiveSampling.Checked = project.AdaptiveSampling == 1;
            reconFilter.SelectedIndex = project.ReconFilter;
            softFilter.Checked = project.FilterType == 1;

            if (project.StartFrame == project.EndFrame)
            {
                slicesDown.Enabled = true;
                slicesAcross.Enabled = true;
                slicesOverlap.Enabled = true;
                lblNbSlices.Enabled = true;
                lblSlicesOverlap.Enabled = true;
                deleteSplitFrames.Enabled = true;
            }
            else
            {
                slicesDown.Enabled = false;
                slicesAcross.Enabled = false;
                slicesOverlap.Enabled = false;
                lblNbSlices.Enabled = false;
                lblSlicesOverlap.Enabled = false;
                deleteSplitFrames.Enabled = false;
            }

            masterList.Items.Clear();

            textContentDir.Text = Directory.GetParent(Directory.GetParent(textScene.Text).FullName).FullName;
            string[] lines = _server.FileReadAllLines(textScene.Text);
            _cameraTypes.Clear();
            cameraName.Items.Clear();
            cameraType.Text = "Classic";
            foreach (string l in lines)
            {
                if (l.StartsWith("CameraName "))
                    cameraName.Items.Add(l.Substring(l.LastIndexOf(' ') + 1));
                else if (l.StartsWith("Plugin CameraHandler"))
                {
                    string[] p = l.Split(' ');
                    _cameraTypes.Add(p[3]);
                }
                else if (l.StartsWith("Plugin MasterHandler"))
                {
                    string[] p = l.Split(' ');
                    masterList.Items.Add(p[3], project.StrippedMaster.Contains(p[3]));
                }
            }
            cameraName.SelectedIndex = project.Camera;
            cameraAntialias.Value = project.CameraAntialias;
            samplingPattern.SelectedIndex = project.SamplingPattern;

            if (cameraType.Text == "Classic")
            {
                // New antialias
                if (project.AntialiasLevel == -1)
                {
                    if (project.Antialias == 0)
                        classicAntialias.SelectedIndex = 0;
                    else
                        classicAntialias.SelectedIndex = project.AntialiasLevel + 1;
                }
                // Old
                else
                    classicAntialias.SelectedIndex = 14 + project.Antialias * 2 + project.EnhanceAA;
            }
            else
                classicAntialias.SelectedIndex = 0;
            cameraName_SelectedIndexChanged(null, null);

            radiosityType.SelectedIndex = project.Radiosity + project.RadiosityType;
            interpolatedGI.Checked = (project.InterpolatedGI == 1);
            backdropTranspGI.Checked = (project.BackdropTranspGI == 1);
            cachedGI.Checked = (project.CachedGI == 1);
            volumetricGI.Checked = (project.VolumetricGI == 1);
            useAmbientGI.Checked = (project.UseAmbientGI == 1);
            directionalGI.Checked = (project.DirectionalGI == 1);
            intensityGI.Text = string.Format("{0:0.00}", project.IntensityGI * 100.0);
            toleranceGI.Text = string.Format("{0:0.00}", project.ToleranceGI * 100.0);
            rayGI.Value = project.RayGI;
            minEvalGI.Text = string.Format("{0:0.00}", project.MinEvalGI * 1000.0);
            minPixelGI.Text = string.Format("{0:0.00}", project.MinPixelGI);
            indirectGI.Value = project.IndirectGI;

            emailNotify.Checked = project.EmailNotify;
            emailTo.Text = project.EmailTo;
            emailSubjectOk.Text = project.EmailSubjectOk;
            emailSubjectNotOk.Text = project.EmailSubjectNotOk;
            emailContainLog.Checked = project.EmailContainLog;
        }

        public AddProject(MasterServer server, string fname)
            : this(server)
        {
            textScene.Text = fname;
            UpdateSceneInfo();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _shouldClose = true;
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SendEmail = emailNotify.Checked;
            Properties.Settings.Default.ToEmail = emailTo.Text;
            Properties.Settings.Default.SubjectOk = emailSubjectOk.Text;
            Properties.Settings.Default.SubjectNotOk = emailSubjectNotOk.Text;
            Properties.Settings.Default.Save();

            if (_projectId != -1 && _project.IsFinished)
            {
                if (MessageBox.Show("Saving changes will start the project.\nAre you sure you want to restart it?", "Save changed", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                _project.IsFinished = false;
            }
            else if (_projectId != -1)
            {
                if (MessageBox.Show("Saving changes will reset the project status.", "Save changed", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }
            if (!_server.DirectoryExists(textOutputDir.Text))
            {
                if (MessageBox.Show("Output directory doesn't exists. Do you want to create it?", "Directory doesn't exist", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                _server.CreateDirectory(textOutputDir.Text);
            }

            if (_projectId == -1)
                _project = new RenderProject();

            StoreProjectInfo(_project);

            if (_projectId == -1)
                _server.AddProject(_project);
            else
                _server.ReplaceProject(_projectId, _project);                

            _shouldClose = true;
            Close();
        }

        private void StoreProjectInfo(RenderProject project)
        {
            project.SceneFile = textScene.Text;
            project.ContentDir = textContentDir.Text;
            project.OutputDir = textOutputDir.Text;
            project.Config = listConfig.SelectedIndex;
            project.StartFrame = Convert.ToInt32(textStartFrame.Text);
            project.EndFrame = Convert.ToInt32(textEndFrame.Text);
            project.FrameSteps = Convert.ToInt32(textFrameStep.Text);
            project.Prefix = textPrefix.Text;
            project.ImageFormat = listImageFormat.SelectedIndex;
            project.AlphaImageFormat = AlphaImageFormat.SelectedIndex;
            project.SaveAlpha = SaveAlpha.Checked;
            project.AlphaPrefix = AlphaPrefix.Text;
            project.AlphaImageFormat = AlphaImageFormat.SelectedIndex;
            project.SaveAlpha = SaveAlpha.Checked;
            project.AlphaPrefix = AlphaPrefix.Text;
            project.FileNameFormat = listOuputFormat.SelectedIndex;
            project.Width = Convert.ToInt32(imageWidth.Text);
            project.Height = Convert.ToInt32(imageHeight.Text);
            project.Block = Convert.ToInt32(renderBlock.Text);
            project.SlicesDown = Convert.ToInt32(slicesDown.Text);
            project.SlicesAcross = Convert.ToInt32(slicesAcross.Text);
            project.Overlap = Convert.ToInt32(slicesOverlap.Text);
            project.DeleteSplitFrames = deleteSplitFrames.Checked;
            project.ReconFilter = reconFilter.SelectedIndex;
            project.AdaptiveSampling = (adaptiveSampling.Checked ? 1 : 0);
            project.FilterType = (softFilter.Checked ? 1 : 0);
			project.OverrideSettings = OverrideSettings.Checked;
            project.OverwriteFrames = OverwriteFrames.Checked;

			if (classicAntialias.SelectedIndex == 0) // No antialias
            {
                project.Antialias = 0;
                project.AntialiasLevel = -1;
                /*project.reconFilter = 0;
                project.adaptiveSampling = 0;
                project.enhanceAA = 0;*/
            }
			else if (classicAntialias.SelectedIndex >= 14) // Old antialias
            {
				project.Antialias = (classicAntialias.SelectedIndex - 14) / 2;
				project.EnhanceAA = (classicAntialias.SelectedIndex - 14) % 2;
                project.AntialiasLevel = -1;
            }
            else // New
            {
				if (classicAntialias.SelectedIndex > 13)
                    project.Antialias = 4;
				else if (classicAntialias.SelectedIndex > 9)
                    project.Antialias = 3;
				else if (classicAntialias.SelectedIndex  > 5)
                    project.Antialias = 2;
                else
                    project.Antialias = 1;
				project.AntialiasLevel = classicAntialias.SelectedIndex - 1;
                project.EnhanceAA = 0;
            }

            project.Aspect = Convert.ToDouble(imageAspect.Text);
            project.RenderEffect = (traceShadow.Checked ? 1 : 0) |
                (traceReflection.Checked ? 2 : 0) |
                (traceRefraction.Checked ? 4 : 0) |
                (traceTransp.Checked ? 8 : 0) |
                (traceOcclusion.Checked ? 16 : 0);
            project.RenderMode = renderMode.SelectedIndex;
            project.RecursionLimit = (int)recusionLimit.Value;
            project.RenderLine = (renderLine.Checked ? 1 : 0);
            project.AdaptiveThreshold = Convert.ToDouble(adaptiveThreshold.Text);

            project.Camera = cameraName.SelectedIndex;
            project.SamplingPattern = samplingPattern.SelectedIndex;
            project.CameraAntialias = (int)cameraAntialias.Value;

            if (radiosityType.SelectedIndex == 0)
            {
                project.Radiosity = 0;
                project.RadiosityType = 0;
            }
            else
            {
                project.Radiosity = 1;
                project.RadiosityType = radiosityType.SelectedIndex - 1;
            }
            project.InterpolatedGI = interpolatedGI.Checked ? 1 : 0;
            project.BackdropTranspGI = backdropTranspGI.Checked ? 1 : 0;
            project.CachedGI = cachedGI.Checked ? 1 : 0;
            project.VolumetricGI = volumetricGI.Checked ? 1 : 0;
            project.UseAmbientGI = useAmbientGI.Checked ? 1 : 0;
            project.DirectionalGI = directionalGI.Checked ? 1 : 0;
            project.IntensityGI = (Convert.ToDouble(intensityGI.Text) / 100.0);
            project.ToleranceGI = (Convert.ToDouble(toleranceGI.Text) / 100.0);
            project.RayGI = (int)rayGI.Value;
            project.MinEvalGI = Convert.ToDouble(minEvalGI.Text) / 1000.0;
            project.MinPixelGI = Convert.ToDouble(minPixelGI.Text);
            project.IndirectGI = (int)indirectGI.Value;

            project.EmailNotify = emailNotify.Checked;
            project.EmailTo = emailTo.Text;
            project.EmailSubjectOk = emailSubjectOk.Text;
            project.EmailSubjectNotOk = emailSubjectNotOk.Text;
            project.EmailContainLog = emailContainLog.Checked;

            project.EmailFrom = _server.EmailFrom;
            project.SmtpServer = _server.SmtpServer;
            project.SmtpUsername = _server.SmtpUsername;
            project.SmtpPassword = _server.SmtpPassword;

            foreach (string s in masterList.CheckedItems)
            {
                project.StrippedMaster.Add(s);
                _server.StrippedMasterAdd(s);
            }

            foreach (string s in masterList.Items)
            {
                if (!project.StrippedMaster.Contains(s))
                    _server.StrippedMasterRemove(s);
            }
        }

        private void btnSelectScene_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Scene|*.lws|All Files|*.*";
            dialog.InitialDirectory = textContentDir.Text;
            dialog.Title = "Select the scene";
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            textScene.Text = dialog.FileName; 
            btnAdd.Enabled = true;

            UpdateSceneInfo();
        }

        /// <summary>
        /// Parse the scene, and retreive it's settings.
        /// </summary>
        public void UpdateSceneInfo()
        {
            int antialias = 0;
            bool enhance = false;
            bool antialiaslevel = false;

            renderLine.Checked = false;
            adaptiveSampling.Checked = false;
            traceShadow.Checked = false;
            traceReflection.Checked = false;
            traceRefraction.Checked = false;
            traceTransp.Checked = false;
            traceOcclusion.Checked = false;
            softFilter.Checked = false;
            renderLine.Checked = false;
            SaveAlpha.Checked = false;
            slicesDown.Text = "1";
            slicesAcross.Text = "1";
            slicesOverlap.Text = "5";
            deleteSplitFrames.Checked = false;

            imageAspect.Text = "1.0";

            renderMode.SelectedIndex = 0;
            classicAntialias.SelectedIndex = 0;
            reconFilter.SelectedIndex = 0;
            recusionLimit.Value = 16;

            adaptiveThreshold.Text = "0.1";

            masterList.Items.Clear();
            cameraName.Items.Clear();
            cameraAntialias.Value = 1;
            samplingPattern.SelectedIndex = 0;
            cameraType.Text = "Classic";
            _cameraTypes.Clear();
            volumetricGI.Checked = true;
            directionalGI.Checked = false;
            useAmbientGI.Checked = false;
            cachedGI.Checked = false;
            backdropTranspGI.Checked = false;
            interpolatedGI.Checked = false;
            indirectGI.Value = 1;
            minEvalGI.Text = "20";
            minPixelGI.Text = "4.0";
            rayGI.Value = 48;
            toleranceGI.Text = "0.0";
            intensityGI.Text = "100.0";

            radiosityType.SelectedIndex = 0;

            _cameras.Clear();
            int camerapos = -1;
            int currentCamera = 0;

            textContentDir.Text = Directory.GetParent(Directory.GetParent(textScene.Text).FullName).FullName;
            string[] lines = _server.FileReadAllLines(textScene.Text);
            foreach (string l in lines)
            {
                try
                {
                    if (l.StartsWith("CameraName "))
                    {
                        _cameras.Add(new CameraSettings { Width = 640, Height = 480, Aspect = 1.0, UseGlobalResolution = false });
                        camerapos++;
                        cameraName.Items.Add(l.Substring(l.LastIndexOf(' ') + 1));
                        cameraName.SelectedIndex = 0;
                    }
                    else if (l.StartsWith("FrameSize "))
                    {
                        string[] p = l.Split(' ');
                        try
                        {
                            _cameras[camerapos].Width = int.Parse(p[1]);
                            _cameras[camerapos].Height = int.Parse(p[2]);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Error splitting FrameSize: " + ex);
                        }
                    }
                    else if (l.StartsWith("UseGlobalResolution "))
                    {
                        string[] p = l.Split(' ');
                        if (p[1][0] == '0')
                            _cameras[camerapos].UseGlobalResolution = false;
                        else
                            _cameras[camerapos].UseGlobalResolution = true;
                    }
                    else if (l.StartsWith("PixelAspect "))
                    {
                        string[] p = l.Split(' ');
                        try
                        {
                            _cameras[camerapos].Aspect = double.Parse(p[1]);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Error PixelAspect: " + ex);
                        }
                    }
                    else if (l.StartsWith("Plugin CameraHandler"))
                    {
                        string[] p = l.Split(' ');
                        _cameraTypes.Add(p[3]);
                    }
                    else if (l.StartsWith("CurrentCamera"))
                    {
                        try
                        {
                            currentCamera = int.Parse(l.Substring(l.LastIndexOf(' ') + 1));
                            cameraName.SelectedIndex = currentCamera;
                            cameraType.Text = _cameraTypes[cameraName.SelectedIndex];
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Error CurrentCamera: " + ex);
                        }
                    }
                    else if (l.StartsWith("AASamples "))
                        cameraAntialias.Value = Convert.ToInt32(l.Substring(l.LastIndexOf(' ') + 1));
                    else if (l.StartsWith("Sampler "))
                        samplingPattern.SelectedIndex = Convert.ToInt32(l.Substring(l.LastIndexOf(' ') + 1));
                    else if (l.StartsWith("EnableRadiosity"))
                        radiosityType.SelectedIndex = Convert.ToInt32(l.Substring(l.LastIndexOf(' ') + 1));
                    else if (l.StartsWith("RadiosityType") && radiosityType.SelectedIndex > 0)
                        radiosityType.SelectedIndex += Convert.ToInt32(l.Substring(l.LastIndexOf(' ') + 1));
                    else if (l.StartsWith("RadiosityInterpolated"))
                        interpolatedGI.Checked = l.EndsWith("1");
                    else if (l.StartsWith("RadiosityTransparency"))
                        backdropTranspGI.Checked = l.EndsWith("1");
                    else if (l.StartsWith("CacheRadiosity"))
                        cachedGI.Checked = l.EndsWith("1");
                    else if (l.StartsWith("VolumetricRadiosity"))
                        volumetricGI.Checked = !l.EndsWith("0");
                    else if (l.StartsWith("RadiosityUseAmbient"))
                        useAmbientGI.Checked = l.EndsWith("1");
                    else if (l.StartsWith("RadiosityDirectionalRays"))
                        directionalGI.Checked = l.EndsWith("1");
                    else if (l.StartsWith("RadiosityIntensity"))
                        intensityGI.Text = "" + (Convert.ToDouble(l.Substring(l.LastIndexOf(' ') + 1)) * 100.0);
                    else if (l.StartsWith("RadiosityTolerance"))
                        toleranceGI.Text = string.Format("{0:0.00}", (Convert.ToDouble(l.Substring(l.LastIndexOf(' ') + 1)) * 100.0));
                    else if (l.StartsWith("RadiosityRays"))
                        rayGI.Value = Convert.ToInt32(l.Substring(l.LastIndexOf(' ') + 1));
                    else if (l.StartsWith("RadiosityMinSpacing"))
                        minEvalGI.Text = string.Format("{0:0.00}", (Convert.ToDouble(l.Substring(l.LastIndexOf(' ') + 1)) * 1000.0));
                    else if (l.StartsWith("RadiosityMinPixelSpacing"))
                        minPixelGI.Text = string.Format("{0:0.00}", (Convert.ToDouble(l.Substring(l.LastIndexOf(' ') + 1))));
                    else if (l.StartsWith("IndirectBounces"))
                        indirectGI.Value = Convert.ToInt32(l.Substring(l.LastIndexOf(' ') + 1));
                    else if (l.StartsWith("Plugin MasterHandler"))
                    {
                        string[] p = l.Split(' ');
                        masterList.Items.Add(p[3], _server.IsStripped(p[3]));
                    }
                    else if (l.StartsWith("SaveAlpha "))
                    {
                        if (l.EndsWith("1"))
                            SaveAlpha.Checked = true;
                        else
                            SaveAlpha.Checked = false;
                    }
                    else if (l.StartsWith("SaveAlphaImagesPrefix "))
                    {
                        AlphaPrefix.Text = l.Substring(l.LastIndexOf('\\') + 1);
                    }
                    else if (l.StartsWith("SaveRGBImagesPrefix "))
                    {
                        textOutputDir.Text = Directory.GetParent(l.Substring(20)).FullName;
                        textPrefix.Text = l.Substring(l.LastIndexOf('\\') + 1);
                    }
                    else if (l.StartsWith("AlphaImageSaver "))
                    {
                        string imgFormat = l.Substring(l.IndexOf(' ') + 1);
                        AlphaImageFormat.SelectedIndex = _server.ImageFormats(0).IndexOf(imgFormat);
                    }
                    else if (l.StartsWith("FirstFrame "))
                        textStartFrame.Text = l.Substring(l.LastIndexOf(' ') + 1);
                    else if (l.StartsWith("LastFrame "))
                        textEndFrame.Text = l.Substring(l.LastIndexOf(' ') + 1);
                    else if (l.StartsWith("FrameStep "))
                        textFrameStep.Text = l.Substring(l.LastIndexOf(' ') + 1);
                    else if (l.StartsWith("RGBImageSaver "))
                    {
                        string imgFormat = l.Substring(l.IndexOf(' ') + 1);
                        listImageFormat.SelectedIndex = _server.ImageFormats(0).IndexOf(imgFormat);
                    }
                    else if (l.StartsWith("OutputFilenameFormat "))
                        listOuputFormat.SelectedIndex = Convert.ToInt32(l.Substring(l.LastIndexOf(' ') + 1));
                    else if (l.StartsWith("RenderMode "))
                        renderMode.SelectedIndex = Convert.ToInt32(l.Substring(l.LastIndexOf(' ') + 1));
                    else if (l.StartsWith("RayTraceEffects "))
                    {
                        int effect = Convert.ToInt32(l.Substring(l.LastIndexOf(' ') + 1));
                        traceShadow.Checked = ((effect & 1) != 0);
                        traceReflection.Checked = ((effect & 2) != 0);
                        traceRefraction.Checked = ((effect & 4) != 0);
                        traceTransp.Checked = ((effect & 8) != 0);
                        traceOcclusion.Checked = ((effect & 16) != 0);
                    }
                    else if (l.StartsWith("GlobalFrameSize "))
                    {
                        string[] p = l.Split(' ');
                        imageWidth.Text = p[1];
                        imageHeight.Text = p[2];
                    }
                    else if (l.StartsWith("GlobalPixelAspect "))
                        imageAspect.Text = l.Substring(l.LastIndexOf(' ') + 1);
                    else if (l.StartsWith("RayRecursionLimit "))
                        recusionLimit.Value = Convert.ToInt32(l.Substring(l.LastIndexOf(' ') + 1));
                    else if (l.StartsWith("RenderLines "))
                    {
                        if (l.Substring(l.LastIndexOf(' ') + 1) == "1")
                            renderLine.Checked = true;
                    }
                    else if (l.StartsWith("AdaptiveSampling "))
                    {
                        if (l.Substring(l.LastIndexOf(' ') + 1) == "1")
                            adaptiveSampling.Checked = true;
                    }
                    else if (l.StartsWith("AdaptiveThreshold "))
                        adaptiveThreshold.Text = l.Substring(l.LastIndexOf(' ') + 1);
                    else if (l.StartsWith("FilterType "))
                    {
                        if (l.Substring(l.LastIndexOf(' ') + 1) == "1")
                            softFilter.Checked = true;
                    }
                    else if (l.StartsWith("Antialiasing "))
                    {
                        antialias = Convert.ToInt32(l.Substring(l.LastIndexOf(' ') + 1));
                    }
                    else if (l.StartsWith("EnhancedAA "))
                    {
                        if (l.Substring(l.LastIndexOf(' ') + 1) == "1")
                            enhance = true;
                    }
                    else if (l.StartsWith("AntiAliasingLevel "))
                    {
                        antialiaslevel = true;
                        int n = Convert.ToInt32(l.Substring(l.LastIndexOf(' ') + 1));
                        if (n > 0)
                            antialias = 1 + n;
                        else if (antialias > 0)
                        {
                            antialias = antialias * 2 + 14;
                            if (enhance)
                                antialias++;
                        }
                    }
                    else if (l.StartsWith("ReconstructionFilter "))
                    {
                        reconFilter.SelectedIndex = Convert.ToInt32(l.Substring(l.LastIndexOf(' ') + 1));
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error processing line: " + l + "\n" + e);
                }
            }

            cameraName_SelectedIndexChanged(null, null);

            if (cameraType.Text == "Classic")
            {
                if (antialiaslevel)
                    classicAntialias.SelectedIndex = antialias;
                else if (antialias > 0)
                {
                    antialias = antialias * 2 + 14;
                    if (enhance)
                        antialias++;
                    classicAntialias.SelectedIndex = antialias;
                }
            }

            if (textOutputDir.Text == "")
            {
                if (Directory.Exists(textContentDir.Text + "\\Output"))
                    textOutputDir.Text = textContentDir.Text + "\\Output";
                else
                    textOutputDir.Text = textContentDir.Text;
            }

            if (textStartFrame.Text == textEndFrame.Text)
            {
                slicesDown.Enabled = true;
                slicesAcross.Enabled = true;
                slicesOverlap.Enabled = true;
                lblNbSlices.Enabled = true;
                lblSlicesOverlap.Enabled = true;
                deleteSplitFrames.Enabled = true;
            }
            else
            {
                slicesDown.Text = "1";
                slicesAcross.Text = "1";
                slicesDown.Enabled = false;
                slicesAcross.Enabled = false;
                slicesOverlap.Enabled = false;
                lblNbSlices.Enabled = false;
                lblSlicesOverlap.Enabled = false;
                deleteSplitFrames.Enabled = false;
            }

            try
            {
                if (_cameras[currentCamera].UseGlobalResolution == false)
                {
                    imageWidth.Text = "" + _cameras[currentCamera].Width;
                    imageHeight.Text = "" + _cameras[currentCamera].Height;
                    imageAspect.Text = "" + _cameras[currentCamera].Aspect;
                }
            }
            catch (Exception ex)
            {
				Debug.WriteLine("Error UseGlobalResolution: " + ex);
            }
        }

        private void btnSelectContentDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = textContentDir.Text;

            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            textContentDir.Text = dlg.SelectedPath;
            dlg.Dispose();
        }

        private void btnSelectOuputDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = textOutputDir.Text;

            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            textOutputDir.Text = dlg.SelectedPath;
            dlg.Dispose();
        }

        private bool CheckHasErrors()
        {
            return CheckHasErrors(this);
        }

        private bool CheckHasErrors(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (checkValues.GetError(c) != "")
                    return true;
                if (CheckHasErrors(c))
                    return true;
            }
            return false;
        }

        private void textStartFrame_Validating(object sender, CancelEventArgs e)
        {
            int a;
            int b = 0;
            
			try
			{
				b = Convert.ToInt32(textEndFrame.Text);
			}
            catch (Exception ex)
            {
				Debug.WriteLine("Error EndFrameText: " + ex);
            }
            
			checkValues.SetError(textStartFrame, "");
            //btnAdd.Enabled = true;
            if (CheckHasErrors() == false && textScene.Text != "")
                btnAdd.Enabled = true;
            try
            {
                a = Convert.ToInt32(textStartFrame.Text);
            }
            catch (Exception ex)
            {
				Debug.WriteLine("Error Start frame should contain a number:" + ex);
				checkValues.SetError(textStartFrame, "Should contain a number");
                btnAdd.Enabled = false;
                return;
            }
            
			if (b < a)
            {
                checkValues.SetError(textStartFrame, "Should be smaller or equal of the end frame");
                btnAdd.Enabled = false;
            }

            if (a == b)
            {
                slicesDown.Enabled = true;
                slicesAcross.Enabled = true;
                slicesOverlap.Enabled = true;
                lblNbSlices.Enabled = true;
                lblSlicesOverlap.Enabled = true;
                deleteSplitFrames.Enabled = true;
            }
            else
            {
                slicesDown.Enabled = false;
                slicesAcross.Enabled = false;
                slicesDown.Text = "1";
                slicesAcross.Text = "1";

                slicesOverlap.Enabled = false;
                lblNbSlices.Enabled = false;
                lblSlicesOverlap.Enabled = false;
                deleteSplitFrames.Enabled = false;
            }
        }

        private void textEndFrame_Validating(object sender, CancelEventArgs e)
        {
            int a;
            int b = 0;

            try
            {
            	b = Convert.ToInt32(textStartFrame.Text);
            }
            catch (Exception ex)
            {
				Debug.WriteLine("Error: " + ex);
            }

            checkValues.SetError(textEndFrame, "");
            if (CheckHasErrors() == false && textScene.Text != "")
                btnAdd.Enabled = true;
            try
            {
                a = Convert.ToInt32(textEndFrame.Text);
            }
            catch (Exception ex)
            {
				Debug.WriteLine("error should contain a number: " + ex);
                checkValues.SetError(textEndFrame, "Should contain a number");
                btnAdd.Enabled = false;
                return;
            }
            if (b > a)
            {
                checkValues.SetError(textEndFrame, "Should be greater or equal of the start frame");
                btnAdd.Enabled = false;
            }

            if (a == b)
            {
                slicesDown.Enabled = true;
                slicesAcross.Enabled = true;
                slicesOverlap.Enabled = true;
                lblNbSlices.Enabled = true;
                lblSlicesOverlap.Enabled = true;
                deleteSplitFrames.Enabled = true;
            }
            else
            {
                slicesDown.Enabled = false;
                slicesAcross.Enabled = false;
                slicesDown.Text = "1";
                slicesAcross.Text = "1";
                slicesOverlap.Enabled = false;
                lblNbSlices.Enabled = false;
                lblSlicesOverlap.Enabled = false;
                deleteSplitFrames.Enabled = false;
            }
        }

        private void textPrefix_Validating(object sender, CancelEventArgs e)
        {
            checkValues.SetError(textPrefix, "");
            if (CheckHasErrors() == false && textScene.Text != "")
                btnAdd.Enabled = true;

            Regex exp = new Regex("[&\\?\\\\\\*<> ]");
            if (exp.IsMatch(textPrefix.Text))
            {
                checkValues.SetError(textPrefix, "Contains invalid characters");
                btnAdd.Enabled = false;
                return;
            }
            if (textPrefix.Text.Trim() == "")
            {
                checkValues.SetError(textPrefix, "Need to contains something");
                btnAdd.Enabled = false;
                return;
            }
        }

        private void listConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = listImageFormat.SelectedItem.ToString();
            if (selected.Contains("_"))
                selected = selected.Split('_')[1];
            listImageFormat.Items.Clear();
            int p = 0;
            listImageFormat.SelectedIndex = -1;
            foreach (string s in _server.ImageFormats(listConfig.SelectedIndex))
            {
                listImageFormat.Items.Add(s);
                string v = s;
                if (v.Contains("_"))
                    v = v.Split('_')[1];
                if (v == selected)
                    listImageFormat.SelectedIndex = p;
                p++;
            }
            if (listImageFormat.SelectedIndex == -1)
                listImageFormat.SelectedIndex = 0;

            /*int n = listImageFormat.SelectedIndex;
            listImageFormat.Items.Clear();
            foreach (string s in server.ImageFormats(0))
                listImageFormat.Items.Add(s);
            if (n < listImageFormat.Items.Count)
                listImageFormat.SelectedIndex = n;
            else
                listImageFormat.SelectedIndex = 0;*/
        }

        private void adaptiveSampling_CheckedChanged(object sender, EventArgs e)
        {
            adaptiveThreshold.Enabled = adaptiveSampling.Checked;
            lblAdaptiveThreshold.Enabled = adaptiveSampling.Checked;
        }

        private void classicAntialias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (classicAntialias.SelectedIndex == 0)
            {
                /*softFilter.Enabled = false;
                adaptiveSampling.Enabled = false;
                lblAdaptiveThreshold.Enabled = false;
                adaptiveThreshold.Enabled = false;
                reconFilter.Enabled = false;
                lblReconFilter.Enabled = false;*/
            }
            else
            {
                /*softFilter.Enabled = true;
                adaptiveSampling.Enabled = true;
                adaptiveThreshold.Enabled = adaptiveSampling.Checked;
                lblAdaptiveThreshold.Enabled = adaptiveSampling.Checked;
                lblReconFilter.Enabled = true;
                reconFilter.Enabled = true;*/
            }
        }

        private void cameraName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cameraName.SelectedIndex >= _cameraTypes.Count)
                return;
            cameraType.Text = _cameraTypes[cameraName.SelectedIndex];
            if (cameraType.Text == "Classic")
            {
                lblCameraAntialias.Enabled = false;
                cameraAntialias.Enabled = false;
                lblSamplingPattern.Enabled = false;
                samplingPattern.Enabled = false;

                lblAntialias.Enabled = true;
                classicAntialias.Enabled = true;
                classicAntialias_SelectedIndexChanged(null, null);
            }
            else
            {
                lblCameraAntialias.Enabled = true;
                cameraAntialias.Enabled = true;
                lblSamplingPattern.Enabled = true;
                samplingPattern.Enabled = true;

                lblAntialias.Enabled = false;
                classicAntialias.Enabled = false;
                classicAntialias.SelectedIndex = 0;
                classicAntialias_SelectedIndexChanged(null, null);
            }
        }

        private void radiosityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radiosityType.SelectedIndex == 0)
            {
                interpolatedGI.Enabled = false;
                cachedGI.Enabled = false;
                backdropTranspGI.Enabled = false;
                volumetricGI.Enabled = false;
                useAmbientGI.Enabled = false;
                directionalGI.Enabled = false;
                intensityGI.Enabled = false;
                lblIntensityGI.Enabled = false;
                toleranceGI.Enabled = false;
                lblToleranceGI.Enabled = false;
                rayGI.Enabled = false;
                lblRayGI.Enabled = false;
                minEvalGI.Enabled = false;
                lblMinEvalGI.Enabled = false;
                indirectGI.Enabled = false;
                lblIndirectGI.Enabled = false;
                minPixelGI.Enabled = false;
                lblMinPixelGI.Enabled = false;
            }
            else
            {
                interpolatedGI.Enabled = true;
                if (interpolatedGI.Checked || radiosityType.SelectedIndex == 3)
                    cachedGI.Enabled = true;
                else
                    cachedGI.Enabled = false;
                if (radiosityType.SelectedIndex == 1)
                    backdropTranspGI.Enabled = true;
                else
                    backdropTranspGI.Enabled = false;
                volumetricGI.Enabled = true;
                useAmbientGI.Enabled = true;
                directionalGI.Enabled = true;
                intensityGI.Enabled = true;
                intensityGI.Enabled = true;
                lblIntensityGI.Enabled = true;
                rayGI.Enabled = true;
                lblRayGI.Enabled = true;
                if (radiosityType.SelectedIndex == 1)
                {
                    indirectGI.Enabled = false;
                    lblIndirectGI.Enabled = false;
                }
                else
                {
                    indirectGI.Enabled = true;
                    lblIndirectGI.Enabled = true;
                }

                if (interpolatedGI.Checked || radiosityType.SelectedIndex == 3)
                {
                    toleranceGI.Enabled = true;
                    lblToleranceGI.Enabled = true;
                    minEvalGI.Enabled = true;
                    lblMinEvalGI.Enabled = true;
                    minPixelGI.Enabled = true;
                    lblMinPixelGI.Enabled = true;
                }
                else
                {
                    toleranceGI.Enabled = false;
                    lblToleranceGI.Enabled = false;
                    minEvalGI.Enabled = false;
                    lblMinEvalGI.Enabled = false;
                    minPixelGI.Enabled = false;
                    lblMinPixelGI.Enabled = false;
                }
            }
        }

        private void interpolatedGI_CheckedChanged(object sender, EventArgs e)
        {
            radiosityType_SelectedIndexChanged(sender, e);
        }

        private void positiveNumber_Validating(object sender, CancelEventArgs e)
        {
            checkValues.SetError((TextBox)sender, "");
            if (CheckHasErrors() == false)
                btnAdd.Enabled = true;
            int a;
            if (int.TryParse(((TextBox)sender).Text, out a) == false || a < 1)
            {
                checkValues.SetError((TextBox)sender, "Need to contain a number greater than 1");
                btnAdd.Enabled = false;
                return;
            }
        }

        private void positiveDouble_Validating(object sender, CancelEventArgs e)
        {
            checkValues.SetError((TextBox)sender, "");
            if (CheckHasErrors() == false)
                btnAdd.Enabled = true;
            double a;
            if (double.TryParse(((TextBox)sender).Text, out a) == false || a <= 0.0)
            {
                checkValues.SetError((TextBox)sender, "Need to contain a number greater than 0.0");
                btnAdd.Enabled = false;
                return;
            }
        }

        private void positiveZeroDouble_Validating(object sender, CancelEventArgs e)
        {
            checkValues.SetError((TextBox)sender, "");
            if (CheckHasErrors() == false)
                btnAdd.Enabled = true;
            double a;
            if (double.TryParse(((TextBox)sender).Text, out a) == false || a < 0.0)
            {
                checkValues.SetError((TextBox)sender, "Need to contain a number greater or equal of 0.0");
                btnAdd.Enabled = false;
                return;
            }
        }

        private void btnSaveProject_Click(object sender, EventArgs e)
        {
            _shouldClose = false;
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = _currentFileName;
            dlg.AddExtension = true;
            dlg.DefaultExt = "xprj";
            dlg.Filter = "Amleto project file (*.xprj)|*.xprj";
            if (dlg.ShowDialog() != DialogResult.OK || dlg.FileName == "")
            {
                _shouldClose = false;
                return;
            }

            FileInfo f = new FileInfo(dlg.FileName);
            Text = "Add project to the queue - " + f.Name;
            _currentFileName = f.Name;

            RenderProject project = new RenderProject();
            StoreProjectInfo(project);
            if (project.Save(dlg.FileName) == false)
            {
                MessageBox.Show("Error while saving " + f.Name, "Error while saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            _shouldClose = false;
        }

        private void btnLoadProject_Click(object sender, EventArgs e)
        {
            _shouldClose = false;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "xprj";
            dlg.Filter = "Amleto project file (*.xprj)|*.xprj";
            if (dlg.ShowDialog() != DialogResult.OK || dlg.FileName == "")
            {
                _shouldClose = false;
                return;
            }
            try
            {
                RenderProject project = new RenderProject();
                project = project.Load(dlg.FileName);

                RestoreProjectInfo(project);
                btnAdd.Enabled = true;

                FileInfo f = new FileInfo(dlg.FileName);
                this.Text = "Add project to the queue - " + f.Name;
                _currentFileName = f.Name;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while restoring", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            dlg.Dispose();
            _shouldClose = false;
        }

        private void AddProject_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_shouldClose)
                e.Cancel = true;
            _shouldClose = true;
        }

		private void OverrideSettings_CheckedChanged(object sender, EventArgs e)
		{
			if (OverrideSettings.Checked)
			{
				renderSetupTabs.TabPages.Add(qualityTab);
				renderSetupTabs.TabPages.Add(cameraTab);
				renderSetupTabs.TabPages.Add(giTab);
			}
			else
			{
				renderSetupTabs.TabPages.Remove(qualityTab);
				renderSetupTabs.TabPages.Remove(cameraTab);
				renderSetupTabs.TabPages.Remove(giTab);
			}
		}

        private void AddProject_Load(object sender, EventArgs e)
        {
            slicesDown.Text = "1";
            slicesAcross.Text = "1";
            slicesDown.Enabled = false;
            slicesAcross.Enabled = false;
            slicesOverlap.Enabled = false;
            deleteSplitFrames.Enabled = false;
            lblNbSlices.Enabled = false;
            lblSlicesOverlap.Enabled = false;
        }
    }
}