using EFCore.Application.Common.CommandsAndHandlers;
using EFCore.Application.Common.Enums;
using EFCore.Application.Core.QueriesAndHandlers;
using EFCore.Domain.Core.Entities;
using EFCore.Domain.Core.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EFCore.Application.Core.CommandsAndHandlers;

public class RemoveHeroCommand : IRequest<ResponseCommand>
{
    public long Id { get; }

    public RemoveHeroCommand(long id)
    {
        Id = id;
    }
}


public class RemoveHeroCommandHandler : IRequestHandler<RemoveHeroCommand, ResponseCommand>
{
    private readonly IHeroRepository _repository;

    public RemoveHeroCommandHandler(IHeroRepository repository)
    {
        _repository = repository;
    }


    public async Task<ResponseCommand> Handle(RemoveHeroCommand request, CancellationToken cancellationToken)
    {
        Hero hero = await _repository.GetByIdAsync(request.Id);

        if(hero is null)
        {
            var response = new ResponseCommand(ResponseStatusCommand.NotFound);
            response.AddError("NotFound", $"Hero {request.Id} not found.");

            return response;
        }
        
        await _repository.DeleteAsync(hero);

        return new ResponseCommand(ResponseStatusCommand.OK);
    }
}

public class RemoveHeroCommandValidator : AbstractValidator<RemoveHeroCommand>
{
    public RemoveHeroCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Field Id is required.")
            .NotEmpty().WithMessage("Field Id is required.");
    }

}
