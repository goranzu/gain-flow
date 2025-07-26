using System.Reflection;
using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Middleware;
using GainFlow.Api.Shared.Persistence;
using GainFlow.Api.Shared.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GainFlow.Api.Shared.Extensions;

public static class ServiceCollectionExtensions
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

    public static IServiceCollection AddQueryHandlers(this IServiceCollection services)
    {
        services.Scan(scan =>
            scan.FromAssembliesOf(typeof(Program))
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
                .AsImplementedInterfaces().WithScopedLifetime());

        return services;
    }

    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        services.Scan(scan =>
            scan.FromAssembliesOf(typeof(Program))
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), publicOnly: false)
                .AsImplementedInterfaces().WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
                .AsImplementedInterfaces().WithScopedLifetime());

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        return services;
    }

    public static IServiceCollection AddErrorHandling(this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Extensions.Add("requestId", context.HttpContext.TraceIdentifier);
            };
        });

        services.AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }

    public static IServiceCollection AddSpa(this IServiceCollection services)
    {
        services.AddSpaStaticFiles(configuration =>
        {
            configuration.RootPath = "wwwroot";
        });
        return services;
    }
}
