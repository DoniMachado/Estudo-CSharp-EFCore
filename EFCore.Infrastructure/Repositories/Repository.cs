using EFCore.Domain.Common.Entities;
using EFCore.Domain.Common.Interfaces;
using EFCore.Domain.Common.ValueObject;
using EFCore.Infrastructure.Context;
using EFCore.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly HeroContext Context;
        private readonly DbSet<TEntity> Entity;

        public Repository(HeroContext context)
        {
            Context = context;
            Entity = context.Set<TEntity>();
        }

        public async Task InsertAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            Entity.Add(entity);
            await Context.SaveChangesAsync();
        }

        public async Task InsertAsync(IEnumerable<TEntity> entities)
        {
            if (entities is null || entities.Any())
                throw new ArgumentNullException(nameof(entities));

            Entity.AddRange(entities);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            entity.SetUpdate();
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            if (entities is null || entities.Any())
                throw new ArgumentNullException(nameof(entities));

            foreach(TEntity entity in entities)
                entity.SetUpdate();

            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            entity.SetDeleted();
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(IEnumerable<TEntity> entities)
        {            
            if (entities is null || entities.Any())
                throw new ArgumentNullException(nameof(entities));

            foreach (TEntity entity in entities)
                entity.SetDeleted();
            
            await Context.SaveChangesAsync();
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await Context.WithNoLock(async () =>
            {
                return await Entity.FirstOrDefaultAsync(e => e.Id == id);
            } );
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.WithNoLock(async () =>
            {
                return await Entity.AsNoTracking().AsQueryable().ToListAsync();
            });
        }

        public async Task<ResultPaginationVO> GetPaginatedAsync(int pageIndex = 1, int pageSize = 25)
        {
            return await Context.WithNoLock(async () =>
            {
                var data = Entity.AsNoTracking().AsQueryable();

                int count = data.Count();

                int pagesToSkip = pageIndex > 1 ? pageIndex - 1: 0;

               var list = await data.Skip(pagesToSkip * pageSize)
                .Take(pageSize).ToListAsync();

                int totalPages = GetTotalPages(count, pageSize);

                return list is null?
                new ResultPaginationVO(0,0,0,0,null):
                new ResultPaginationVO(count, totalPages,pageIndex,pageSize, list);
            });
        }

        protected int GetTotalPages(int count, int pageSize)
            => (int)Math.Ceiling((double)count / pageSize);
    }
}
