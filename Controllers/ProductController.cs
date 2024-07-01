using EmpresaNexer.Data;
using EmpresaNexer.Extensions;
using EmpresaNexer.Models;
using EmpresaNexer.ViewModels;
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
                return Ok(new ResultViewModel<List<Product>>(product));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<List<Product>>("05XE06 - Falha interna no servidor"));
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
                    return NotFound(new ResultViewModel<Product>("Produto não encontrado"));

                return Ok(new ResultViewModel<Product>(product));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Product>("05XE05 - Falha interna no servidor"));
            }
        }

        [HttpPost("v1/products")]
        public async Task<IActionResult> PostAsync(
            [FromBody] EditorProductViewModel model,
            [FromServices] EmpresaNexerDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Product>(ModelState.GetErrors()));

            try
            {
                var product = new Product
                {
                    Id = 0,
                    NameProduct = model.NameProduct
                };

                await context.Products.AddAsync(product);
                await context.SaveChangesAsync();

                return Created($"v1/products/{product.Id}", new ResultViewModel<Product>(product));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Product>("05XE9 - Não foi possível incluir o produto"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Product>("05X10 - Falha interna no servidor"));
            }


        }

        [HttpPut("v1/products/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] EditorProductViewModel model,
            [FromServices] EmpresaNexerDataContext context)
        {
            try
            {
                var product = await context
                .Products
                .FirstOrDefaultAsync(x => x.Id == id);

                if (product == null)
                    return NotFound(new ResultViewModel<Product>("Produto não encontrado"));

                product.NameProduct = model.NameProduct;

                context.Products.Update(product);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Product>(product));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Product>("05XE8 - Não foi possível alterar o cliente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Product>("05X11 - Falha interna no servidor"));
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
                    return NotFound(new ResultViewModel<Product>("Produto não encontrado"));

                context.Products.Remove(product);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Product>(product));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Product>("05XE7 - Não foi possível excluir o cliente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Product>("05X12 - Falha interna no servidor"));
            }
        }
    }
}