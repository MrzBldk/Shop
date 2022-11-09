using Ordering.API.Filters;
using System.ComponentModel.DataAnnotations;

namespace Ordering.API.UseCase.CreateOrder
{
    [ValidateModel]
    public class CreateOrderRequest
    {
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }
    }
}
