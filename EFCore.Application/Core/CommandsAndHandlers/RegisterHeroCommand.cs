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
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EFCore.Application.Core.CommandsAndHandlers;

public class RegisterHeroCommand: IRequest<ResponseCommand>
{
    public string Name { get; }

    public RegisterHeroCommand(string name)
       => Name = name;
}


public class RegisterHeroCommandHandler : IRequestHandler<RegisterHeroCommand, ResponseCommand>
{
    private readonly IHeroRepository _repository;

    public RegisterHeroCommandHandler(IHeroRepository repository)
    {
        _repository = repository;
    }


    public async Task<ResponseCommand> Handle(RegisterHeroCommand request, CancellationToken cancellationToken)
    {
        Hero hero = await _repository.GetByNameAsync(request.Name);

        if (hero is not null)
        {
            var response = new ResponseCommand(ResponseStatusCommand.NotAllowed);
            response.AddError("Already Exists Hero", $"Already Exists Hero with Name: {request.Name}");
            return response;
        }


        hero = new(request.Name);
            
        await _repository.InsertAsync(hero);

        return new ResponseCommand(ResponseStatusCommand.OK);
    }
}

public class RegisterHeroCommandValidator: AbstractValidator<RegisterHeroCommand>
{
    public RegisterHeroCommandValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(256).WithMessage("Field Name must have between 1 and 256 characters.")
            .NotEmpty().WithMessage("Field Name is required.");
    }

}
