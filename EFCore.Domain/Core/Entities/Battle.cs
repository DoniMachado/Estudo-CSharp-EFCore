using System.Text.Json.Serialization;
using EFCore.Domain.Common.Entities;

namespace EFCore.Domain.Core.Entities;

public class Battle : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTimeOffset InitialDate { get; private set; }
    public DateTimeOffset? FinalDate { get; private set; }

    [JsonIgnore]
    public virtual ICollection<HeroBattle> Heroes { get; set; }

    public Battle(string name, string description, DateTimeOffset initialDate, DateTimeOffset? finalDate)
    {
        Name = name;
        Description = description;
        InitialDate = initialDate;
        FinalDate = finalDate;
    }

    public void SetName(string name)
        => Name = name;

    public void SetDescription(string description)
        => Description = description;

    public void SetFinalDate(DateTimeOffset finalDate)
        => FinalDate = finalDate;
}
