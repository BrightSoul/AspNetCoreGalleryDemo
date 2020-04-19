using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace DemoGallery.Models.InputModels
{
    public class ImageEditInputModel : IValidatableObject
    {
        [Required(ErrorMessage = "Il nome dell'album è richiesto")]
        public string Album { get; set; }

        [Required(ErrorMessage = "Il nome dell'immagine è richiesto")]
        public string Image { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var invalidCharacters = Path.GetInvalidFileNameChars();
            if (Album == null || Album.IndexOfAny(invalidCharacters) >= 0)
            {
                yield return new ValidationResult($"L'album ${Album} contiene caratteri non validi");
            }
        }
    }
}