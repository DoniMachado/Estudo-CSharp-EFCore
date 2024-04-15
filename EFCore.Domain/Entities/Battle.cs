using System.Text.Json.Serialization;

namespace EFCore.Domain.Entities;

public class Battle: Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime InitialDate { get; private set; }
    public DateTime? FinalDate { get; private set; }

    [JsonIgnore]
    public virtual ICollection<HeroBattle> Heroes { get; set; }

    public Battle(string name, string description, DateTime initialDate, DateTime? finalDate) : base()
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

    public void SetFinalDate(DateTime finalDate)
        => FinalDate = finalDate;
}
