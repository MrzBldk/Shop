using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Models.Type
{
    public class CreateTypeViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
