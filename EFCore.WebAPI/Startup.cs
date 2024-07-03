using EFCore.Application;
using EFCore.Infrastructure;
using EFCore.WebAPI.Common.Middlewares;
using Microsoft.AspNetCore.RateLimiting;
using Serilog;
using System.Threading.RateLimiting;

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

        builder.Services.AddRateLimiter(_ => _
            .AddFixedWindowLimiter(policyName: "fixed", options =>
            {
                options.PermitLimit = 4;
                options.Window = TimeSpan.FromSeconds(12);
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.QueueLimit = 2;
            }));

        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddApplication();
        services.AddInfrastructure(_configuration); 
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRateLimiter();

        //Add support to logging request with SERILOG
        app.UseSerilogRequestLogging();

        app.Use((context, next) =>
        {
            context.Request.EnableBuffering();
            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Remove("Server");
                context.Response.Headers.Remove("X-Powered-By");
                context.Response.Headers.Remove("X-SourceFiles");

                return Task.CompletedTask;
            });

            return next();
        });


        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
       
        app.UseRouting();
        app.UseAuthorization();

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.Map("/", async context => { await context.Response.WriteAsync("Estudos C# Asp.Net EFCore"); });
            endpoints.MapControllers();
        });
    }
}
