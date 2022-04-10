namespace AtomicTransactionDemo.Models;

public record AuditLogOutboxMessage(Guid id, DateTime time, string type, string data)
{
    public void MarkAsProcessed(DateTime processingTime)
    {
        IsProcessed = true;
        ProcessingTime = processingTime;
    }

    public bool IsProcessed { get; private set; }
    public DateTime? ProcessingTime { get; private set; }
}
