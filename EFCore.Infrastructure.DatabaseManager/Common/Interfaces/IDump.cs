namespace EFCore.Infrastructure.DatabaseManager.Common.Interfaces;

public interface IDump
{
    int Order { get; }

    Task DumpAsync();
}
