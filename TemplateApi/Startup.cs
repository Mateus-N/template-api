using Microsoft.EntityFrameworkCore;
using TemplateApi.Data;
using TemplateApi.Services;

namespace TemplateApi;

public class Startup
{
    public ConfigurationManager Configuration { get; set; }
    public IWebHostEnvironment Env { get; set; }

    public Startup(ConfigurationManager configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        Env = env;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        Configuration
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{Env.EnvironmentName}.json");

        string connectionString = Configuration
            .GetConnectionString("TemplateConnection")!;

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySQL(connectionString);
        });

        services.AddScoped<ItemService>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app)
    {
        if (Env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
