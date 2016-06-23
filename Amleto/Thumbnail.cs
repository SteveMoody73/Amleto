using System;
using FreeImageAPI;
using System.Drawing;
using NLog;
using System.IO;

namespace Amleto
{
    class Thumbnail
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private const string _basePath = @"Cache\thumbnails";

        public static Image CreateImageThumbnail(string project, string filename, int thumbsize, bool force = false)
        {
            Image img = null;
            FIBITMAP image;

            // Filetypes are handled nativly by the image grid, no need to generate thumbnail
            if (filename.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                filename.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                filename.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) ||
                filename.EndsWith(".tif", StringComparison.OrdinalIgnoreCase) ||
                filename.EndsWith(".tiff", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }


                string thumbnail = GenerateThumbnailName(project, thumbsize, filename);

            try
            {
                if (File.Exists(thumbnail) && force == false)
                {
                    try
                    {
                        img = Image.FromFile(thumbnail);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "Error loading thumbnail image");
                        img = null;
                    }
                }
                
                if (img == null)
                {
                    FREE_IMAGE_FORMAT format = FREE_IMAGE_FORMAT.FIF_UNKNOWN;
                    format = FreeImage.GetFileType(filename, 16);
                    if (format != FREE_IMAGE_FORMAT.FIF_UNKNOWN)
                    {
                        image = FreeImage.LoadEx(filename, ref format);

                        // Calculate the thumbnail size based on it's aspect ratio.
                        int sizeX = thumbsize;
                        int sizeY = thumbsize;
                        int width = (int)FreeImage.GetWidth(image);
                        int height = (int)FreeImage.GetHeight(image);
                        float aspect = (float)width / (float)height;

                        if (width > height)
                            sizeY = (int)((float)sizeY / aspect);
                        else
                            sizeX = (int)((float)sizeX * aspect);

                        // Resize the image to the correct thumbnail size
                        FIBITMAP resized = FreeImage.Rescale(image, sizeX, sizeY, FREE_IMAGE_FILTER.FILTER_BOX);
                        if (!resized.IsNull)
                        {
                            FREE_IMAGE_TYPE type = FreeImage.GetImageType(resized);
                            Bitmap thumb = null;

                            switch (type)
                            {
                                case FREE_IMAGE_TYPE.FIT_BITMAP:
                                    thumb = FreeImage.GetBitmap(resized);
                                    break;

                                case FREE_IMAGE_TYPE.FIT_FLOAT:
                                case FREE_IMAGE_TYPE.FIT_DOUBLE:
                                case FREE_IMAGE_TYPE.FIT_RGB16:
                                case FREE_IMAGE_TYPE.FIT_RGBF:
                                case FREE_IMAGE_TYPE.FIT_RGBA16:
                                case FREE_IMAGE_TYPE.FIT_RGBAF:
                                    FIBITMAP mapped = FreeImage.ToneMapping(resized, FREE_IMAGE_TMO.FITMO_REINHARD05, 0, 0.1);

                                    //if (format == FREE_IMAGE_FORMAT.FIF_EXR)
                                    //{
                                    //    // EXR images seem to be mirrored, need to test with more images
                                    //  FreeImage.FlipHorizontal(mapped);
                                    //  FreeImage.FlipVertical(mapped);
                                    //}

                                    thumb = FreeImage.GetBitmap(mapped);
                                    FreeImage.UnloadEx(ref mapped);
                                    break;
                                default:
                                    logger.Info("Unable to create thumbnail from filename {0}\nImage format is {1}", filename, type.ToString());
                                    break;
                            }
                            img = thumb;
                            SaveCachedThumbnail(project, thumbsize, filename, img);
                        }
                        FreeImage.UnloadEx(ref resized);
                        FreeImage.UnloadEx(ref image);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error generating image thumbnail");
            }
            return img;
        }

        private static string GenerateThumbnailName(string project, int thumbsize, string filename)
        {
            string thumbName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Amleto");
            thumbName = Path.Combine(thumbName, _basePath);
            thumbName = Path.Combine(thumbName, project + "_" + thumbsize.ToString());
            thumbName = Path.Combine(thumbName, Path.GetFileNameWithoutExtension(filename) + ".png");
            Directory.CreateDirectory(Path.GetDirectoryName(thumbName));
            return thumbName;
        }

        private static void SaveCachedThumbnail(string project, int thumbsize, string filename, Image image)
        {
            string thumbnail = GenerateThumbnailName(project, thumbsize, filename);

            try
            {
                if (image != null)
                    image.Save(thumbnail);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error saving thumbnail image");
            }
        }
    }
}
