namespace Catalog.BLL.DTO
{
    public class BrandDTO: EntityDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public List<TypeDTO> Types { get; set; }

    }
}
