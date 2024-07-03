namespace EFCore.Infrastructure.DatabaseManager.Common.Helpers;

public static class EnvironmentHelper
{

    public static bool IsDevelopmentEnvironment()
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        return string.IsNullOrEmpty(environmentName) || environmentName == "Local" || environmentName == "Development";
    }
}
