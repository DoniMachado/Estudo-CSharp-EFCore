using EFCore.Domain.Entities;
using EFCore.Domain.Interfaces;
using EFCore.Infrastructure.Context;

namespace EFCore.Infrastructure.Repositories
{
    public class SecretIdentityRepository : Repository<SecretIdentity>, ISecretIdentityRepository
    {
        public SecretIdentityRepository(HeroContext context):base(context) 
        { }
        

       
    }
}
