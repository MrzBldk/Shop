using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Commands.AddOrderItem;

namespace Ordering.API.UseCase.AddOrderItem
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(Roles = "ShopClient")]
    public class OrderController : ControllerBase
    {
        private readonly IAddOrderItemUseCase _addOrderItemUseCase;
        public OrderController(IAddOrderItemUseCase addOrderItemUseCase)
        {
            _addOrderItemUseCase = addOrderItemUseCase;
        }

        [HttpPost("{id}/AddItem")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put([FromRoute] Guid id, AddOrderItemRequest addOrderItemRequest)
        {
            await _addOrderItemUseCase.Execute(id, addOrderItemRequest.UnitPrice, addOrderItemRequest.Name, addOrderItemRequest.Units);
            return NoContent();
        }

        [HttpPost("{id}/AddMultiple")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put([FromRoute] Guid id, List<AddOrderItemRequest> addOrderItemRequest)
        {
            foreach(var item in addOrderItemRequest)
            {
                await _addOrderItemUseCase.Execute(id, item.UnitPrice, item.Name, item.Units);
            }
            return NoContent();
        }
    }
}
