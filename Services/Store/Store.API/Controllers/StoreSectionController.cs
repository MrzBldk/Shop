using Microsoft.AspNetCore.Mvc;
using Store.Application.StoreSections.Commands.CreateStoreSection;
using Store.Application.StoreSections.Commands.DeleteStoreSection;
using Store.Application.StoreSections.Commands.UpdateStoreSection;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreSectionController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateStoreSectionCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateStoreSectionCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteStoreSectionCommand(id));

            return NoContent();
        }
    }
}
