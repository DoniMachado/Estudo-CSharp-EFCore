using EFCore.Domain.Common.ValueObject;
using EFCore.Domain.Core.Entities;
using EFCore.Domain.Core.Interfaces;
using EFCore.Infrastructure.Context;
using EFCore.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Infrastructure.Repositories
{
    public class HeroRepository : Repository<Hero>, IHeroRepository
    {
        public HeroRepository(HeroContext context):base(context) 
        { }

        public async Task<Hero> GetByIdAsync(long id)
        {
            return await base.Context.WithNoLock(async () =>
            {
                var data = base.Context.Heroes
                .Include(h => h.Weapons)
                .Include(h => h.SecretIdentity)
                .Include(h => h.Battles)
                .AsQueryable();

                data = data.Where(h => h.Id == id);

                return await data.FirstOrDefaultAsync();
            });
        }

        public async Task<Hero> GetByNameAsync(string name)
        {
            return await base.Context.WithNoLock(async () =>
            {
                var data =  base.Context.Heroes
                .Include(h => h.Weapons)
                .Include(h => h.SecretIdentity)
                .Include(h => h.Battles)
                .AsQueryable();

                data = data.Where(h => h.Name.ToLower() == name.ToLower());

                return await data.FirstOrDefaultAsync();
            });
        }

        public async Task<ResultPaginationVO> GetPaginatedAsync(int pageIndex = 1, int pageSize = 25, string name = null)
        {
            return await base.Context.WithNoLock(async () =>
            {
                var data = base.Context.Heroes
                .Include(h => h.Weapons)
                .Include(h => h.SecretIdentity)
                .Include(h => h.Battles)
                .AsNoTracking()
                .AsQueryable();

                if(!string.IsNullOrEmpty(name))
                    data = data.Where(h => EF.Functions.Like(h.Name.ToLower(),$"%{name.ToLower()}%"));

                int count = data.Count();

                int pagesToSkip = pageIndex > 1 ? pageIndex - 1 : 0;

                var list = await data.Skip(pagesToSkip * pageSize)
                 .Take(pageSize).ToListAsync();

                int totalPages = GetTotalPages(count, pageSize);

                return list is null ?
               new ResultPaginationVO(0, 0, 0, 0, null) :
               new ResultPaginationVO(count, totalPages, pageIndex, pageSize, list.Select(h => new
               {
                   h.Id,
                   h.Name
               }));
            });
        }

    }
}
