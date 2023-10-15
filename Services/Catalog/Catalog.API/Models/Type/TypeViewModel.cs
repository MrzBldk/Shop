using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Models.Type
{
    public class TypeViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
