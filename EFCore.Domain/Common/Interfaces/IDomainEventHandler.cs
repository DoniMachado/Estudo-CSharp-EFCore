using EFCore.Domain.Common.Events;

namespace EFCore.Domain.Common.Interfaces;

public interface IDomainEventHandler
{
    Task Publish(DomainEvent domainEvent);
}
