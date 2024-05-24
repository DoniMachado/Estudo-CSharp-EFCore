using EFCore.Infrastructure.Context;
using EFCore.WebAPI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Environment);

startup.ConfigureServices(builder);



try
{
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
