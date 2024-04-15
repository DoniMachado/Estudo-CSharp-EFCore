using System.Text.Json.Serialization;

namespace EFCore.Domain.Entities;

public class Hero: Entity
{   
    public string Name { get; private set; }

    public SecretIdentity SecretIdentity { get; set; }
    [JsonIgnore]
    public virtual ICollection<HeroBattle> Battles { get; set; }
    [JsonIgnore]
    public virtual ICollection<Weapon> Weapons { get; set; }

    public Hero(string name) : base()
    {
        Name = name;
    }

    public void SetName(string name)
        => Name = name;
}
