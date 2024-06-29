using EmpresaNexer.Data;
using EmpresaNexer.Models;
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
            try
            {
                var customers = await context.Customers.ToListAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05XE06 - Falha interna no servidor");
            }
        }

        [HttpGet("v1/customers/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] EmpresaNexerDataContext context)
        {
            try
            {
                var customer = await context
                    .Customers
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (customer == null)
                    return NotFound();

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05XE05 - Falha interna no servidor");
            }
        }

        [HttpPost("v1/customers")]
        public async Task<IActionResult> PostAsync(
            [FromBody] Customer model,
            [FromServices] EmpresaNexerDataContext context)
        {
            try
            {
                await context.Customers.AddAsync(model);
                await context.SaveChangesAsync();

                return Created($"v1/customers/{model.Id}", model);
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

        [HttpPut("v1/customers/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] Customer model,
            [FromServices] EmpresaNexerDataContext context)
        {
            try
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
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "05XE8 - Não foi possível alterar o cliente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05X11 - Falha interna no servidor");
            }
        }

        [HttpDelete("v1/customers/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
        [FromRoute] int id,
        [FromServices] EmpresaNexerDataContext context)
        {
            try
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
