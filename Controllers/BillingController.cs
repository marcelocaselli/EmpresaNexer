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
            var billing = await context.Billings.ToListAsync();
            return Ok(billing);
        }

        [HttpGet("v1/billings/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] EmpresaNexerDataContext context)
        {
            var billing = await context
                .Billings
                .FirstOrDefaultAsync(x => x.Id == id);

            if (billing == null)
                return NotFound();

            return Ok(billing);
        }

        [HttpPost("v1/billings")]
        public async Task<IActionResult> PostAsync(
            [FromBody] Billing model,
            [FromServices] EmpresaNexerDataContext context)
        {
            await context.Billings.AddAsync(model);
            await context.SaveChangesAsync();

            return Created($"v1/billings/{model.Id}", model);
        }

        [HttpPut("v1/billings/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] Billing model,
            [FromServices] EmpresaNexerDataContext context)
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

        [HttpDelete("v1/billings/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
        [FromRoute] int id,
        [FromServices] EmpresaNexerDataContext context)
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
    }
}