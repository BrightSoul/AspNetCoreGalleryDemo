using System.Collections.Generic;
using System.Linq;

namespace DemoGallery.Models.ViewModels
{
    public class AlbumViewModel
    {
        public AlbumViewModel(string name, IEnumerable<ImageViewModel> images)
        {
            Name = name;
            Images = images.ToList().AsReadOnly();
        }
        public string Name { get; }
        public IReadOnlyCollection<ImageViewModel> Images { get; }
    }
}