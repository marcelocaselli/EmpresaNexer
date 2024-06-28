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
            var billingLine = await context.BillingLines.ToListAsync();
            return Ok(billingLine);
        }

        [HttpGet("v1/billinglines/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] EmpresaNexerDataContext context)
        {
            var billingLine = await context
                .BillingLines
                .FirstOrDefaultAsync(x => x.Id == id);

            if (billingLine == null)
                return NotFound();

            return Ok(billingLine);
        }

        [HttpPost("v1/billinglines")]
        public async Task<IActionResult> PostAsync(
            [FromBody] BillingLine model,
            [FromServices] EmpresaNexerDataContext context)
        {
            await context.BillingLines.AddAsync(model);
            await context.SaveChangesAsync();

            return Created($"v1/billinglines/{model.Id}", model);
        }

        [HttpDelete("v1/billinglines/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
        [FromRoute] int id,
        [FromServices] EmpresaNexerDataContext context)
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
    }
}