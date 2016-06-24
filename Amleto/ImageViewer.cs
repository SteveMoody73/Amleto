using System;
using System.Windows.Forms;
using FreeImageAPI;
using RemoteExecution;

namespace Amleto
{
    public partial class ImageViewer : Form
    {
        private string imageFile = "";
        FIBITMAP originalImage;
        FIBITMAP displayImage;
        FIBITMAP icon;
        ServerSettings _settings;

        public ImageViewer()
        {
            InitializeComponent();
        }

        public ImageViewer(string filename) : this()
        {
            imageFile = filename;      
        }

        public void SetImageFile(string filename)
        {
            imageFile = filename;
        }

        private void ImageViewerShown(object sender, EventArgs e)
        {
            Text = imageFile;

            Cursor.Current = Cursors.WaitCursor;
            ZoomLevels.SelectedIndexChanged -= ZoomLevelsSelectedIndexChanged;

            foreach (int zoom in ImageView.ZoomLevels)
            {
                ZoomLevels.Items.Add(string.Format("{0}%", zoom));
                if (zoom == 100)
                    ZoomLevels.SelectedIndex = ZoomLevels.Items.Count - 1;
            }
            ZoomLevels.SelectedIndexChanged += ZoomLevelsSelectedIndexChanged;

            try
            {
                originalImage = FreeImage.LoadEx(imageFile);
                if (!originalImage.IsNull)
                {
                    Text = Text + " (" + FreeImage.GetHeight(originalImage) + " x " + FreeImage.GetWidth(originalImage) + ")";

                    FREE_IMAGE_TYPE type = FreeImage.GetImageType(originalImage);
                    if (type != FREE_IMAGE_TYPE.FIT_BITMAP)
                    {
                        displayImage = FreeImage.ToneMapping(originalImage, FREE_IMAGE_TMO.FITMO_REINHARD05, 0, -1);
                    }
                    else
                        displayImage = FreeImage.Clone(originalImage);

                    if (!displayImage.IsNull)
                    {
                        ImageView.Image = FreeImage.GetBitmap(displayImage);
                        icon = FreeImage.Rescale(displayImage, 16, 15, FREE_IMAGE_FILTER.FILTER_BOX);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
            Cursor.Current = Cursors.Default;
        }

        private void ImageViewerFormClosing(object sender, FormClosingEventArgs e)
        {
            if (!originalImage.IsNull)
                FreeImage.UnloadEx(ref originalImage);

            if (!displayImage.IsNull)
                FreeImage.UnloadEx(ref displayImage);

            if (!icon.IsNull)
                FreeImage.UnloadEx(ref icon);

            _settings.ViewerWidth = Width;
            _settings.ViewerHeight = Height;
            ServerSettings.SaveSettings(_settings);
        }

        private void ZoomLevelsSelectedIndexChanged(object sender, EventArgs e)
        {
            int zoom = 0;
            string zoomPercent = ZoomLevels.Text.Substring(0, ZoomLevels.Text.Length - 1);

            int.TryParse(zoomPercent, out zoom);

            if (zoom > 0)
                ImageView.Zoom = zoom;
        }

        private void ZoomInClick(object sender, EventArgs e)
        {
            ImageView.ZoomIn();
        }

        private void ZoomOutClick(object sender, EventArgs e)
        {
            ImageView.ZoomOut();
        }

        private void ActualSizeClick(object sender, EventArgs e)
        {
            ImageView.ActualSize();
        }

        private void ImageViewerLoad(object sender, EventArgs e)
        {
            _settings = ServerSettings.LoadSettings();
            Width = _settings.ViewerWidth;
            Height = _settings.ViewerHeight;            
        }
    }
}
