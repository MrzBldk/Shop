using Basket.API.Models;
using Basket.DAL.Entities;
using Basket.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;

        public BasketController(IBasketRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerBasketDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<CustomerBasketDto>> GetBasketByIdAsync(string id)
        {
            CustomerBasket basket = await _repository.GetBasketAsync(id);
            basket ??= new CustomerBasket(id);
            return Ok(basket.AdaptToDto());
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerBasketDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasketAsync([FromBody] CustomerBasketDto value)
        {
            foreach (BasketItemDto item in value.Items)
            {
                if (item.Quantity < 1)
                {
                    return BadRequest("One or more of the item has invalid quantity");
                }
            }

            CustomerBasket basket = await _repository.UpdateBasketAsync(value.AdaptToCustomerBasket());
            return Ok(basket.AdaptToDto());
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteBasketByIdAsync(string id)
        {
            await _repository.DeleteBasketAsync(id);
            return NoContent();
        }
    }
}
