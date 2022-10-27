using Store.Domain.Common;
using Store.Domain.Entities;

namespace Store.Domain.Events
{
    public class SectionCreatedEvent : BaseEvent
    {
        public SectionCreatedEvent(StoreSection section)
        {
            Section = section;
        }

        public StoreSection Section { get; }
    }
}
