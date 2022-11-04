using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Commands.AddOrderItem;

namespace Ordering.API.UseCase.AddOrderItem
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IAddOrderItemUseCase _addOrderItemUseCase;
        public OrderController(IAddOrderItemUseCase addOrderItemUseCase)
        {
            _addOrderItemUseCase = addOrderItemUseCase;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, AddOrderItemRequest addOrderItemRequest)
        {
            await _addOrderItemUseCase.Execute(id, addOrderItemRequest.UnitPrice, addOrderItemRequest.Name, addOrderItemRequest.Units);
            return NoContent();
        }
    }
}
