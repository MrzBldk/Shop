namespace Catalog.BLL.DTO
{
    public abstract class EntityDTO
    {
        public virtual bool IsNew => Id == Guid.Empty;
        public virtual Guid Id { get; set; }

    }
}
