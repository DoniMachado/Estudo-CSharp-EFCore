using EFCore.Domain.Core.Entities;
using EFCore.Domain.Core.Interfaces;
using EFCore.Infrastructure.Context;

namespace EFCore.Infrastructure.Repositories
{
    public class HeroBattleRepository : Repository<HeroBattle>, IHeroBattleRepository
    {
        public HeroBattleRepository(HeroContext context):base(context) 
        { }
        

       
    }
}
