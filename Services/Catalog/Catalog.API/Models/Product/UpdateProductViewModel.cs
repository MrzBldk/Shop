using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Models.Product
{
    public class UpdateProductViewModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int AvailableStock { get; set; }

        [Required]
        public bool IsArchived { get; set; }

        [Required]
        public Guid BrandId { get; set; }

        [Required]
        public Guid TypeId { get; set; }

        [Required]
        public Guid StoreId { get; set; }
    }
}
