using System.Linq;
using DemoGallery.Models.InputModels;
using DemoGallery.Models.Services.Infrastructure;
using DemoGallery.Models.ViewModels;

namespace DemoGallery.Models.Services.Application
{
    public class AlbumService : IAlbumService
    {
        private readonly IImagePersister imagePersister;

        public AlbumService(IImagePersister imagePersister)
        {
            this.imagePersister = imagePersister;
        }
        public void AddImages(ImageUploadInputModel inputModel)
        {
            foreach (var image in inputModel.Images)
            {
                imagePersister.AddImage(inputModel.Album, image);
            }
        }

        public AlbumViewModel GetAlbum(string album)
        {
            var images = imagePersister.GetImagesInAlbum(album);
            var imageViewModels = images.Select(image => new ImageViewModel(image, imagePersister.IsDefault(album, image)));
            var viewModel = new AlbumViewModel(album, imageViewModels);
            return viewModel;
        }

        public void RemoveImage(ImageEditInputModel inputModel)
        {
            imagePersister.RemoveImage(inputModel.Album, inputModel.Image);
        }

        public void RotateImageClockwise(ImageEditInputModel inputModel)
        {
            imagePersister.RotateImageClockwise(inputModel.Album, inputModel.Image);
        }

        public void SetDefaultImage(ImageEditInputModel inputModel)
        {
            imagePersister.SetDefaultImage(inputModel.Album, inputModel.Image);
        }
    }
}