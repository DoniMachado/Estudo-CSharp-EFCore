using EFCore.Domain.Common.Events;
using MediatR;

namespace EFCore.Application.Common.EventHandlers;

public class DomainNotification<TDomainEvent>(TDomainEvent domainEvent): INotification where TDomainEvent: DomainEvent
{
    public TDomainEvent DomainEvent { get; } = domainEvent;
}
