using EFCore.Domain.Common.Entities;

namespace EFCore.Domain.Core.Entities;

public class HeroBattle : Entity
{
    public long HeroId { get; private set; }
    public long BattleId { get; private set; }


    public virtual Hero Hero { get; set; }
    public virtual Battle Battle { get; set; }

    public HeroBattle(long heroId, long battleId)
    {
        HeroId = heroId;
        BattleId = battleId;
    }
}
