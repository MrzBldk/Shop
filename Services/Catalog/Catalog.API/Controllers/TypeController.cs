using AutoMapper;
using Catalog.API.Models.Type;
using Catalog.BLL.DTO;
using Catalog.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TypeController : ControllerBase
    {
        private IMapper _mapper;
        private ITypeService _typeService;

        public TypeController(IMapper mapper, ITypeService typeService)
        {
            _mapper = mapper;
            _typeService = typeService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<TypeViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            List<TypeDTO> types = await _typeService.Get();
            return Ok(_mapper.Map<List<TypeViewModel>>(types));
        }

        [HttpPost]
        [Authorize(Roles = "StoreManager")]
        [ProducesResponseType(typeof(TypeViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateTypeViewModel type)
        {
            if (ModelState.IsValid)
            {
                var typeDTO = _mapper.Map<TypeDTO>(type);
                await _typeService.Save(typeDTO);
                var created = _mapper.Map<TypeViewModel>(await _typeService.GetLast());
                return Ok(created);
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        [Authorize(Roles = "StoreManager")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] TypeViewModel type)
        {
            if (await _typeService.GetById(type.Id) is null)
                ModelState.AddModelError("Id", "Incorrect ID");

            if (ModelState.IsValid)
            {
                var typeDTO = _mapper.Map<TypeDTO>(type);
                await _typeService.Save(typeDTO);
                return NoContent();
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ShopAdmin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (await _typeService.GetById(id) is null)
                return NotFound();

            await _typeService.Delete(id);
            return NoContent();
        }
    }
}
