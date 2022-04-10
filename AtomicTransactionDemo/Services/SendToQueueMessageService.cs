using AtomicTransactionDemo.Core;

namespace AtomicTransactionDemo.Services;

public class SendToQueueMessageService : ISendMessageService
{
    private readonly ILogger<SendToQueueMessageService> _logger;
    public SendToQueueMessageService(ILogger<SendToQueueMessageService> logger)
    {
        _logger = logger;
    }
    public async Task SendMessageAsync(string data)
    {
        //Send the message to the Azure Queue.
        _logger.LogInformation(data);
    }
}
