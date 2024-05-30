using EFCore.Domain.Common.Events;
using EFCore.Domain.Common.Interfaces;
using MediatR;

namespace EFCore.Application.Common.EventHandlers;

public class DomainEventHandler(IPublisher mediator): IDomainEventHandler
{
    readonly IPublisher _mediator = mediator;

    public async Task Publish(DomainEvent domainEvent)
    {
        await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
    }

    private INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent)
        => (INotification)Activator.CreateInstance(typeof(DomainNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent);
}
