using DbUp;
using DbUp.ScriptProviders;
using EFCore.Infrastructure.DatabaseManager.Common.Consts;
using EFCore.Infrastructure.DatabaseManager.Common.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;

namespace EFCore.Infrastructure.DatabaseManager.Scripts.SQL;

public class ScriptRunner
{
    private readonly IConfiguration _configuration;
    private static readonly Regex AppRootPathMatcher = new(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");

    public ScriptRunner(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Run(bool dropDatabase)
    {
        if (dropDatabase)
            DropDatabase(_configuration.GetConnectionString("DefaultConnection"));


        if (EnvironmentHelper.IsDevelopmentEnvironment())
            EnsureDatabaseBeforeOrAfter("Scripts/SQL/01_runBefore");

        RunScripts("Scripts/SQL/02_tables/");
        RunScripts("Scripts/SQL/03_indexes/");
        RunScripts("Scripts/SQL/04_functions/");
        RunScripts("Scripts/SQL/05_triggers/");
        RunScripts("Scripts/SQL/06_procedures/");
        EnsureDatabaseBeforeOrAfter("Scripts/SQL/07_runAfter");
    }

    private void RunScripts(string scriptPath)
    {
        try
        {
            var execPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var rootPath = AppRootPathMatcher.Match(execPath).Value;
            var scriptFullPath = Path.Combine(rootPath, scriptPath);

            if (!Directory.Exists(scriptFullPath))
                scriptFullPath = Path.Combine(execPath, scriptPath);

            if (!Directory.Exists(scriptFullPath))
                throw new DirectoryNotFoundException($"Path path not found: {scriptFullPath}");


            var upgraderEngine =
               DeployChanges.To
                   .SqlDatabase(_configuration.GetConnectionString("DefaultConnection"))
                   .WithScriptsFromFileSystem(
                    scriptPath, new FileSystemScriptOptions
                    {
                        IncludeSubDirectories = true
                    }
                   )
                   .WithTransactionPerScript()
                   .WithVariablesDisabled()
                   .Build();

            var upgrader = upgraderEngine.PerformUpgrade();

            if (!upgrader.Successful)
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(upgrader.Error);
                Console.ResetColor();
                Console.ReadLine();
            }
            else
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Sucess!");
                Console.ResetColor();
            }
        }
        catch (Exception ex)
        {
            string innerException = ex.InnerException is not null ? ex.InnerException.Message : "[]";
            ConsoleDraw.DrawException($"{ex.Message} - {innerException} - {ex.StackTrace.ToString()}");
            throw;
        }

    }
    private void EnsureDatabaseBeforeOrAfter(string scriptPath)
    {
        try
        {
            var execPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var rootPath = AppRootPathMatcher.Match(execPath).Value;
            var scriptFullPath = Path.Combine(rootPath, scriptPath);

            if (!Directory.Exists(scriptFullPath))
                scriptFullPath = Path.Combine(execPath, scriptPath);

            if (!Directory.Exists(scriptFullPath))
                throw new DirectoryNotFoundException($"Path path not found: {scriptFullPath}");

            string[] sqlFiles = Directory.GetFiles(scriptFullPath, "*.sql");

            using var connection = new SqlConnection(_configuration.GetConnectionString("ConnSqlManager"));
            connection.Open();

            foreach (var file in sqlFiles)
            {
                var script = File.ReadAllText(file);
                var commandTexts = script.Split(new[] { "\r\nGO\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var commandTxt in commandTexts)
                {
                    using var command = new SqlCommand(commandTxt, connection);
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            string innerException = ex.InnerException is not null ? ex.InnerException.Message : "[]";
            ConsoleDraw.DrawException($"{ex.Message} - {innerException} - {ex.StackTrace.ToString()}");
            throw;
        }
    }

    private void DropDatabase(string connectionString)
    {
        try
        {
            IDbConnection bBConnection = new SqlConnection(connectionString);
            var dataBaseName = bBConnection.Database;

            using var connection = new SqlConnection(_configuration.GetConnectionString("ConnSqlManager"));
            connection.Open();

            using var command = new SqlCommand($"IF (SELECT db_id('{dataBaseName}')) IS NOT NULL BEGIN ALTER DATABASE [{dataBaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE END", connection);
            command.ExecuteNonQuery();

            using var command2 = new SqlCommand($"IF (SELECT db_id('{dataBaseName}')) IS NOT NULL BEGIN DROP DATABASE [{dataBaseName}] END", connection);
            command2.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            string innerException = ex.InnerException is not null ? ex.InnerException.Message : "[]";
            ConsoleDraw.DrawException($"{ex.Message} - {innerException} - {ex.StackTrace.ToString()}");
            throw;
        }
    }
}
