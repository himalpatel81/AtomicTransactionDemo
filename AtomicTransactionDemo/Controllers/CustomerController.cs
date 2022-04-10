using AtomicTransactionDemo.Core;
using AtomicTransactionDemo.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AtomicTransactionDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/<AtomicController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        
        // POST api/<AtomicController>
        [HttpPost]
        public async Task Post([FromBody] CustomerRequest customerRequest)
        {
            Customer customer = new (0,customerRequest.Name);  
            await _customerService.AddCustomerAsync(customer);

        }

        
    }
}
