using EFCore.Application.Common.CommandsAndHandlers;
using EFCore.Application.Common.Enums;
using EFCore.Domain.Core.Interfaces;
using MediatR;

namespace EFCore.Application.Core.QueriesAndHandlers;

public class GetHeroByNameCommandQuery : IRequest<ResponseCommand>
{
    public string Name { get;   }

    public GetHeroByNameCommandQuery(string name)
        => Name = name;

}

public class GetHeroByNameCommandQueryHandler : IRequestHandler<GetHeroByNameCommandQuery, ResponseCommand>
{
    private readonly IHeroRepository _repository;

    public GetHeroByNameCommandQueryHandler(IHeroRepository repository)
    {
        _repository = repository;
    }


    public async Task<ResponseCommand> Handle(GetHeroByNameCommandQuery request, CancellationToken cancellationToken)
    {
        var hero = await _repository.GetByNameAsync(request.Name);

        return hero is null ? new ResponseCommand(ResponseStatusCommand.NotFound) : new ResponseCommand(hero);
    }
}
