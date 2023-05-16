using Customer.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Customer.Api.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) { }

        public DbSet<Customers> Customer { get; set; }
    }
}
