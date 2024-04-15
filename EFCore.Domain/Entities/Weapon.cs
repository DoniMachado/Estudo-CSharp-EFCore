using System.ComponentModel;
using System.Xml.Linq;

namespace EFCore.Domain.Entities;

public class Weapon: Entity
{   
    public string Name { get; private set; }
    public string Description { get; private set; }
    public long HeroId { get; private set; }

    public Hero Hero { get; set; }

    public Weapon(string name,string description, long heroId) :base()
    {
        Name = name;
        Description = description;
        HeroId = heroId;
    }

    public void SetName(string name)
        => Name = name;

    public void SetDescription(string description)
        => Description = description;
}
