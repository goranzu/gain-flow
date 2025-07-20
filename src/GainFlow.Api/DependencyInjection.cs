using System.Reflection;
using GainFlow.Api.Common;
using GainFlow.Api.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GainFlow.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        var currentAssembly = Assembly.GetExecutingAssembly();
        IEnumerable<Type> endpoints = currentAssembly.GetTypes().Where(t =>
            typeof(IEndpoint).IsAssignableFrom(t) && t != typeof(IEndpoint) &&
            t is { IsPublic: true, IsAbstract: false });

        foreach (Type endpoint in endpoints)
        {
            services.AddSingleton(typeof(IEndpoint), endpoint);
        }

        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services,
        ConfigurationManager configurationManager)
    {
        string? connectionString = configurationManager.GetConnectionString("Database");

        services.AddDbContext<IdentityApplicationDbContext>(options =>
        {
            options.UseSqlite(connectionString).UseSnakeCaseNamingConvention();
        });

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(connectionString).UseSnakeCaseNamingConvention();
        });

        return services;
    }

    public static IServiceCollection AddIdentity(this IServiceCollection services, IWebHostEnvironment environment)
    {
        services.Configure<IdentityOptions>(options =>
        {
            if (!environment.IsDevelopment())
            {
                return;
            }

            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 3;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredUniqueChars = 0;
        });


        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<IdentityApplicationDbContext>();

        return services;
    }

    public static WebApplication UseEndpoints(this WebApplication app)
    {
        IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        foreach (IEndpoint endpoint in endpoints)
        {
            endpoint.AddPoint(app);
        }

        return app;
    }

    public static async Task ApplyMigrations(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        IdentityApplicationDbContext identityContext =
            scope.ServiceProvider.GetRequiredService<IdentityApplicationDbContext>();
        ApplicationDbContext applicationContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        try
        {
            await identityContext.Database.MigrateAsync();
            await applicationContext.Database.MigrateAsync();
            app.Logger.LogInformation("Migrations applied");
        }
        catch (Exception e)
        {
            app.Logger.LogError(e, "Error applying migrations");
            throw;
        }
    }
}
