using EFCore.Domain.Entities;
using EFCore.Domain.Interfaces;
using EFCore.Repository.Context;

namespace EFCore.Repository.Repositories
{
    public class HeroBattleRepository : Repository<HeroBattle>, IHeroBattleRepository
    {
        public HeroBattleRepository(HeroContext context):base(context) 
        { }
        

       
    }
}
