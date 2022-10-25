namespace Catalog.DAL.Entities
{
    public class Brand : Entity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<Product> Products { get; set; }

    }
}
