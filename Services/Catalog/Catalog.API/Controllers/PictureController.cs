using Catalog.API.Models.Picture;
using Catalog.BLL.DTO;
using Catalog.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PictureController : ControllerBase
    {
        [HttpGet("{filename}")]
        [Produces("image/jpeg", "application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(string filename)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filename);
            FileStream stream;
            try
            {
                stream = new(path, FileMode.Open);
            }
            catch
            {
                return NotFound(new { message = "Image not found" });
            }
            return File(stream, "image/jpeg");
        }

        [HttpPost("{id}")]
        [Authorize(Roles = "StoreManager")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromForm] PicturesViewModel files, Guid id,
            [FromServices] IProductService productService)
        {
            ProductDTO product = await productService.GetById(id);
            if (product is null)
                return NotFound(new { message = "Product not found" });

            if (files.FilesNames.Count != files.FormFiles.Count)
                ModelState.AddModelError("count", "Count of names and files is different.");

            if (ModelState.IsValid)
            {
                for (var i = 0; i < files.FilesNames.Count; i++)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", files.FilesNames[i]);
                    using FileStream stream = new(path, FileMode.Create);
                    files.FormFiles[i].CopyTo(stream);
                }

                if (product.PicturesUris[0] == "")
                {
                    product.PicturesUris = files.FilesNames.ToArray();
                }
                else
                {
                    product.PicturesUris = product.PicturesUris.Concat(files.FilesNames).ToArray();
                }
                await productService.Save(product);

                return Ok();
            }

            return BadRequest(ModelState);
        }
    }
}
