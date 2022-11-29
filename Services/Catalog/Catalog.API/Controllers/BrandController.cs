using AutoMapper;
using Catalog.API.Models.Brand;
using Catalog.BLL.DTO;
using Catalog.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BrandController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBrandService _brandService;

        public BrandController(IMapper mapper, IBrandService productService)
        {
            _mapper = mapper;
            _brandService = productService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<BrandViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            List<BrandDTO> brands = await _brandService.Get();
            return Ok(_mapper.Map<List<BrandViewModel>>(brands));
        }

        [HttpPost]
        [Authorize(Roles = "StoreManager")]
        [ProducesResponseType(typeof(BrandViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateBrandViewModel brand)
        {
            if (ModelState.IsValid)
            {
                var brandDTO = _mapper.Map<BrandDTO>(brand);
                await _brandService.Save(brandDTO);
                var created = _mapper.Map<BrandViewModel>(await _brandService.GetLast());
                return Ok(created);
            }
            
            return BadRequest(ModelState);
        }

        [HttpPut]
        [Authorize(Roles = "StoreManager")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] BrandViewModel brand)
        {
            if (await _brandService.GetById(brand.Id) is null)
                ModelState.AddModelError("Id", "Incorrect ID");
            
            if (ModelState.IsValid)
            {
                var brandDTO = _mapper.Map<BrandDTO>(brand);
                await _brandService.Save(brandDTO);
                return NoContent();
            }
            
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ShopModerator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (await _brandService.GetById(id) is null)
                return NotFound();

            await _brandService.Delete(id);
            return NoContent();
        }
    }
}
