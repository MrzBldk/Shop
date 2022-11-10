using System.Collections.Generic;
using Basket.API.Models;

namespace Basket.API.Models
{
    public partial class CustomerBasketDto
    {
        public string BuyerId { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
}