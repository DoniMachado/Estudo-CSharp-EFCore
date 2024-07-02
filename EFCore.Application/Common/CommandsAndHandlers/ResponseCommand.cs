using EFCore.Application.Common.Enums;

namespace EFCore.Application.Common.CommandsAndHandlers;

public class ResponseCommand
{
    public ResponseStatusCommand Status { get; } = ResponseStatusCommand.OK;

    public object Result { get; set; }

    public Dictionary<string, string> Errors { get; } = new();

    public bool IsSuccessful => Status == ResponseStatusCommand.OK;


    public ResponseCommand(object result)
    {
        Result = result;
    }

    public ResponseCommand(object result, ResponseStatusCommand status)
    {
        Result = result;
        Status = status;
    }

    public ResponseCommand()
    {
        
    }

    public void AddError(string code, string messsage)
    {
        if(!Errors.ContainsKey(code)) 
            Errors.Add(code,messsage);
    }

    public TEntity ConvertTo<TEntity>()
        => (TEntity)Result;

    public void SetResult(object result)
        => Result = result;
}
