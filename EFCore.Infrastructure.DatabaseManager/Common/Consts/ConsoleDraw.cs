using EFCore.Infrastructure.DatabaseManager.Common.Enum;

namespace EFCore.Infrastructure.DatabaseManager.Common.Consts;

public static class ConsoleDraw
{
    public const string LineHead = "####################################################################";
    public const string LineFooter = "############################################################### v 2024.0";
    public const string PoweredBy = "Luiz Felipe";
    public const string Image = @"
                                   Estudo:
                                        Entity FrameWork Core, 
                                        Mediator, 
                                        FluentValidation,
                                        E outros
                                 ";

    public const string Environment = "----------------------------- Enviroments -----------------------------";
    public const string Options = "----------------------------- Options -----------------------------";
    public const string Line = "--------------------------------------------------------------";


    public static void DrawHeader()
    {
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.WriteLine(LineHead);
        Console.WriteLine(Image);
        Console.WriteLine(LineFooter);
        Console.ResetColor();
        Console.WriteLine("\n");
    }


    public static void DrawMenuEnvironments()
    {
        DrawHeader();
        Console.WriteLine(Environment);
        Console.WriteLine("\t 0 - Exit");
        Console.WriteLine("\t 1 - Development");
        //Console.WriteLine("\t 2 - Staging");
        //Console.WriteLine("\t 3 - Production");
        Console.WriteLine("\t =>");
    }

    public static void DrawMenuAction(string environment)
    {
        DrawHeader();
        Console.WriteLine(Options);
        Console.WriteLine($"\t Environment {environment}");
        Console.WriteLine("-------------");
        Console.WriteLine("\t 0 - Back");
        Console.WriteLine("\t 1 - Update database and run dump");
        Console.WriteLine("\t 2 - Run dump only");

        if (environment == EnvironmentDumpManager.Development.ToString())
            Console.WriteLine("\t 3 - Reset");

        Console.WriteLine("\t =>");
    }

    public static void DrawInvalidOption()
    {
        DrawHeader();
        Console.ResetColor();
        Console.WriteLine("\n");
        Console.BackgroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid option, please choose again");
        Console.ReadKey();
        Console.ResetColor();
    }
    public static void DrawException(string message)
    {
        Console.WriteLine(Line);
        Console.ResetColor();
        Console.WriteLine("\n");
        Console.BackgroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.WriteLine("\n");
        Console.WriteLine("Error {•_•}");
    }

    public static void DrawEnvironment(string environment = "local")
    {
        Console.WriteLine(Line);
        Console.WriteLine($"\t Environment {environment}");
    }

    public static void DrawFeedback(string message)
    {
        Console.WriteLine(Line);
        Console.WriteLine(message);
    }

    public static void DrawPoweredBy()
    {
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n");
        Console.WriteLine($"Powered By Luiz Felipe {DateTime.Now.Year}");
    }

    public static void DrawInputClient(string environment = "local")
    {
        Console.WriteLine(Line);
        Console.WriteLine($"\t Environment {environment}");
        Console.WriteLine("Please insert the client name");
        Console.WriteLine("\t =>");
    }
}
