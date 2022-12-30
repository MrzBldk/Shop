using Aggregator.Models;
using Aggregator.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {

        private readonly ICatalogService _catalog;
        private readonly IBasketService _basket;

        public BasketController(ICatalogService catalogService, IBasketService basketService)
        {
            _catalog = catalogService;
            _basket = basketService;
        }

        [ProducesResponseType(typeof(BasketData), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BasketData>> UpdateQuantitiesAsync([FromBody] UpdateBasketItemsRequest data)
        {
            if (!data.Updates.Any())
                return BadRequest("No updates sent");

            var currentBasket = await _basket.GetByIdAsync(data.BasketId);
            if (currentBasket is null)
                return BadRequest($"Basket with id {data.BasketId} not found.");

            foreach (var update in data.Updates)
            {
                var basketItem = currentBasket.Items.SingleOrDefault(bitem => bitem.Id == update.BasketItemId);
                if (basketItem is null)
                {
                    return BadRequest($"Basket item with id {update.BasketItemId} not found");
                }
                basketItem.Quantity = update.NewQuantity;
            }

            await _basket.UpdateAsync(currentBasket);

            return Ok(currentBasket);
        }

        [ProducesResponseType(typeof(BasketData), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddBasketItemAsync([FromBody] AddBasketItemRequest data)
        {
            if (data is null || data.Quantity == 0)
                return BadRequest("Invalid payload");

            CatalogItem item = await _catalog.GetCatalogItemAsync(data.CatalogItemId);

            BasketData currentBasket = (await _basket.GetByIdAsync(data.BasketId)) ?? new(data.BasketId);
            currentBasket.Items.Add(new BasketDataItem()
            {
                UnitPrice = item.Price,
                PictureUrl = item.PicturesUris.FirstOrDefault(),
                ProductId = item.Id.ToString(),
                ProductName = item.Name,
                Quantity = data.Quantity,
                Id = Guid.NewGuid().ToString()
            });

            await _basket.UpdateAsync(currentBasket);

            return Ok(currentBasket);
        }
    }
}
