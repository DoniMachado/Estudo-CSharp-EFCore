using EFCore.Domain.Common.Events;
using MediatR;

namespace EFCore.Application.Common.EventHandlers;

public class DomainEventNotification<TDomainEvent>(TDomainEvent domainEvent): INotification where TDomainEvent: DomainEvent
{
    public TDomainEvent DomainEvent { get; } = domainEvent;
}
