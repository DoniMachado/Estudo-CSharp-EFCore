using EFCore.Domain.Common.Events;
using EFCore.Domain.Common.Helpers;
using EFCore.Domain.Common.Interfaces;
using EFCore.Domain.Common.ValueObject;
using System.Text.Json.Serialization;

namespace EFCore.Domain.Common.Entities;

public class Entity: IHasDomainEvent
{
    public long Id { get; }
    public bool IsDeleted { get; private set; }

    [JsonIgnore]

    public AuditVO Audit { get; private set; } = new();

    [JsonIgnore]

    public HashSet<DomainEvent> DomainEvents { get; set; } = new();

    public Entity()
    {
        Id = SnowFlakeIdGeneratorHelper.CreateId();
    }

    
    public virtual void SetCreator(string createdBy = "R. Daneel Olivaw")
        => Audit = new AuditVO(createdBy);

    public virtual void SetUpdate(string updatedBy = "R. Daneel Olivaw")
        => Audit.Update(updatedBy);

    public virtual void SetDeleted(string deletedBy = "R. Daneel Olivaw")
    {
        Audit.Delete(deletedBy);
        IsDeleted = true;
    }
}
