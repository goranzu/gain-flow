using GainFlow.Api.Common;

namespace GainFlow.Api.Endpoints;

public class HelloWorld : IEndpoint
{
    public void AddPoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/", () => "Hello World!");
    }
}
