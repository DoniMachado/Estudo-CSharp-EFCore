using EFCore.Domain.Entities;
using EFCore.Domain.Enum;
using EFCore.Domain.ValueObject;
using EFCore.Repository.Context;
using EFCore.Repository.Extensions;
using EFCore.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace EFCore.Repository.Repositories
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
            if(entity is null)
                throw new ArgumentNullException(nameof(entity));

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
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            entity.OnEntityModified();
            Entity.Update(entity);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            if (entities is null || entities.Any())
                throw new ArgumentNullException(nameof(entities));

            foreach(TEntity entity in entities)
                entity.OnEntityModified();

            Entity.UpdateRange(entities);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            entity.DeleteEntity();
            Entity.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            if (entities is null || entities.Any())
                throw new ArgumentNullException(nameof(entities));

            foreach (TEntity entity in entities)
                entity.DeleteEntity();

            Entity.RemoveRange(entities);
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
