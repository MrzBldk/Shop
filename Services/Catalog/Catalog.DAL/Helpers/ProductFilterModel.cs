namespace Catalog.DAL.Helpers
{
    public class ProductFilter
    {
        public Guid[]? Brands { get; set; }
        public Guid[]? Types { get; set; }
        public Guid[]? Stores { get; set; }
    }
}
