namespace AtomicTransactionDemo.Core;

public interface ISendMessageService
{
    Task SendMessageAsync(string data);
}
