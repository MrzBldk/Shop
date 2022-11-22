using Microsoft.AspNetCore.Mvc;
using Store.Application.Stores.Commands.CreateStore;
using Store.Application.Stores.Commands.DeleteStore;
using Store.Application.Stores.Commands.UpdateStore;
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

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> Create(CreateStoreCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Update(UpdateStoreCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteStoreCommand(id));

            return NoContent();
        }
    }
}
