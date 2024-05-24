using EFCore.Domain.Interfaces;
using EFCore.Infrastructure.Context;
using EFCore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EFCore.WebAPI;

public class Startup
{
    private IConfiguration _configuration;

    public Startup(IWebHostEnvironment env)
    {
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        _configuration = builder.Build();
    }

    public void ConfigureServices(WebApplicationBuilder builder)
    {
        IServiceCollection services = builder.Services;

        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddDbContext<HeroContext>(options =>
                        options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"),
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.EnableRetryOnFailure(maxRetryCount:10,maxRetryDelay:TimeSpan.FromSeconds(30),errorNumbersToAdd: null);
                            sqlOptions.CommandTimeout(180);
                        }));


        services.Scan(i =>
        i.FromCallingAssembly()
        .AddClasses(classes => classes.AssignableTo(typeof(Repository<>))
            .Where(type => type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepository<>))))
        .AsImplementedInterfaces()
        .WithScopedLifetime()
        ); 
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.Map("/", async context => { await context.Response.WriteAsync("Estudos C# Asp.Net EFCore"); });
            endpoints.MapControllers();
        });
    }
}
