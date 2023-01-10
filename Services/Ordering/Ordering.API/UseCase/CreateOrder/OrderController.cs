using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ordering.API.Services;
using Ordering.Application.Commands.CreateOrder;
using Ordering.Domain.ValueObjects;

namespace Ordering.API.UseCase.CreateOrder
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(Roles = "ShopClient")]
    public class OrderController : ControllerBase
    {
        ICreateOrderUseCase _createOrderUseCase;
        IIdentityService _identityService;
        public OrderController(ICreateOrderUseCase createOrderUseCase, IIdentityService identityService)
        {
            _createOrderUseCase = createOrderUseCase;
            _identityService = identityService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] CreateOrderRequest createOrderRequest)
        {
            Address address = new(createOrderRequest.Street, createOrderRequest.City,
                createOrderRequest.State, createOrderRequest.Country, createOrderRequest.ZipCode);
            CreateOrderResult createOrderResult = await _createOrderUseCase.Execute(address, _identityService.UserId);
            
            return Ok(createOrderResult.Order.Id);
        }
    }
}
