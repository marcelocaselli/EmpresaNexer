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
            try
            {
                var product = await context.Products.ToListAsync();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05XE06 - Falha interna no servidor");
            }
        }

        [HttpGet("v1/products/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] EmpresaNexerDataContext context)
        {
            try
            {
                var product = await context
                    .Products
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (product == null)
                    return NotFound();

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05XE05 - Falha interna no servidor");
            }
        }

        [HttpPost("v1/products")]
        public async Task<IActionResult> PostAsync(
            [FromBody] Product model,
            [FromServices] EmpresaNexerDataContext context)
        {
            try
            {
                await context.Products.AddAsync(model);
                await context.SaveChangesAsync();

                return Created($"v1/products/{model.Id}", model);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "05XE9 - Não foi possível incluir o produto");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05X10 - Falha interna no servidor");
            }


        }

        [HttpPut("v1/products/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] Product model,
            [FromServices] EmpresaNexerDataContext context)
        {
            try
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
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "05XE8 - Não foi possível alterar o cliente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05X11 - Falha interna no servidor");
            }
        }

        [HttpDelete("v1/products/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
        [FromRoute] int id,
        [FromServices] EmpresaNexerDataContext context)
        {
            try
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
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "05XE7 - Não foi possível excluir o cliente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05X12 - Falha interna no servidor");
            }
        }
    }
}