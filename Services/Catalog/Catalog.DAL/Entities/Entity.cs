using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.DAL.Entities
{
    public abstract class Entity
    {
        public virtual bool IsNew => Id == Guid.Empty;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }
        public DateTime Created { get; set; }

        public Entity()
        {
            Id = Guid.Empty;
        }
    }
}
