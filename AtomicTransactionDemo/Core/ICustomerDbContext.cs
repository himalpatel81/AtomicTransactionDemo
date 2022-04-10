using AtomicTransactionDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace AtomicTransactionDemo.Core;

public interface ICustomerDbContext
{
    DbSet<Customer> Customers { get; set; }
    DbSet<AuditLogOutboxMessage> AuditLogOutboxMessages { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
