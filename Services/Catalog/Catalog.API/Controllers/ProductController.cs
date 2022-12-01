using AutoMapper;
using Catalog.API.Models.Product;
using Catalog.BLL.DTO;
using Catalog.BLL.Services.Interfaces;
using Catalog.DAL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ProductViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] int skip, [FromQuery] int take, [FromQuery] ProductFilter filter)
        {
            List<ProductDTO> products;
            if (skip == 0 && take == 0)
                products = await _productService.Get(filter);
            else
                products = await _productService.Get(filter, skip, take);
            return Ok(_mapper.Map<List<ProductViewModel>>(products));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            ProductDTO product = await _productService.GetById(id);
            if (product is null) return NotFound(new { message = "Product not found" });
            return Ok(_mapper.Map<ProductViewModel>(product));
        }

        [HttpPost]
        [Authorize(Roles = "StoreManager")]
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromServices] IBrandService brandService, [FromServices] ITypeService typeService,
            [FromBody] CreateProductViewModel product)
        {
            if (await brandService.GetById(product.BrandId) is null)
                ModelState.AddModelError("BrandId", "Incorrect brand ID");
            if (await typeService.GetById(product.TypeId) is null)
                ModelState.AddModelError("TypeId", "Incorrect type ID");

            if (ModelState.IsValid)
            {
                var productDTO = _mapper.Map<ProductDTO>(product);
                await _productService.Save(productDTO);
                var created = _mapper.Map<ProductViewModel>(await _productService.GetLast());
                return Created($"api/products/{created.Id}", created);
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        [Authorize(Roles = "StoreManager")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromServices] IBrandService brandService, [FromServices] ITypeService typeService,
            [FromBody] UpdateProductViewModel product)
        {
            if (await _productService.GetById(product.Id) is null)
                ModelState.AddModelError("Id", "Incorrect ID");
            if (await brandService.GetById(product.BrandId) is null)
                ModelState.AddModelError("BrandId", "Incorrect brand ID");
            if (await typeService.GetById(product.TypeId) is null)
                ModelState.AddModelError("TypeId", "Incorrect type ID");

            if (ModelState.IsValid)
            {
                var productDTO = _mapper.Map<ProductDTO>(product);
                await _productService.Save(productDTO);
                return NoContent();
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ShopAdmin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            if (await _productService.GetById(Id) is null)
                return NotFound();

            await _productService.Delete(Id);
            return NoContent();
        }
    }
}
