using EmpresaNexer.Data;
using EmpresaNexer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpresaNexer.Controllers
{
    [ApiController]
    public class BillingLineController : ControllerBase
    {
        [HttpGet("v1/billinglines")]
        public async Task<IActionResult> GetAsync(
            [FromServices] EmpresaNexerDataContext context)
        {
            try
            {
                var billingLine = await context.BillingLines.ToListAsync();
                return Ok(billingLine);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05XE06 - Falha interna no servidor");
            }
        }

        [HttpGet("v1/billinglines/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] EmpresaNexerDataContext context)
        {
            try
            {
                var billingLine = await context
                .BillingLines
                .FirstOrDefaultAsync(x => x.Id == id);

                if (billingLine == null)
                    return NotFound();

                return Ok(billingLine);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05XE05 - Falha interna no servidor");
            }
        }

        [HttpPost("v1/billinglines")]
        public async Task<IActionResult> PostAsync(
            [FromBody] BillingLine model,
            [FromServices] EmpresaNexerDataContext context)
        {
            try
            {
                await context.BillingLines.AddAsync(model);
                await context.SaveChangesAsync();

                return Created($"v1/billinglines/{model.Id}", model);
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

        [HttpPut("v1/billinglines/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] BillingLine model,
            [FromServices] EmpresaNexerDataContext context)
        {
            try
            {
                var billingline = await context
                .BillingLines
                .FirstOrDefaultAsync(x => x.Id == id);

                if (billingline == null)
                    return NotFound();

                billingline.Description = model.Description;

                context.BillingLines.Update(billingline);
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

        [HttpDelete("v1/billinglines/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
        [FromRoute] int id,
        [FromServices] EmpresaNexerDataContext context)
        {
            try
            {
                var billingLine = await context
                    .BillingLines
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (billingLine == null)
                    return NotFound();

                context.BillingLines.Remove(billingLine);
                await context.SaveChangesAsync();

                return Ok(billingLine);
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