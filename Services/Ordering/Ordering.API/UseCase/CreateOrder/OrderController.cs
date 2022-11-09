using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Commands.CreateOrder;
using Ordering.Domain.ValueObjects;

namespace Ordering.API.UseCase.CreateOrder
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        ICreateOrderUseCase _createOrderUseCase;
        public OrderController(ICreateOrderUseCase createOrderUseCase)
        {
            _createOrderUseCase = createOrderUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateOrderRequest createOrderRequest)
        {
            Address address = new(createOrderRequest.Street, createOrderRequest.City,
                createOrderRequest.State, createOrderRequest.Country, createOrderRequest.ZipCode);
            CreateOrderResult createOrderResult = await _createOrderUseCase.Execute(address);
            
            return Ok(createOrderResult.Order.Id);
        }
    }
}
