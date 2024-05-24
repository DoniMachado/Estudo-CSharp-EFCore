using EFCore.Domain.Entities;
using EFCore.Domain.Interfaces;
using EFCore.Infrastructure.Context;

namespace EFCore.Infrastructure.Repositories
{
    public class WeaponRepository : Repository<Weapon>, IWeaponRepository
    {
        public WeaponRepository(HeroContext context):base(context) 
        { }
        

       
    }
}
