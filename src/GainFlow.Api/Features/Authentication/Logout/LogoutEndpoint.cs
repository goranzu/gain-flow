using GainFlow.Api.Shared.Common;
using Microsoft.AspNetCore.Identity;

namespace GainFlow.Api.Features.Authentication.Logout;

public sealed class LogoutEndpoint : IEndpoint
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
