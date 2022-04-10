using AtomicTransactionDemo.Core;
using AtomicTransactionDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace AtomicTransactionDemo;

public class CustomerDbContext : DbContext, ICustomerDbContext
{
    public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerDbContext).Assembly);
        modelBuilder.Entity<Customer>().HasData(
            new Customer(1,"Himal")
        );
        base.OnModelCreating(modelBuilder);
    }

    
    public DbSet<AuditLogOutboxMessage> AuditLogOutboxMessages { get; set; }
    public DbSet<Customer> Customers { get; set; }

    
}
