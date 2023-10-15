namespace Aggregator.Models
{
    public class CatalogItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string[] PicturesUris { get; set; }
        public int AvailableStock { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public bool IsArchived { get; set; }

        public Guid StoreId { get; set; }
    }
}
