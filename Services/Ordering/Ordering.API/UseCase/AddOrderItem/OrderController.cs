using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Commands.AddOrderItem;

namespace Ordering.API.UseCase.AddOrderItem
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        private readonly IAddOrderItemUseCase _addOrderItemUseCase;
        public OrderController(IAddOrderItemUseCase addOrderItemUseCase)
        {
            _addOrderItemUseCase = addOrderItemUseCase;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put([FromRoute] Guid id, AddOrderItemRequest addOrderItemRequest)
        {
            await _addOrderItemUseCase.Execute(id, addOrderItemRequest.UnitPrice, addOrderItemRequest.Name, addOrderItemRequest.Units);
            return NoContent();
        }
    }
}
