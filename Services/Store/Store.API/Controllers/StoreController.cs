using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Stores.Commands.BlockStore;
using Store.Application.Stores.Commands.CreateStore;
using Store.Application.Stores.Commands.DeleteStore;
using Store.Application.Stores.Commands.UnblockStore;
using Store.Application.Stores.Commands.UpdateStore;
using Store.Application.Stores.Queries.GetManagedStore;
using Store.Application.Stores.Queries.GetStore;
using Store.Application.Stores.Queries.GetStores;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class StoreController : ApiControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(StoresViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<StoresViewModel>> Get()
        {
            return Ok(await Mediator.Send(new GetStoresQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StoreViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<StoreViewModel>> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetStoreQuery { Id = id }));
        }

        [HttpGet("Managed")]
        [Authorize(Roles = "StoreManager")]
        [ProducesResponseType(typeof(StoreViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<StoreViewModel>> GetManaged()
        {
            return Ok(await Mediator.Send(new GetManagedStoreQuery()));
        }


        [HttpPost]
        [Authorize(Roles = "StoreManager")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> Create(CreateStoreCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        [Authorize(Roles = "StoreManager")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Update(UpdateStoreCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}/block")]
        [Authorize(Roles = "ShopAdmin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> BlockShop(Guid id)
        {
            await Mediator.Send(new BlockStoreCommand(id));

            return NoContent();
        }

        [HttpPut("{id}/unblock")]
        [Authorize(Roles = "ShopAdmin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UnblockShop(Guid id)
        {
            await Mediator.Send(new UnblockStoreCommand(id));

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ShopAdmin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteStoreCommand(id));

            return NoContent();
        }
    }
}
