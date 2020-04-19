using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DemoGallery.Models.Services.Infrastructure
{
    public interface IImagePersister
    {
        string AddImage(string album, IFormFile file);
        IEnumerable<string> GetImagesInAlbum(string album);
        void RotateImageClockwise(string album, string image);
        void SetDefaultImage(string album, string image);
        void RemoveImage(string album, string image);
        bool IsDefault(string album, string image);
    }
}