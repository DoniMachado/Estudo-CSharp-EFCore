using EFCore.Infrastructure.DatabaseManager.Common;
using EFCore.Infrastructure.DatabaseManager.Common.Consts;
using EFCore.Infrastructure.DatabaseManager.Common.Enum;


const string LOCAL = "Local";

async Task MenuEnvironment(string[] args = null)
{
    if (args is not null && args.Length == 1)
        args = null;

    EnvironmentDumpManager environmentChoice;

    if(args is null)
    {
        ConsoleDraw.DrawMenuEnvironments();
        bool isNumeric = int.TryParse(Console.ReadLine().ToString(), out int option);

        if(!isNumeric)
        {
            ConsoleDraw.DrawInvalidOption();
            await MenuEnvironment();
            return;
        }

        if (option is 0)
            return;

        EnvironmentDumpManager GetEnvironment(int opt)
        {
            return opt switch
            {
                1 => EnvironmentDumpManager.Development,
               // 2 => EnvironmentDumpManager.Staging,
                //3 => EnvironmentDumpManager.Production,
                _ => EnvironmentDumpManager.Development
            };
        }

        EnvironmentDumpManager envOption = GetEnvironment(option);

        if (envOption is EnvironmentDumpManager.Development)
            await ActionMenu(envOption);
        else
            await ClientMenu(envOption);

    }
    else
    {
        if(args.Length <  2)
        {
            ConsoleDraw.DrawFeedback("Invalid Arguments");
            return;
        }

        EnvironmentDumpManager GetEnvironment(string option){
            return option switch
            {
                "development" => EnvironmentDumpManager.Development,
                //"staging" => EnvironmentDumpManager.Staging,
                //"production" => EnvironmentDumpManager.Production,
                 _ => EnvironmentDumpManager.Development
            } ;
        }

        EnvironmentDumpAction GetAction(string action)
        {
            return action switch
            {
                "dump" => EnvironmentDumpAction.Dump,
                "update" => EnvironmentDumpAction.Update,
                "reset" => EnvironmentDumpAction.Reset,
                _ => EnvironmentDumpAction.None
            };
        }

        environmentChoice = GetEnvironment(args[0]);

        EnvironmentDumpAction actionChoice = GetAction(args[1]);

        string clientName = args.Length > 2 ? args[2]: string.Empty;

        if (actionChoice is EnvironmentDumpAction.None)
            return;

        if(environmentChoice is EnvironmentDumpManager.Development)
        {
            await ActionMenu(environmentChoice, actionChoice, clientName);
        }
        else
        {
            await ClientMenu(environmentChoice, actionChoice);
        }

    }

}


async Task ActionMenu(EnvironmentDumpManager environmentChoice, EnvironmentDumpAction? actionChoice = null, string clientName = "")
{
    if (string.IsNullOrEmpty(clientName))
        clientName = LOCAL;

    if (actionChoice is not null && actionChoice == EnvironmentDumpAction.None)
        return;

    if(actionChoice is null)
    {
        ConsoleDraw.DrawMenuAction(environmentChoice.ToString());
        bool isNumeric = int.TryParse(Console.ReadLine().ToString(), out int actionChoiceInt);

        if(!isNumeric)
        {
            ConsoleDraw.DrawInvalidOption();
            await ActionMenu(environmentChoice);
            return;
        }

        if(actionChoiceInt is 0)
        {
            await MenuEnvironment();
            return;
        }

        EnvironmentDumpAction GetAction(int action)
        {
            return action switch
            {
                1 => EnvironmentDumpAction.Update,
                2 => EnvironmentDumpAction.Dump,                
                3 => EnvironmentDumpAction.Reset,
                _ => EnvironmentDumpAction.None
            };
        }

        actionChoice = GetAction(actionChoiceInt);

    }

    await Run(environmentChoice, actionChoice.Value, clientName);
}

async Task ClientMenu(EnvironmentDumpManager environmentChoice, EnvironmentDumpAction? actionChoice = null)
{
    string clientName = LOCAL;

    if(environmentChoice is not EnvironmentDumpManager.Development)
    {
        ConsoleDraw.DrawInputClient(environmentChoice.ToString());
        clientName = Console.ReadLine().ToString();
    }

    await ActionMenu(environmentChoice, actionChoice, clientName);
}


async Task Run(EnvironmentDumpManager environmentChoice, EnvironmentDumpAction actionChoice, string clientName = LOCAL)
{
    try
    {
        await new Startup(environmentChoice, actionChoice, clientName).RunAsync();
    }
    catch (Exception ex)
    {
        string innerException = ex.InnerException is not null ? ex.InnerException.Message: "[]";
        ConsoleDraw.DrawException($"{ex.Message} - {innerException} - {ex.StackTrace.ToString()}");
    }
}

await MenuEnvironment(Environment.GetCommandLineArgs());
ConsoleDraw.DrawPoweredBy();
