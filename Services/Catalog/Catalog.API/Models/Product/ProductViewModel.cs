namespace Catalog.API.Models.Product
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string[] PicturesUris { get; set; }
        public int AvailableStock { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }

        public Guid storeId { get; set; }
    }
}
