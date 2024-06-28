using EmpresaNexer.Data;
using EmpresaNexer.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpresaNexer.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet("v1/customers")]
        public async Task<IActionResult> GetAsync(
            [FromServices] EmpresaNexerDataContext context)
        {
            var customers = await context.Customers.ToListAsync();
            return Ok(customers);
        }

        [HttpGet("v1/customers/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] EmpresaNexerDataContext context)
        {
            var customer = await context
                .Customers
                .FirstOrDefaultAsync(x => x.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost("v1/customers")]
        public async Task<IActionResult> PostAsync(
            [FromBody] Customer model,
            [FromServices] EmpresaNexerDataContext context)
        {
            await context.Customers.AddAsync(model);
            await context.SaveChangesAsync();

            return Created($"v1/customers/{model.Id}", model);
        }

        [HttpPut("v1/customers/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] Customer model,
            [FromServices] EmpresaNexerDataContext context)
        {
            var customer = await context
                .Customers
                .FirstOrDefaultAsync(x => x.Id == id);

            if (customer == null)
                return NotFound();

            customer.Name = model.Name;
            customer.Email = model.Email;
            customer.Address = model.Address;

            context.Customers.Update(customer);
            await context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete("v1/customers/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
        [FromRoute] int id,
        [FromServices] EmpresaNexerDataContext context)
        {
            var customer = await context
                .Customers
                .FirstOrDefaultAsync(x => x.Id == id);

            if (customer == null)
                return NotFound();

            context.Customers.Remove(customer);
            await context.SaveChangesAsync();

            return Ok(customer);
        }
    }
}