using Store.Domain.Common;
using Store.Domain.Enums;

namespace Store.Domain.Entities
{
    public class StoreSection : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public PriorityLevel Priority { get; set; }


        public Guid StoreId { get; set; }
    }
}
