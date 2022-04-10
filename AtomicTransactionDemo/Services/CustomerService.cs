using AtomicTransactionDemo.Core;
using AtomicTransactionDemo.Models;
using System.Text.Json;

namespace AtomicTransactionDemo.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerDbContext _customerDbContext;
    public CustomerService(ICustomerDbContext customerDbContext)
    {
        _customerDbContext = customerDbContext;
    }

    public async Task<int> AddCustomerAsync(Customer customer)
    {
        await _customerDbContext.Customers.AddAsync(customer);
        var strEvent = "Customer Created";
        AuditLogOutboxMessage auditLogOutboxMessage = new (Guid.NewGuid(),DateTime.Now,strEvent,JsonSerializer.Serialize(customer));
        await _customerDbContext.AuditLogOutboxMessages.AddAsync(auditLogOutboxMessage);
        await _customerDbContext.SaveChangesAsync();
        return customer.Id;
    }

}
