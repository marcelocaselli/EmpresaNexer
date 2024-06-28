using EmpresaNexer.Data;
using EmpresaNexer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpresaNexer.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("v1/products")]
        public async Task<IActionResult> GetAsync(
            [FromServices] EmpresaNexerDataContext context)
        {
            var product = await context.Products.ToListAsync();
            return Ok(product);
        }

        [HttpGet("v1/products/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] EmpresaNexerDataContext context)
        {
            var product = await context
                .Products
                .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost("v1/products")]
        public async Task<IActionResult> PostAsync(
            [FromBody] Product model,
            [FromServices] EmpresaNexerDataContext context)
        {
            await context.Products.AddAsync(model);
            await context.SaveChangesAsync();

            return Created($"v1/products/{model.Id}", model);
        }

        [HttpPut("v1/products/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] Product model,
            [FromServices] EmpresaNexerDataContext context)
        {
            var product = await context
                .Products
                .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
                return NotFound();

            product.NameProduct = model.NameProduct;

            context.Products.Update(product);
            await context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete("v1/products/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
        [FromRoute] int id,
        [FromServices] EmpresaNexerDataContext context)
        {
            var product = await context
                .Products
                .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
                return NotFound();

            context.Products.Remove(product);
            await context.SaveChangesAsync();

            return Ok(product);
        }
    }
}