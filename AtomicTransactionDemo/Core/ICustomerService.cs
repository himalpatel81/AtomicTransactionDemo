using AtomicTransactionDemo.Models;

namespace AtomicTransactionDemo.Core;

public interface ICustomerService
{
    Task<int> AddCustomerAsync(Customer customer);
}
