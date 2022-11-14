using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Commands.AddOrderItem;
using Ordering.Application.Commands.SetAwaitingValidationStatus;
using Ordering.Application.Commands.SetCancelledStatus;
using Ordering.Application.Commands.SetShippedStatus;
using Ordering.Application.Commands.SetStockConfirmedStatus;

namespace Ordering.API.UseCase.ChangeOrderStatus
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
