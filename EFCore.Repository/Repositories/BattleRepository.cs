using EFCore.Domain.Core.Entities;
using EFCore.Domain.Core.Interfaces;
using EFCore.Infrastructure.Context;

namespace EFCore.Infrastructure.Repositories
{
    public class BattleRepository : Repository<Battle>, IBattleRepository
    {
        public BattleRepository(HeroContext context):base(context) 
        { }
        

       
    }
}
