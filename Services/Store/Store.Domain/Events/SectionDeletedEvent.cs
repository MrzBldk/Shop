using Store.Domain.Common;
using Store.Domain.Entities;

namespace Store.Domain.Events
{
    public class SectionDeletedEvent : BaseEvent
    {
        public SectionDeletedEvent(StoreSection section)
        {
            Section = section;
        }

        public StoreSection Section { get; }
    }
}
