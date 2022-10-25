using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Models.Brand
{
    public class BrandViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
