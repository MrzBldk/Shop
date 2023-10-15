using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.DAL.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string[] PicturesUris { get; set; }
        public int AvailableStock { get; set; }
        public bool IsArchived { get; set; }


        [ForeignKey("Brand")]
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }

        [ForeignKey("Type")]
        public Guid TypeId { get; set; }
        public Type Type { get; set; }

        public Guid StoreId { get; set; }
    }
}
