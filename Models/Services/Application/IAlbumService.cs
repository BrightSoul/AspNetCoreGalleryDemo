using DemoGallery.Models.InputModels;
using DemoGallery.Models.ViewModels;

namespace DemoGallery.Models.Services.Application
{
    public interface IAlbumService
    {
        void AddImages(ImageUploadInputModel inputModel);
        AlbumViewModel GetAlbum(string album);
        void RotateImageClockwise(ImageEditInputModel inputModel);
        void SetDefaultImage(ImageEditInputModel inputModel);
        void RemoveImage(ImageEditInputModel inputModel);
    }
}