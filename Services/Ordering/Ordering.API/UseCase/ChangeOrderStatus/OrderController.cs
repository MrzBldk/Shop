using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Commands.SetAwaitingValidationStatus;
using Ordering.Application.Commands.SetCancelledStatus;
using Ordering.Application.Commands.SetShippedStatus;
using Ordering.Application.Commands.SetStockConfirmedStatus;

namespace Ordering.API.UseCase.ChangeOrderStatus
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        private readonly ISetAwaitingValidationStatusUseCase _setAwaitingValidationStatus;
        private readonly ISetCancelledStatusUseCase _setCancelledStatusUseCase;
        private readonly ISetShippedStatusUseCase _setShippedStatusUseCase;
        private readonly ISetStockConfirmedStatusUseCase _setStockConfirmedStatusUseCase;

        public OrderController(ISetAwaitingValidationStatusUseCase setAwaitingValidationStatus,
            ISetCancelledStatusUseCase setCancelledStatusUseCase,
            ISetShippedStatusUseCase setShippedStatusUseCase,
            ISetStockConfirmedStatusUseCase setStockConfirmedStatusUseCase)
        {
            _setAwaitingValidationStatus = setAwaitingValidationStatus;
            _setCancelledStatusUseCase = setCancelledStatusUseCase;
            _setShippedStatusUseCase = setShippedStatusUseCase;
            _setStockConfirmedStatusUseCase = setStockConfirmedStatusUseCase;
        }

        [HttpPut("{id}/AwaitingValidation")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SetAwaitingValidationStatus(Guid id)
        {
            await _setAwaitingValidationStatus.Execute(id);
            return NoContent();
        }

        [HttpPut("{id}/Cancelled")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SetCancelledStatusUseCase(Guid id)
        {
            await _setCancelledStatusUseCase.Execute(id);
            return NoContent();
        }

        [HttpPut("{id}/Shipped")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SetShippedStatusUseCase(Guid id)
        {
            await _setShippedStatusUseCase.Execute(id);
            return NoContent();
        }

        [HttpPut("{id}/StockConfirmed")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SetStockConfirmedStatusUseCase(Guid id)
        {
            await _setStockConfirmedStatusUseCase.Execute(id);
            return NoContent();
        }
    }
}
