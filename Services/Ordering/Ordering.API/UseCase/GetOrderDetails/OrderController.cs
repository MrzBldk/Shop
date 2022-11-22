using Microsoft.AspNetCore.Mvc;
using Ordering.API.Model;
using Ordering.Application.Queries;
using Ordering.Application.Results;

namespace Ordering.API.UseCase.GetOrderDetails
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

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderDetailsModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            OrderResult order = await _orderQueries.GetOrder(id);

            if (order is null) return NotFound();

            List<OrderItemDetailsModel> orderItemDetailsModels = new();

            foreach (ItemResult item in order.Items)
                orderItemDetailsModels.Add(new(item.Name, item.Units, item.UnitPrice));

            OrderDetailsModel orderDetailsModel = new(order.Id, order.Address, order.OrderDate,
                order.Price, orderItemDetailsModels, order.Status);

            return Ok(orderDetailsModel);
        }
    }
}
