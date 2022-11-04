using Ordering.API.Filters;
using System.ComponentModel.DataAnnotations;

namespace Ordering.API.UseCase.AddOrderItem
{
    [ValidateModel]
    public class AddOrderItemRequest
    {
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Units { get; set; }
    }
}
