using EFCore.Domain.Core.Entities;
using EFCore.Domain.Core.Interfaces;
using EFCore.Infrastructure.Context;

namespace EFCore.Infrastructure.Repositories
{
    public class WeaponRepository : Repository<Weapon>, IWeaponRepository
    {
        public WeaponRepository(HeroContext context):base(context) 
        { }
        

       
    }
}
