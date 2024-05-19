using EFCore.Domain.Entities;
using EFCore.Domain.Interfaces;
using EFCore.Repository.Context;

namespace EFCore.Repository.Repositories
{
    public class BattleRepository : Repository<Battle>, IBattleRepository
    {
        public BattleRepository(HeroContext context):base(context) 
        { }
        

       
    }
}
