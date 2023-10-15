namespace Catalog.BLL.DTO
{
    public class TypeDTO : EntityDTO
    {
        public string Name { get; set; }
        public List<TypeDTO> Types { get; set; }
    }
}
