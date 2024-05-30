using EFCore.Domain.Common.Entities;

namespace EFCore.Domain.Core.Entities;

public class SecretIdentity : Entity
{
    public string Name { get; private set; }
    public long HeroId { get; private set; }

    public Hero Hero { get; set; }

    public SecretIdentity(string name, long heroId) : base()
    {
        Name = name;
        HeroId = heroId;
    }

    public void SetName(string name)
       => Name = name;
}
