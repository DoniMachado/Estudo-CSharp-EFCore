using EFCore.Application.Common.CommandsAndHandlers;
using EFCore.Application.Common.Enums;
using EFCore.Domain.Core.Interfaces;
using MediatR;

namespace EFCore.Application.Core.QueriesAndHandlers;

public class GetHeroByIdCommandQuery: IRequest<ResponseCommand>
{
    public long Id { get;   }

    public GetHeroByIdCommandQuery(long id)
        => Id = id;

}

public class GetHeroByIdCommandQueryHandler: IRequestHandler<GetHeroByIdCommandQuery, ResponseCommand>
{
    private readonly IHeroRepository _repository;

    public GetHeroByIdCommandQueryHandler(IHeroRepository repository)
    {
        _repository = repository;
    }


    public async Task<ResponseCommand> Handle(GetHeroByIdCommandQuery request, CancellationToken cancellationToken)
    {
        var hero = await _repository.GetByIdAsync(request.Id);

        return hero is null ? new ResponseCommand(ResponseStatusCommand.NotFound) : new ResponseCommand(hero);
    }
}
