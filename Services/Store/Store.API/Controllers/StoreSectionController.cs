﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.StoreSections.Commands.CreateStoreSection;
using Store.Application.StoreSections.Commands.DeleteStoreSection;
using Store.Application.StoreSections.Commands.UpdateStoreSection;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class StoreSectionController : ApiControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "StoreManager")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> Create(CreateStoreSectionCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        [Authorize(Roles = "StoreManager")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Update(UpdateStoreSectionCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "StoreManager")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteStoreSectionCommand(id));

            return NoContent();
        }
    }
}
