using EFCore.Application.Common.CommandsAndHandlers;
using EFCore.Domain.Core.Interfaces;
using MediatR;

namespace EFCore.Application.Core.QueriesAndHandlers;

public class GetHeroPaginatedCommandQuery : IRequest<ResponseCommand>
{
    public int PageIndex { get;   }
    public int PageSize { get;   }
    public string Name { get;   }

    public GetHeroPaginatedCommandQuery(int pageIndex = 1, int pageSize = 25, string name = null)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        Name = name;
    }

}

public class GetHeroPaginatedCommandQueryHandler : IRequestHandler<GetHeroPaginatedCommandQuery, ResponseCommand>
{
    private readonly IHeroRepository _repository;

    public GetHeroPaginatedCommandQueryHandler(IHeroRepository repository)
    {
        _repository = repository;
    }


    public async Task<ResponseCommand> Handle(GetHeroPaginatedCommandQuery request, CancellationToken cancellationToken)
    {
        var heroes = await _repository.GetPaginatedAsync(request.PageIndex, request.PageSize, request.Name);

        return new ResponseCommand(heroes);
    }
}
