namespace EFCore.Domain.Common.Events;

public abstract class DomainEvent
{
    public bool IsPublished { get; set; }
    public DateTimeOffset DateOcurred { get; protected set; } = DateTimeOffset.UtcNow;

    protected DomainEvent()
    {
        DateOcurred = DateTimeOffset.Now;
    }
}
