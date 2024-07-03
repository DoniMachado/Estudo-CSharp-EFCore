using EFCore.Application.Common.CommandsAndHandlers;
using EFCore.Application.Common.Enums;
using EFCore.Domain.Common.Entities;
using EFCore.Infrastructure.DatabaseManager.Common.Interfaces;
using MediatR;

namespace EFCore.Infrastructure.DatabaseManager.Common.Dump;

public abstract class Dump<TEntity,TQuery,TCommand>: IDump
    where TEntity : Entity
    where TQuery : IRequest<ResponseCommand>
    where TCommand : IRequest<ResponseCommand>
{
    protected readonly IMediator Mediator;
    protected readonly int _order;

    public int Order => _order; 

    protected Dump(IMediator mediator, string dumpName,int order = 0)
    {
        _order = order;
        Mediator = mediator;

        Console.WriteLine("\t" + dumpName);
    }


    protected virtual async Task<bool> CanSave(TQuery query)
    {
        try
        {
            var result = await Mediator.Send(query);

            return result is null || result.Status == ResponseStatusCommand.NotFound;
            
        }catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    protected virtual async Task SaveAsync(TQuery query,TCommand command)
    {
        if(await CanSave(query))
        {
            var result = await Mediator.Send(command);

            if(result.Status != ResponseStatusCommand.OK)
            {
                foreach(var error in result.Errors)
                {
                    PrinError(string.Format("{0} - {1}",error.Key,error.Value));    
                }
            }

        }
    }

    public virtual Task DumpAsync()
        => Task.FromResult(true);

    protected virtual void PrinError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ForegroundColor = ConsoleColor.Green;
    }


}
