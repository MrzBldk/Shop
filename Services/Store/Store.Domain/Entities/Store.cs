using Store.Domain.Common;

namespace Store.Domain.Entities
{
    public class Store : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public List<StoreSection> Sections { get; set; } = new();
    }
}
