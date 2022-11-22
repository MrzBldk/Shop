using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Models.Picture
{
    public class PicturesViewModel
    {
        [Required]
        public List<string> FilesNames { get; set; }

        [Required]
        public List<IFormFile> FormFiles { get; set; }
    }
}
