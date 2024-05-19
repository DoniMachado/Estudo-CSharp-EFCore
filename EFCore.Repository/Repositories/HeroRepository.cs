using EFCore.Domain.Entities;
using EFCore.Domain.Interfaces;
using EFCore.Repository.Context;

namespace EFCore.Repository.Repositories
{
    public class HeroRepository : Repository<Hero>, IHeroRepository
    {
        public HeroRepository(HeroContext context):base(context) 
        { }
        

       
    }
}
