using AtomicTransactionDemo.Core;
using AtomicTransactionDemo.Models;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System.Text.Json;

namespace AtomicTransactionDemo.Jobs;

[DisallowConcurrentExecution]
public class MonitorCustomerCreateJob : IJob
{
    private readonly ILogger<MonitorCustomerCreateJob> _logger;
    private readonly ICustomerDbContext _customerDbContext;
    private readonly ISendMessageService _sendMessageService;
    public MonitorCustomerCreateJob(ILogger<MonitorCustomerCreateJob> logger, ICustomerDbContext customerDbContext, ISendMessageService sendMessageService)
    {
        _logger = logger;
        _customerDbContext = customerDbContext;
        _sendMessageService = sendMessageService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Check messages!");
        var messages = await _customerDbContext.AuditLogOutboxMessages.
            Where(c => (c.type == Constants.CUSTOMER_CREATED_EVENT) && c.IsProcessed == false)
            .OrderBy(c => c.data).ToListAsync();
        foreach (var message in messages)
        {
            try
            {
                var data = JsonSerializer.Deserialize<Customer>(message.data);
                //Only for demo
                await _sendMessageService.SendMessageAsync(message.data);
            
                message.MarkAsProcessed(DateTime.Now);
                await _customerDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred whle processing the message {message}. {ex}");
            }
        }
    }
}
