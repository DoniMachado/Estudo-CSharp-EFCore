using EFCore.Domain.Entities;
using EFCore.Domain.Interfaces;
using EFCore.Repository.Context;

namespace EFCore.Repository.Repositories
{
    public class SecretIdentityRepository : Repository<SecretIdentity>, ISecretIdentityRepository
    {
        public SecretIdentityRepository(HeroContext context):base(context) 
        { }
        

       
    }
}
