using EmpresaNexer.Data;
using EmpresaNexer.Extensions;
using EmpresaNexer.Models;
using EmpresaNexer.ViewModels;
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
                return Ok(new ResultViewModel<List<Customer>>(customers));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<List<Customer>>("05XE06 - Falha interna no servidor"));
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
                    return NotFound(new ResultViewModel<Customer>("Cliente não encontrado"));

                return Ok(new ResultViewModel<Customer>(customer));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Customer>("05XE05 - Falha interna no servidor"));
            }
        }

        [HttpPost("v1/customers")]
        public async Task<IActionResult> PostAsync(
            [FromBody] EditorCustomerViewModel model,
            [FromServices] EmpresaNexerDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Customer>(ModelState.GetErrors()));

            try
            {
                var customer = new Customer
                {
                    Id = 0,
                    Name = model.Name,
                    Email = model.Email,
                    Address = model.Address
                };
                await context.Customers.AddAsync(customer);
                await context.SaveChangesAsync();

                return Created($"v1/customers/{customer.Id}", new ResultViewModel<Customer>(customer));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Customer>("05XE9 - Não foi possível incluir o cliente"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Customer>("05X10 - Falha interna no servidor"));
            }
        }

        [HttpPut("v1/customers/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] EditorCustomerViewModel model,
            [FromServices] EmpresaNexerDataContext context)
        {
            try
            {
                var customer = await context
                .Customers
                .FirstOrDefaultAsync(x => x.Id == id);

                if (customer == null)
                    return NotFound(new ResultViewModel<Customer>("Cliente não encontrado"));

                customer.Name = model.Name;
                customer.Email = model.Email;
                customer.Address = model.Address;

                context.Customers.Update(customer);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Customer>(customer));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Customer>("05XE8 - Não foi possível alterar o cliente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Customer>("05X11 - Falha interna no servidor"));
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
                    return NotFound(new ResultViewModel<Customer>("Cliente não encontrado"));

                context.Customers.Remove(customer);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Customer>(customer));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Customer>("05XE7 - Não foi possível excluir o cliente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Customer>("05X12 - Falha interna no servidor"));
            }
        }
    }
}
