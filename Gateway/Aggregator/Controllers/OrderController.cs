using Aggregator.Models;
using Aggregator.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly IOrderApiClient _orderClient;

        public OrderController(IBasketService basketService, IOrderApiClient orderClient)
        {
            _basketService = basketService;
            _orderClient = orderClient;
        }

        [HttpPost("Create/{basketId}")]
        public async Task<ActionResult> CreateOrderFromBasket([FromRoute] string basketId, [FromBody] CreateOrderRequest orderData)
        {
            BasketData basket = await _basketService.GetByIdAsync(basketId);

            if (basket is null)
            {
                return BadRequest($"No basket found for id {basketId}");
            }

            string orderId = await _orderClient.CreateOrder(orderData);

            await _orderClient.AddOrderItems(basket, orderId);

            return Ok(orderId);
        }
    }
}
