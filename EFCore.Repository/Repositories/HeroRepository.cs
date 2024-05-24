using EFCore.Domain.Entities;
using EFCore.Domain.Interfaces;
using EFCore.Infrastructure.Context;

namespace EFCore.Infrastructure.Repositories
{
    public class HeroRepository : Repository<Hero>, IHeroRepository
    {
        public HeroRepository(HeroContext context):base(context) 
        { }
        

       
    }
}
