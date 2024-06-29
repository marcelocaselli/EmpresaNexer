using EmpresaNexer.Data;
using EmpresaNexer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpresaNexer.Controllers
{
    [ApiController]
    public class BillingController : ControllerBase
    {
        [HttpGet("v1/billings")]
        public async Task<IActionResult> GetAsync(
            [FromServices] EmpresaNexerDataContext context)
        {
            try
            {
                var billing = await context.Billings.ToListAsync();
                return Ok(billing);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05XE06 - Falha interna no servidor");
            }
        }

        [HttpGet("v1/billings/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] EmpresaNexerDataContext context)
        {
            try
            {
                var billing = await context
                .Billings
                .FirstOrDefaultAsync(x => x.Id == id);

                if (billing == null)
                    return NotFound();

                return Ok(billing);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05XE05 - Falha interna no servidor");
            }
        }

        [HttpPost("v1/billings")]
        public async Task<IActionResult> PostAsync(
            [FromBody] Billing model,
            [FromServices] EmpresaNexerDataContext context)
        {
            try
            {
                await context.Billings.AddAsync(model);
                await context.SaveChangesAsync();

                return Created($"v1/billings/{model.Id}", model);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "05XE9 - Não foi possível incluir o cliente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05X10 - Falha interna no servidor");
            }
        }

        [HttpPut("v1/billings/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] Billing model,
            [FromServices] EmpresaNexerDataContext context)
        {
            try
            {
                var billing = await context
                .Billings
                .FirstOrDefaultAsync(x => x.Id == id);

                if (billing == null)
                    return NotFound();

                billing.DataVencimento = model.DataVencimento;

                context.Billings.Update(billing);
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

        [HttpDelete("v1/billings/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
        [FromRoute] int id,
        [FromServices] EmpresaNexerDataContext context)
        {
            try
            {
                var billing = await context
                .Billings
                .FirstOrDefaultAsync(x => x.Id == id);

                if (billing == null)
                    return NotFound();

                context.Billings.Remove(billing);
                await context.SaveChangesAsync();

                return Ok(billing);
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