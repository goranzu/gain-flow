using System.Reflection;
using GainFlow.Api.Common;

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

    public static WebApplication UseEndpoints(this WebApplication app)
    {
        IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        foreach (IEndpoint endpoint in endpoints)
        {
            endpoint.AddPoint(app);
        }

        return app;
    }
}
