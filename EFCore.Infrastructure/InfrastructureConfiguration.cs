using EFCore.Domain.Common.Interfaces;
using EFCore.Infrastructure.Context;
using EFCore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<HeroContext>(options =>
                  options
                  .UseLazyLoadingProxies()
                  .UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                  sqlServerOptionsAction: sqlOptions =>
                  {
                      sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                      sqlOptions.CommandTimeout(180);
                  })
                  .ConfigureWarnings(warning => warning.Ignore(CoreEventId.DetachedLazyLoadingWarning))                  
                  );


        services.Scan(i =>
        i.FromCallingAssembly()
        .AddClasses(classes => classes.AssignableTo(typeof(Repository<>))
            .Where(type => type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepository<>))))
        .AsImplementedInterfaces()
        .WithScopedLifetime()
        );
        return services;
    }
}
