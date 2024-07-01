using EmpresaNexer.Data;
using EmpresaNexer.Extensions;
using EmpresaNexer.Models;
using EmpresaNexer.ViewModels;
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
                return Ok(new ResultViewModel<List<BillingLine>>(billingLine));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<List<BillingLine>>("05XE06 - Falha interna no servidor"));
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
                    return NotFound(new ResultViewModel<BillingLine>("BillingLine não encontrado"));

                return Ok(new ResultViewModel<BillingLine>(billingLine));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<BillingLine>("05XE05 - Falha interna no servidor"));
            }
        }

        [HttpPost("v1/billinglines")]
        public async Task<IActionResult> PostAsync(
            [FromBody] EditorBillingLineViewModel model,
            [FromServices] EmpresaNexerDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<BillingLine>(ModelState.GetErrors()));

            try
            {
                var billingline = new BillingLine
                {
                    Id = 0,
                    Description = model.Description
                };

                await context.BillingLines.AddAsync(billingline);
                await context.SaveChangesAsync();

                return Created($"v1/billinglines/{billingline.Id}", new ResultViewModel<BillingLine>(billingline));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<BillingLine>("05XE9 - Não foi possível incluir a billingline"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<BillingLine>("05X10 - Falha interna no servidor"));
            }
        }

        [HttpPut("v1/billinglines/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] EditorBillingLineViewModel model,
            [FromServices] EmpresaNexerDataContext context)
        {
            try
            {
                var billingline = await context
                .BillingLines
                .FirstOrDefaultAsync(x => x.Id == id);

                if (billingline == null)
                    return NotFound(new ResultViewModel<BillingLine>("BillingLine não encontrado"));

                billingline.Description = model.Description;

                context.BillingLines.Update(billingline);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<BillingLine>(billingline));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<BillingLine>("05XE8 - Não foi possível alterar a billingline"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<BillingLine>("05X11 - Falha interna no servidor"));
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
                    return NotFound(new ResultViewModel<BillingLine>("BillingLine não encontrado"));

                context.BillingLines.Remove(billingLine);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<BillingLine>(billingLine));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<BillingLine>("05XE7 - Não foi possível excluir a billingline"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<BillingLine>("05X12 - Falha interna no servidor"));
            }
        }
    }
}