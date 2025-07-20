using GainFlow.Api.Common;
using Microsoft.AspNetCore.Identity;

namespace GainFlow.Api.Endpoints;

public sealed class Logout : IEndpoint
{
    public void AddPoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost("/api/logout", async (SignInManager<IdentityUser> signInManager) =>
            {
                await signInManager.SignOutAsync();
                return Results.NoContent();
            })
            .RequireAuthorization();
    }
}
