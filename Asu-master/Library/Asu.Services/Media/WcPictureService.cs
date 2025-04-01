using Asu.Core.Domain.Customization;

namespace Asu.Services.Media
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.IO;

    using ImageResizer;

    using Asu.Core;
    using Asu.Core.Data;
    using Asu.Core.Domain.Catalog;
    using Asu.Core.Domain.Media;
    using Configuration;
    using Events;
    using Logging;

    public class WcPictureService : PictureService
    {
        private readonly IStoreContext storeContext;

        public WcPictureService(IRepository<Picture> pictureRepository, IRepository<ProductPicture> productPictureRepository, IRepository<AdditionalImage> additionalImageRepository, ISettingService settingService, IWebHelper webHelper, ILogger logger, IEventPublisher eventPublisher, MediaSettings mediaSettings, IStoreContext store)
            : base(pictureRepository, productPictureRepository, additionalImageRepository, settingService, webHelper, logger, eventPublisher, mediaSettings, store)
        {
            this.storeContext = store;
        }

        /// <summary>
        /// Loads a picture from file
        /// </summary>
        /// <param name="pictureId">Picture identifier</param>
        /// <param name="mimeType">MIME type</param>
        /// <returns>Picture binary</returns>
        protected override byte[] LoadPictureFromFile(int pictureId, string mimeType)
        {
            var lastPart = GetFileExtensionFromMimeType(mimeType);
            var folder = pictureId / 10000;
            var fileName = $"{pictureId:00000000}_0.{lastPart}";
#if DEBUG
            string url = $"{this._webHelper.GetStoreLocation()}content/images/{folder}/{fileName}";
            var webClient = new System.Net.WebClient();
            try
            {
                return webClient.DownloadData(new Uri(url));
            }
            catch
            {
                return new byte[0];
            }
#endif
            var filePath = GetPictureLocalPath(fileName, folder.ToString(CultureInfo.InvariantCulture));
            if (!File.Exists(filePath))
                return new byte[0];

            return File.ReadAllBytes(filePath);
        }

        /// <summary>
        /// Save picture on file system
        /// </summary>
        /// <param name="pictureId">Picture identifier</param>
        /// <param name="pictureBinary">Picture binary</param>
        /// <param name="mimeType">MIME type</param>
        protected override void SavePictureInFile(int pictureId, byte[] pictureBinary, string mimeType)
        {
            var lastPart = GetFileExtensionFromMimeType(mimeType);
            var folder = pictureId / 10000;
            var fileName = string.Format("{0}_0.{1}", pictureId.ToString("00000000"), lastPart);

            var newPath = Path.Combine(_webHelper.MapPath("~/content/images/"), folder.ToString(CultureInfo.InvariantCulture));

            File.WriteAllBytes(GetPictureLocalPath(fileName, folder.ToString(CultureInfo.InvariantCulture)), pictureBinary);
        }

        /// <summary>
        /// Delete a picture on file system
        /// </summary>
        /// <param name="picture">Picture</param>
        protected override void DeletePictureOnFileSystem(Picture picture)
        {
            if (picture == null)
                throw new ArgumentNullException("picture");

            var lastPart = GetFileExtensionFromMimeType(picture.MimeType);
            var folder = picture.Id / 10000;
            var fileName = string.Format("{0}_0.{1}", picture.Id.ToString("00000000"), lastPart);
            var filePath = GetPictureLocalPath(fileName, folder.ToString(CultureInfo.InvariantCulture));
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        /// <summary>
        /// Delete picture thumbs
        /// </summary>
        /// <param name="picture">Picture</param>
        protected override void DeletePictureThumbs(Picture picture)
        {
            var filter = string.Format("{0}*.*", picture.Id.ToString("00000000"));
            var folder = picture.Id / 10000;
            var thumbDirectoryPath = _webHelper.MapPath(string.Format("~/content/images/thumbs/{0}", folder));
            if (Directory.Exists(thumbDirectoryPath))
            {
                var currentFiles = Directory.GetFiles(thumbDirectoryPath, filter, SearchOption.AllDirectories);
                foreach (var currentFileName in currentFiles)
                {
                    var thumbFilePath = GetThumbLocalPath(currentFileName, picture.Id);
                    if (File.Exists(thumbFilePath))
                        File.Delete(thumbFilePath);
                }
            }
        }

        /// <summary>
        /// Get picture local path. Used when images stored on file system (not in the database). Creates directory if it don't exist
        /// </summary>
        /// <param name="fileName">Filename</param>
        /// <param name="directoryName">Path in the folder</param>
        /// <returns>Local picture path</returns>
        protected string GetPictureLocalPath(string fileName, string directoryName)
        {
            var imagesDirectoryPath = _webHelper.MapPath("~/content/images/");
            var filePath = Path.Combine(Path.Combine(imagesDirectoryPath, directoryName), fileName);
            return filePath;
        }

        /// <summary>
        /// Get a picture URL
        /// </summary>
        /// <param name="picture">Picture instance</param>
        /// <param name="targetSize">The target picture size (longest side)</param>
        /// <param name="showDefaultPicture">A value indicating whether the default picture is shown</param>
        /// <param name="storeLocation">Store location URL; null to use determine the current store location automatically</param>
        /// <param name="defaultPictureType">Default picture type</param>
        /// <returns>Picture URL</returns>
        public override string GetPictureUrl(Picture picture,
            int targetSize = 0,
            bool showDefaultPicture = true,
            string storeLocation = null,
            PictureType defaultPictureType = PictureType.Entity)
        {
            string url = string.Empty;
            byte[] pictureBinary = null;
            if (picture != null)
                pictureBinary = LoadPictureBinary(picture);
            if (picture == null || pictureBinary == null || pictureBinary.Length == 0)
            {
                if (showDefaultPicture)
                {
                    url = GetDefaultPictureUrl(targetSize, defaultPictureType, storeLocation);
                }
                return url;
            }

            string lastPart = GetFileExtensionFromMimeType(picture.MimeType);
            string thumbFileName;
            if (picture.IsNew)
            {
                DeletePictureThumbs(picture);

                //we do not validate picture binary here to ensure that no exception ("Parameter is not valid") will be thrown
                picture = UpdatePicture(picture.Id,
                    pictureBinary,
                    picture.MimeType,
                    picture.SeoFilename,
                    false,
                    false);
            }
            lock (s_lock)
            {
                //string seoFileName = picture.SeoFilename; // = GetPictureSeName(picture.SeoFilename); //just for sure
                string seoFileName = null;
                if (targetSize == 0)
                {
                    thumbFileName = !string.IsNullOrEmpty(seoFileName) ? $"{picture.Id:00000000}_{seoFileName}.{lastPart}" : $"{picture.Id:00000000}_0.{lastPart}";
                    var thumbFilePath = GetThumbLocalPath(thumbFileName, picture.Id);
                    if (!File.Exists(thumbFilePath))
                    {
                        File.WriteAllBytes(thumbFilePath, pictureBinary);
                    }
                }
                else
                {
                    thumbFileName = !string.IsNullOrEmpty(seoFileName) ? $"{picture.Id:00000000}_{seoFileName}_{targetSize}.{lastPart}" : $"{picture.Id:00000000}_{targetSize}.{lastPart}";
                    var thumbFilePath = GetThumbLocalPath(thumbFileName, picture.Id);
                    if (!File.Exists(thumbFilePath))
                    {
                        using (var stream = new MemoryStream(pictureBinary))
                        {
                            Bitmap b = null;
                            try
                            {
                                //try-catch to ensure that picture binary is really OK. Otherwise, we can get "Parameter is not valid" exception if binary is corrupted for some reasons
                                b = new Bitmap(stream);
                            }
                            catch (ArgumentException exc)
                            {
                                _logger.Error($"Error generating picture thumb. ID={picture.Id}", exc);
                            }
                            if (b == null)
                            {
                                //bitmap could not be loaded for some reasons
                                return url;
                            }

                            var newSize = CalculateDimensions(b.Size, targetSize);

                            var destStream = new MemoryStream();
                            ImageBuilder.Current.Build(b, destStream, new ResizeSettings()
                            {
                                Width = newSize.Width,
                                Height = newSize.Height,
                                Scale = ScaleMode.Both,
                                Quality = _mediaSettings.DefaultImageQuality
                            });
                            var destBinary = destStream.ToArray();

                            File.WriteAllBytes(thumbFilePath, destBinary);

                            b.Dispose();
                        }
                    }
                }
            }

            url = GetThumbUrl(thumbFileName, picture.Id, storeLocation);
            return url;
        }

        public override string GetWidthHeightPictureUrl(Picture picture, int maxWidth = 0, int maxHeight = 0, bool showDefaultPicture = true,
            string storeLocation = null, PictureType defaultPictureType = PictureType.Entity, bool useSeoName = true)
        {
            string url = string.Empty;
            byte[] pictureBinary = null;
            if (picture != null)
                pictureBinary = LoadPictureBinary(picture);
            if (picture == null || pictureBinary == null || pictureBinary.Length == 0)
            {
                if (showDefaultPicture)
                {
                    url = GetWidthHeightDefaultPictureUrl(maxWidth, maxHeight, defaultPictureType, storeLocation);
                }
                return url;
            }

            string lastPart = GetFileExtensionFromMimeType(picture.MimeType);
            string thumbFileName;
            if (picture.IsNew)
            {
                DeletePictureThumbs(picture);

                //we do not validate picture binary here to ensure that no exception ("Parameter is not valid") will be thrown
                picture = UpdatePicture(picture.Id,
                    pictureBinary,
                    picture.MimeType,
                    picture.SeoFilename,
                    false,
                    false);
            }
            lock (s_lock)
            {
                //string seoFileName = useSeoName ? picture.SeoFilename : string.Empty; // = GetPictureSeName(picture.SeoFilename); //just for sure
                string seoFileName = null;
                if (maxWidth == 0 && maxHeight == 0)
                {
                    thumbFileName = !string.IsNullOrEmpty(seoFileName) ? $"{picture.Id:00000000}_{seoFileName}.{lastPart}" : $"{picture.Id:00000000}_0.{lastPart}";
                    var thumbFilePath = GetThumbLocalPath(thumbFileName, picture.Id);
                    if (!File.Exists(thumbFilePath))
                    {
                        File.WriteAllBytes(thumbFilePath, pictureBinary);
                    }
                }
                else
                {
                    thumbFileName = !string.IsNullOrEmpty(seoFileName) ? $"{picture.Id:00000000}_{seoFileName}_{maxWidth}_{maxHeight}.{lastPart}" : $"{picture.Id:00000000}_{maxWidth}_{maxHeight}.{lastPart}";
                    var thumbFilePath = GetThumbLocalPath(thumbFileName, picture.Id);
                    if (!File.Exists(thumbFilePath))
                    {
                        using (var stream = new MemoryStream(pictureBinary))
                        {
                            Bitmap b = null;
                            try
                            {
                                //try-catch to ensure that picture binary is really OK. Otherwise, we can get "Parameter is not valid" exception if binary is corrupted for some reasons
                                b = new Bitmap(stream);
                            }
                            catch (ArgumentException exc)
                            {
                                _logger.Error(string.Format("Error generating picture thumb. ID={0}", picture.Id), exc);
                            }
                            if (b == null)
                            {
                                //bitmap could not be loaded for some reasons
                                return url;
                            }

                            var newSize = CalculateWidthHeightDimensions(b.Size, maxWidth, maxHeight);

                            var destStream = new MemoryStream();
                            ImageBuilder.Current.Build(b, destStream, new ResizeSettings
                            {
                                Width = newSize.Width,
                                Height = newSize.Height,
                                Scale = ScaleMode.Both,
                                Quality = _mediaSettings.DefaultImageQuality
                            });
                            var destBinary = destStream.ToArray();

                            File.WriteAllBytes(thumbFilePath, destBinary);

                            b.Dispose();
                        }
                    }
                }
            }
            url = GetThumbUrl(thumbFileName, picture.Id, storeLocation);
            return url;
        }

        protected override string GetFileExtensionFromMimeType(string mimeType)
        {
            if (mimeType == null)
                return null;

            //also see System.Web.MimeMapping for more mime types

            string[] parts = mimeType.Split('/');
            string lastPart = parts[parts.Length - 1];
            switch (lastPart)
            {
                case "pjpeg":
                    lastPart = "jpg";
                    break;
                case "x-png":
                    lastPart = "png";
                    break;
                case "x-icon":
                    lastPart = "ico";
                    break;
                case "jpeg":
                    lastPart = "jpeg";
                    break;
                case "gif":
                    lastPart = "gif";
                    break;
            }
            return lastPart;
        }

        /// <summary>
        /// Get a picture local path
        /// </summary>
        /// <param name="picture">Picture instance</param>
        /// <param name="targetSize">The target picture size (longest side)</param>
        /// <param name="showDefaultPicture">A value indicating whether the default picture is shown</param>
        /// <returns></returns>
        public override string GetThumbLocalPath(Picture picture, int targetSize = 0, bool showDefaultPicture = true)
        {
            string url = GetPictureUrl(picture, targetSize, showDefaultPicture);
            if (String.IsNullOrEmpty(url))
                return String.Empty;

            return GetThumbLocalPath(Path.GetFileName(url), picture.Id);
        }

        /// <summary>
        /// Get picture (thumb) local path with subdirectory from picture Id
        /// </summary>
        /// <param name="thumbFileName">Filename</param>
        /// <param name="pictureId">Picture Id</param>
        /// <returns>Local picture thumb path</returns>
        protected string GetThumbLocalPath(string thumbFileName, int pictureId)
        {
            var folder = pictureId / 10000;
            var thumbsDirectoryPath = _webHelper.MapPath(string.Format("~/content/images/thumbs/{0}", folder));
            /*if (_mediaSettings.MultipleThumbDirectories)
            {
                //get the first two letters of the file name
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(thumbFileName);
                if (fileNameWithoutExtension != null && fileNameWithoutExtension.Length > MULTIPLE_THUMB_DIRECTORIES_LENGTH)
                {
                    var subDirectoryName = fileNameWithoutExtension.Substring(0, MULTIPLE_THUMB_DIRECTORIES_LENGTH);
                    thumbsDirectoryPath = Path.Combine(thumbsDirectoryPath, subDirectoryName);
                    if (!System.IO.Directory.Exists(thumbsDirectoryPath))
                    {
                        System.IO.Directory.CreateDirectory(thumbsDirectoryPath);
                    }
                }
            }*/

            if (!System.IO.Directory.Exists(thumbsDirectoryPath))
            {
                System.IO.Directory.CreateDirectory(thumbsDirectoryPath);
            }

            var thumbFilePath = Path.Combine(thumbsDirectoryPath, thumbFileName);
            return thumbFilePath;
        }

        /// <summary>
        /// Get picture (thumb) URL 
        /// </summary>
        /// <param name="thumbFileName">Filename</param>
        /// <param name="storeLocation">Store location URL; null to use determine the current store location automatically</param>
        /// <returns>Local picture thumb path</returns>
        protected string GetThumbUrl(string thumbFileName, int pictureId, string storeLocation = null)
        {
            storeLocation = !String.IsNullOrEmpty(storeLocation)
                                    ? storeLocation
                                    : _webHelper.GetStoreLocation();
            var folder = pictureId / 10000;
            var url = $"{this._webHelper.GetStoreLocation()}content/images/thumbs/{folder}/{thumbFileName}";

            /*if (_mediaSettings.MultipleThumbDirectories)
            {
                //get the first two letters of the file name
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(thumbFileName);
                if (fileNameWithoutExtension != null && fileNameWithoutExtension.Length > MULTIPLE_THUMB_DIRECTORIES_LENGTH)
                {
                    var subDirectoryName = fileNameWithoutExtension.Substring(0, MULTIPLE_THUMB_DIRECTORIES_LENGTH);
                    url = url + subDirectoryName + "/";
                }
            }*/

            return url;
        }
    }
}
