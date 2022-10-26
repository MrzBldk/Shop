namespace Catalog.BLL.DTO
{
    public class ProductDTO : EntityDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string[] PicturesUris { get; set; }
        public int AvailableStock { get; set; }
        public bool IsArchived { get; set; }

        public Guid BrandId { get; set; }
        public BrandDTO Brand {get; set;}

        public Guid TypeId { get; set; }
        public TypeDTO Type { get; set; }

        public Guid StoreId { get; set; }
    }
}
