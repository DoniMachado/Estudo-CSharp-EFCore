using EFCore.Domain.Common.Events;

namespace EFCore.Domain.Common.Interfaces;

public interface IHasDomainEvent
{
    HashSet<DomainEvent> DomainEvents { get; protected set; }
}

