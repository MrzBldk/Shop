using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ordering.API.Model;
using Ordering.Application.Queries;
using Ordering.Application.Results;

namespace Ordering.API.UseCase.GetOrders
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderQueries _orderQueries;
        public OrderController(IOrderQueries orderQueries)
        {
            _orderQueries = orderQueries;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<OrderDetailsModel>), StatusCodes.Status200OK)]
        [Authorize(Roles = "ShopAdmin")]
        public async Task<IActionResult> Get()
        {
            List<OrderResult> orders = await _orderQueries.GetOrders();

            List<OrderDetailsModel> results = new();

            foreach (var order in orders)
            {
                List<OrderItemDetailsModel> orderItemDetailsModels = new();

                foreach (ItemResult item in order.Items)
                    orderItemDetailsModels.Add(new(item.Name, item.Units, item.UnitPrice, item.ProductId));

                OrderDetailsModel orderDetailsModel = new(order.Id, order.Address, order.OrderDate,
                    order.Price, orderItemDetailsModels, order.Status, order.UserId);

                results.Add(orderDetailsModel);
            }

            return Ok(results);
        }

        [HttpGet("ByUser/{userId}")]
        [ProducesResponseType(typeof(List<OrderDetailsModel>), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> GetByUser(Guid userId)
        {
            List<OrderResult> orders = await _orderQueries.GetOrdersByUser(userId);

            List<OrderDetailsModel> results = new();

            foreach (var order in orders)
            {
                List<OrderItemDetailsModel> orderItemDetailsModels = new();

                foreach (ItemResult item in order.Items)
                    orderItemDetailsModels.Add(new(item.Name, item.Units, item.UnitPrice, item.ProductId));

                OrderDetailsModel orderDetailsModel = new(order.Id, order.Address, order.OrderDate,
                    order.Price, orderItemDetailsModels, order.Status, order.UserId);

                results.Add(orderDetailsModel);
            }

            return Ok(results);
        }
    }
}
