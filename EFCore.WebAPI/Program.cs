using EFCore.WebAPI;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Environment);

startup.ConfigureServices(builder);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

try
{
    builder.Host.UseSerilog();

    var app = builder.Build();
    startup.Configure(app, builder.Environment);


    app.Run();
}
catch (Exception)
{


}
finally
{

}
