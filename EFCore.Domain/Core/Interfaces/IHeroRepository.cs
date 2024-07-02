using EFCore.Domain.Common.Interfaces;
using EFCore.Domain.Common.ValueObject;
using EFCore.Domain.Core.Entities;

namespace EFCore.Domain.Core.Interfaces;

public interface IHeroRepository : IRepository<Hero>
{
    Task<Hero> GetByNameAsync(string name);

    Task<ResultPaginationVO> GetPaginatedAsync(int pageIndex = 1, int pageSize = 25, string name = null);
}
