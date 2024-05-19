using EFCore.Domain.Entities;
using EFCore.Domain.Interfaces;
using EFCore.Repository.Context;

namespace EFCore.Repository.Repositories
{
    public class WeaponRepository : Repository<Weapon>, IWeaponRepository
    {
        public WeaponRepository(HeroContext context):base(context) 
        { }
        

       
    }
}
