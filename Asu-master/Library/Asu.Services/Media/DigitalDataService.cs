namespace Asu.Services.Media
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Web;

    using Asu.Core;
    using Asu.Core.Data;
    using Core.Domain.ProductGroups;

    using ImageResizer;

    using Asu.Core.Domain.Media;

    using Asu.Core.Domain.Stores;

    public class DigitalDataService : IDigitalDataService
    {
        private readonly IRepository<DigitalData> digitalDataRepository;
        private readonly IWebHelper webHelper;
        private readonly MediaSettings mediaSettings;
        private readonly HttpContextBase httpContext;
        private readonly IStoreContext storeContext;

        public DigitalDataService(IRepository<DigitalData> digitalDataRepository,
            IWebHelper webHelper,
            MediaSettings mediaSettings,
            HttpContextBase httpContext,
            IStoreContext storeContext)
        {
            this.digitalDataRepository = digitalDataRepository;
            this.webHelper = webHelper;
            this.mediaSettings = mediaSettings;
            this.httpContext = httpContext;
            this.storeContext = storeContext;
        }

        public DigitalData GetById(int id)
        {
            return id <= 0 ? null : this.digitalDataRepository.GetById(id);
        }

        public string GetUrl(int id)
        {
            var digitalData = this.GetById(id);
            return this.GetUrl(digitalData);
        }

        public string GetUrl(DigitalData digitalData)
        {
            if (digitalData == null)
            {
                return null;
            }

            return this.GetUrl(digitalData.Path);
        }

        public string GetThumbUrl(DigitalData digitalData, int maxWidth = 0, int maxHeight = 0)
        {
            if (digitalData == null)
            {
                return null;
            }

            if (maxWidth == 0 || maxHeight == 0)
            {
                return this.GetUrl(digitalData);
            }

            switch (digitalData.Type)
            {
                case DigitalDataType.Picture:
                    return this.GetThumbUrl(digitalData.Path, maxWidth, maxHeight);
                default:
                    return this.GetUrl(digitalData.Path);
            }
        }

        public string GetDefaultPictureUrl()
        {
            var baseUri = new Uri(this.webHelper.GetStoreLocation());
            return new Uri(baseUri, $"content/images/{this.storeContext.CurrentStore.GetDefaultPictureNameWithoutExtension()}.gif").ToString();
        }

        private string GetUrl(string path)
        {
            try
            {
                if (Uri.IsWellFormedUriString(path, UriKind.RelativeOrAbsolute))
                {
                    return path;
                }

                var localPath = this.GetLocalPath(path);
                if (!this.IsLocalHost() && !File.Exists(localPath))
                {
                    return null;
                }

                var baseUri = new Uri(this.webHelper.GetStoreLocation());
                return new Uri(baseUri, Path.Combine("\\content\\digital\\", path).Replace("\\", "/")).ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string GetLocalPath(string relativePath)
        {
            return Path.Combine(this.webHelper.MapPath("~/content/digital"), relativePath.Replace("/", "\\"));
        }

        private string GetThumbUrl(string digitalDataPath, int maxWidth, int maxHeight)
        {
            try
            {
                var localPath = this.GetLocalPath(digitalDataPath);
                if (!this.IsLocalHost() && !File.Exists(localPath))
                {
                    return null;
                }

                var thumbRelativeLocalPath = this.GetThumbRelativeLocalPath(digitalDataPath, maxWidth, maxHeight);
                var thumLocalPath = this.GetThumbLocalPath(digitalDataPath, maxWidth, maxHeight);
                if (!this.IsLocalHost() && !File.Exists(thumLocalPath))
                {
                    if (!this.CreateThumb(digitalDataPath, maxWidth, maxHeight))
                    {
                        return this.GetUrl(digitalDataPath);
                    }
                }

                var baseUri = new Uri(this.webHelper.GetStoreLocation());
                return new Uri(baseUri, Path.Combine("\\content\\digital\\thumbs\\", thumbRelativeLocalPath).Replace("\\", "/")).ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string GetThumbLocalPath(string digitalDataPath, int maxWidth, int maxHeight)
        {
            var thumbRelativeLocalPath = this.GetThumbRelativeLocalPath(digitalDataPath, maxWidth, maxHeight);
            return Path.Combine(this.webHelper.MapPath("~/content/digital/thumbs/"), thumbRelativeLocalPath);
        }

        private string GetThumbRelativeLocalPath(string digitalDataPath, int maxWidth, int maxHeight)
        {
            digitalDataPath = digitalDataPath.Replace("/", "\\");
            var directory = Path.GetDirectoryName(digitalDataPath);
            if (string.IsNullOrEmpty(directory))
            {
                return null;
            }

            return Path.Combine(directory, string.Format("{0}_{1}_{2}{3}", Path.GetFileNameWithoutExtension(digitalDataPath), maxWidth, maxHeight, Path.GetExtension(digitalDataPath)));
        }

        private bool CreateThumb(string digitalDataPath, int maxWidth, int maxHeight)
        {
            try
            {
                var localPath = this.GetLocalPath(digitalDataPath);
                var pictureBinary = File.ReadAllBytes(localPath);
                using (var stream = new MemoryStream(pictureBinary))
                {
                    var bitmap = new Bitmap(stream);
                    var newSize = CalculateWidthHeightDimensions(bitmap.Size, maxWidth, maxHeight);

                    var destStream = new MemoryStream();
                    ImageBuilder.Current.Build(bitmap, destStream, new ResizeSettings
                    {
                        Width = newSize.Width,
                        Height = newSize.Height,
                        Scale = ScaleMode.Both,
                        Quality = this.mediaSettings.DefaultImageQuality
                    });

                    var destBinary = destStream.ToArray();
                    var thumbLocalPath = this.GetThumbLocalPath(digitalDataPath, maxWidth, maxHeight);
                    var directory = Path.GetDirectoryName(thumbLocalPath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    File.WriteAllBytes(thumbLocalPath, destBinary);

                    bitmap.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static Size CalculateWidthHeightDimensions(Size originalSize, int maxWidth = 0, int maxHeight = 0)
        {
            var newSize = new Size();
            var ratioX = (float)maxWidth / originalSize.Width;
            var ratioY = (float)maxHeight / originalSize.Height;
            var ratio = Math.Min(ratioX, ratioY);

            newSize.Width = (int)(originalSize.Width * ratio);
            newSize.Height = (int)(originalSize.Height * ratio);

            if (newSize.Width < 1)
            {
                newSize.Width = 1;
            }

            if (newSize.Height < 1)
            {
                newSize.Height = 1;
            }

            return newSize;
        }

        private bool IsLocalHost()
        {
            if (this.httpContext.Request == null || this.httpContext.Request.Url == null)
            {
                return false;
            }

            return this.httpContext.Request.Url.Host == "localhost";
        }
    }
}