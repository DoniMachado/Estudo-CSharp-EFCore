using EFCore.Domain.Enum;

namespace EFCore.Domain.Entities;

public class Entity
{
    public long Id { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset ModifiedAt { get; private set; }
    public DateTimeOffset DeletedAt { get; private set; }
    public EntityStatus EntityStatus { get; private set; }

    public Entity()
    {
        CreatedAt = DateTimeOffset.UtcNow;
        ModifiedAt = DateTimeOffset.UtcNow;
        EntityStatus = EntityStatus.Active;
    }

    public void ArchiveEntity()
    {
        EntityStatus = EntityStatus.Archived;
    }

    public void DeleteEntity()
    {
        EntityStatus = EntityStatus.Deleted;
        DeletedAt = DateTimeOffset.UtcNow;
    }

    public void OnEntityModified()
    {
        ModifiedAt = DateTimeOffset.UtcNow;
    }
}
