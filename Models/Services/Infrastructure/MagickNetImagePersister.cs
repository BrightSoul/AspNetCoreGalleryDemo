using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DemoGallery.Models.Options;
using ImageMagick;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace DemoGallery.Models.Services.Infrastructure
{
    public class MagickNetImagePersister : IImagePersister
    {
        private readonly IWebHostEnvironment env;
        private readonly IOptionsMonitor<AlbumOptions> options;
        const string imageExtension = ".jpg";
        const string defaultImageExtension = ".default.jpg";

        public MagickNetImagePersister(IWebHostEnvironment env, IOptionsMonitor<AlbumOptions> options)
        {
            ResourceLimits.Width = 4000;
            ResourceLimits.Height = 4000;
            this.env = env;
            this.options = options;
        }
        public string AddImage(string album, IFormFile file)
        {
            using var stream = file.OpenReadStream();
            using var magickImage = new MagickImage(stream);
            magickImage.Resize(options.CurrentValue.Width, options.CurrentValue.Height);

            string imagePath = GenerateImagePath(album);
            magickImage.Write(imagePath, MagickFormat.Jpg);
            string imageName = Path.GetFileName(imagePath);
            return imageName;
        }

        public IEnumerable<string> GetImagesInAlbum(string album)
        {
            var directory = GetDirectoryForAlbum(album);
            var imagePaths = Directory.EnumerateFiles(directory).Select(imagepath => Path.GetFileName(imagepath));
            return imagePaths;
        }

        public void RemoveImage(string album, string imageName)
        {
            var imagePath = GetPathForImage(album, imageName);
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }

        public void RotateImageClockwise(string album, string imageName)
        {
            var imagePath = GetPathForImage(album, imageName);
            if (!File.Exists(imagePath))
            {
                throw new InvalidOperationException($"Could't find image {imageName} in album {album}");
            }
            using var magickImage = new MagickImage(imagePath);
            magickImage.Rotate(90);
            magickImage.Write(imagePath);
        }

        public void SetDefaultImage(string album, string image)
        {
            var currentDefaultImage = GetPathOfDefaultImage(album);
            if (currentDefaultImage != null)
            {
                File.Move(currentDefaultImage, MakeRegularImagePath(currentDefaultImage));
            }
            var imagePath = GetPathForImage(album, image);
            File.Move(imagePath, MakeDefaultImagePath(imagePath));
        }

        private string MakeDefaultImagePath(string imagePath)
        {
            return imagePath.Replace(imageExtension, defaultImageExtension);
        }

        private string MakeRegularImagePath(string imagePath)
        {
            return imagePath.Replace(defaultImageExtension, imageExtension);
        }

        private string GetDirectoryForAlbum(string album)
        {
            var directory = Path.Combine(env.ContentRootPath, options.CurrentValue.Directory, album);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            return directory;
        }

        private string GetPathForImage(string album, string imageName)
        {
            var directory = GetDirectoryForAlbum(album);
            return Path.Combine(directory, imageName);
        }

        private string GetPathOfDefaultImage(string album)
        {
            var directory = GetDirectoryForAlbum(album);
            return Directory.GetFiles(directory, "*.default.*").FirstOrDefault();
        }

        private string GenerateImagePath(string album)
        {
            var directory = GetDirectoryForAlbum(album);
            string imageName = $"{DateTime.Now.Ticks}.jpg";
            var imagePath = Path.Combine(directory, imageName);
            if (!GetImagesInAlbum(album).Any())
            {
                imagePath = MakeDefaultImagePath(imagePath);
            }
            return imagePath;
        }

        public bool IsDefault(string album, string image)
        {
            return image.Contains(defaultImageExtension);
        }
    }
}