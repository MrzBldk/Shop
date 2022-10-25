namespace Catalog.DAL.Entities
{
    public class Type : Entity
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
