using GainFlow.Api.Shared.Abstractions;

namespace GainFlow.Api.Shared.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseEndpoints(this WebApplication app)
    {
        IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        foreach (IEndpoint endpoint in endpoints)
        {
            endpoint.AddEndpoint(app);
        }

        return app;
    }
}
