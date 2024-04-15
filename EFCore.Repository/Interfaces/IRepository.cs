using EFCore.Domain.Entities;
using EFCore.Domain.ValueObject;

namespace EFCore.Repository.Interfaces;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task InsertAsync(TEntity entity);
    Task InsertAsync(IEnumerable<TEntity> entities);
    Task UpdateAsync(TEntity entity);
    Task UpdateAsync(IEnumerable<TEntity> entities);
    Task DeleteAsync(TEntity entity);
    Task DeleteAsync(IEnumerable<TEntity> entities);
    Task<TEntity> GetByIdAsync(long id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<ResultPaginationVO> GetPaginatedAsync(int pageIndex = 1, int pageSize = 25);
}
