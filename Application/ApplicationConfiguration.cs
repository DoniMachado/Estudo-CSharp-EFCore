using EFCore.Application.Common.EventHandlers;
using EFCore.Domain.Common.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EFCore.Application;

public static class ApplicationConfiguration
{

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddScoped<IDomainEventHandler, DomainEventHandler>();
        return services;
    }
}
