using EFCore.Application;
using EFCore.Infrastructure;

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
        services.AddApplication();
        services.AddInfrastructure(_configuration);        
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
