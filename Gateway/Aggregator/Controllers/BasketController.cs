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

        [HttpPut]
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

                CatalogItem item = await _catalog.GetCatalogItemAsync(update.BasketItemId);
                if(update.NewQuantity > item.AvailableStock)
                {
                    return BadRequest("Available stock is lesser than requested quantity");
                }

                basketItem.Quantity = update.NewQuantity;
            }

            await _basket.UpdateAsync(currentBasket);

            return Ok(currentBasket);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BasketData), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddBasketItemAsync([FromBody] AddBasketItemRequest data)
        {
            if (data is null || data.Quantity == 0)
                return BadRequest("Invalid payload");

            CatalogItem item = await _catalog.GetCatalogItemAsync(data.CatalogItemId);

            if (item.AvailableStock < data.Quantity)
                return BadRequest("Available stock is lesser than requested quantity");

            if (item.IsArchived)
                return BadRequest("Item is archived");

            BasketData currentBasket = (await _basket.GetByIdAsync(data.BasketId)) ?? new(data.BasketId);

            if (currentBasket.Items.Any(item => item.ProductId == data.CatalogItemId))
                return BadRequest("Item is already added to basket");

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
