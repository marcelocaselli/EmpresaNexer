using Microsoft.EntityFrameworkCore;
using EmpresaNexer.Data.Mappings;
using EmpresaNexer.Models;

namespace EmpresaNexer.Data
{
    public class TextNexerDataContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Billing> Billings { get; set; }
        public DbSet<BillingLine> BillingLines { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=localhost,1433; Database=TestNexer;User ID = sa; Password=1q2w3e4r@#$;TrustServerCertificate=true");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new BillingMap());
            modelBuilder.ApplyConfiguration(new BillingLineMap());
        }
    }
}