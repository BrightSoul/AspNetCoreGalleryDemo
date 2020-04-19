using System;

namespace DemoGallery.Models.ViewModels
{
    public class ImageViewModel
    {
        public ImageViewModel(string name, bool isDefault)
        {
            Name = name;
            IsDefault = isDefault;
        }
        public string Name { get; }
        public bool IsDefault { get; }
    }
}