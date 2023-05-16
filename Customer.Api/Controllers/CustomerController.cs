using Customer.Api.Data;
using Customer.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerDbContext context;
        public CustomerController(CustomerDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Customers[]>>GetAllCustomers()
        {
            var customer = await context.Customer.ToArrayAsync();
            return Ok(customer);
        }

        [HttpGet("{id:guid}", Name = "GetCustomers")]
        public async Task<ActionResult<Customers>>GetCustomers(Guid id)
        {
            var customer = await context.Customer.FirstOrDefaultAsync(x => x.ID == id);
            if(customer != null)
            {
                return Ok(customer);
            }
            return NotFound("Customer not found");
        }

        [HttpPost]
        public async Task<ActionResult<Customers>> AddCustomers([FromBody] Customers customers)
        {
            customers.ID = Guid.NewGuid();
            await context.Customer.AddAsync(customers);
            await context.SaveChangesAsync();
            return CreatedAtRoute("GetCustomers", new {id = customers.ID}, customers);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Customers>> UpdateCustomers(Guid id, [FromBody] Customers customers)
        {
            var existingCustomer = await context.Customer.FirstOrDefaultAsync(x=>x.ID == id);
            if(existingCustomer != null)
            {
                existingCustomer.CustomerName = customers.CustomerName;
                existingCustomer.Geder = customers.Geder;
                existingCustomer.Address = customers.Address;
                existingCustomer.Phone = customers.Phone;
                existingCustomer.Email = customers.Email;
                existingCustomer.Date = customers.Date;
                await context.SaveChangesAsync();
                return Ok(existingCustomer);
            }
            return NotFound("Customer not found");
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Customers>> DeleteCustomers(Guid id)
        {
            var existingCustomer = await context.Customer.FirstOrDefaultAsync(x=>x.ID==id);
            if(existingCustomer != null)
            {
                context.Remove(existingCustomer);
                await context.SaveChangesAsync();
                return Ok(existingCustomer);
            }
            return NotFound("Customer not found");
        }
    }
}
