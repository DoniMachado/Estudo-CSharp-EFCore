using System.Text.Json.Serialization;
using EFCore.Domain.Common.Entities;
using EFCore.Domain.Common.Exceptions;

namespace EFCore.Domain.Core.Entities;

public class Hero : Entity
{
    public string Name { get; private set; }
    
    public virtual SecretIdentity SecretIdentity { get; set; }
    [JsonIgnore]
    public virtual ICollection<HeroBattle> Battles { get; set; }
    [JsonIgnore]
    public virtual ICollection<Weapon> Weapons { get; set; }

    public Hero(string name)
    {
        DomainException.When(string.IsNullOrEmpty(name), "Invalid Hero. Name is required.");
        Name = name;
    }

    public void SetName(string name)
        => Name = name;
}
