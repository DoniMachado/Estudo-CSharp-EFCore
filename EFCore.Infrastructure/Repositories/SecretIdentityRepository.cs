using EFCore.Domain.Core.Entities;
using EFCore.Domain.Core.Interfaces;
using EFCore.Infrastructure.Context;

namespace EFCore.Infrastructure.Repositories
{
    public class SecretIdentityRepository : Repository<SecretIdentity>, ISecretIdentityRepository
    {
        public SecretIdentityRepository(HeroContext context):base(context) 
        { }
        

       
    }
}
