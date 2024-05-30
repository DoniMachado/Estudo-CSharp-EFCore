using EFCore.Domain.Common.Enums;

namespace EFCore.Domain.Common.ValueObject;

public class AuditVO
{
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public string ModifiedBy { get; set; } = "R. Daneel Olivaw";
    public string LastAction { get; private set; } = nameof(ActionType.Insert);

    public AuditVO()
    {
        
    }

    public AuditVO(string modifiedBy)
    {
        ModifiedBy = modifiedBy;        
    }

    public void Update(string modifiedBy = "")
    {
        UpdatedAt = DateTimeOffset.UtcNow;
        LastAction = nameof(ActionType.Update);
        ModifiedBy = string.IsNullOrEmpty(modifiedBy) ? "R. Daneel Olivaw" : modifiedBy;
    }

    public void Delete(string modifiedBy = "")
    {
        DeletedAt = DateTimeOffset.UtcNow;
        LastAction = nameof(ActionType.Delete);
        ModifiedBy = string.IsNullOrEmpty(modifiedBy) ? "R. Daneel Olivaw" : modifiedBy;
    }
}
