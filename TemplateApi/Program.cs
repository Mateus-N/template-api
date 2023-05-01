using Microsoft.EntityFrameworkCore;
using Serilog;
using TemplateApi;
using TemplateApi.Data;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration, builder.Environment);

builder.Host.UseSerilog();
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File($"logs/day-{DateTime.Now:dd-MM-yyyy}/log.txt", rollingInterval: RollingInterval.Hour)
    .CreateLogger();

startup.ConfigureServices(builder.Services);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

startup.Configure(app);

try
{
    Log.Information("Iniciando a aplicação");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "A aplicação foi encerrada inesperadamente");
    throw;
}
finally
{
    Log.CloseAndFlush();
}