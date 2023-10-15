using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Models.Brand
{
    public class CreateBrandViewModel
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
