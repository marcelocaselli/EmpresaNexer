using EmpresaNexer.Data;
using EmpresaNexer.Extensions;
using EmpresaNexer.Models;
using EmpresaNexer.ViewModels;
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
                return Ok(new ResultViewModel<List<Billing>>(billing));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<List<Billing>>("05XE06 - Falha interna no servidor"));
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
                    return NotFound(new ResultViewModel<Billing>("Billing não encontrado"));

                return Ok(new ResultViewModel<Billing>(billing));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Billing>("05XE05 - Falha interna no servidor"));
            }
        }

        [HttpPost("v1/billings")]
        public async Task<IActionResult> PostAsync(
            [FromBody] EditorBillingViewModel model,
            [FromServices] EmpresaNexerDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Billing>(ModelState.GetErrors()));

            try
            {
                var billing = new Billing
                {
                    Id = 0,
                    DataVencimento = model.DataVencimento
                };
                await context.Billings.AddAsync(billing);
                await context.SaveChangesAsync();

                return Created($"v1/billings/{billing.Id}", new ResultViewModel<Billing>(billing));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Billing>("05XE9 - Não foi possível incluir a billing"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Billing>("05X10 - Falha interna no servidor"));
            }
        }

        [HttpPut("v1/billings/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] EditorBillingViewModel model,
            [FromServices] EmpresaNexerDataContext context)
        {
            try
            {
                var billing = await context
                .Billings
                .FirstOrDefaultAsync(x => x.Id == id);

                if (billing == null)
                    return NotFound(new ResultViewModel<Billing>("Billing não encontrado"));

                billing.DataVencimento = model.DataVencimento;

                context.Billings.Update(billing);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Billing>(billing));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Billing>("05XE8 - Não foi possível alterar a billing"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Billing>("05X11 - Falha interna no servidor"));
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
                    return NotFound(new ResultViewModel<Billing>("Billing não encontrado"));

                context.Billings.Remove(billing);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Billing>(billing));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Billing>("05XE7 - Não foi possível excluir a billing"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Billing>("05X12 - Falha interna no servidor"));
            }
        }
    }
}