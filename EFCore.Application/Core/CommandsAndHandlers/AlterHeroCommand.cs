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

public class AlterHeroCommand : IRequest<ResponseCommand>
{
    public long Id { get; }
    public string Name { get; }

    public AlterHeroCommand(long id ,string name)
    {
        Id = id;
        Name = name;
    }
}


public class AlterHeroCommandHandler : IRequestHandler<AlterHeroCommand, ResponseCommand>
{
    private readonly IHeroRepository _repository;

    public AlterHeroCommandHandler(IHeroRepository repository)
    {
        _repository = repository;
    }


    public async Task<ResponseCommand> Handle(AlterHeroCommand request, CancellationToken cancellationToken)
    {
        Hero hero = await _repository.GetByIdAsync(request.Id);

        if(hero is null)
        {
            var response = new ResponseCommand(ResponseStatusCommand.NotFound);
            response.AddError("NotFound", $"Hero {request.Id} not found.");

            return response;
        }

        bool goToDataBase = false;

        if (!string.Equals(hero.Name, request.Name))
        {
            hero.SetName(request.Name);
            goToDataBase = true;
        }    
          
        if (goToDataBase)
            await _repository.UpdateAsync(hero);

        return new ResponseCommand(ResponseStatusCommand.OK);
    }
}

public class AlterHeroCommandValidator : AbstractValidator<AlterHeroCommand>
{
    public AlterHeroCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Field Id is required.")
            .NotEmpty().WithMessage("Field Id is required.");

        RuleFor(x => x.Name)
            .MaximumLength(256).WithMessage("Field Name must have between 1 and 256 characters.")
            .NotEmpty().WithMessage("Field Name is required.");
    }

}
