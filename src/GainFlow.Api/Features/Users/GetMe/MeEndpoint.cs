using System.Security.Claims;
using GainFlow.Api.Infrastructure.Data;
using GainFlow.Api.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace GainFlow.Api.Features.Users.GetMe;

public sealed class MeEndpoint : IEndpoint
{
    public void AddPoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/api/me",
                async (HttpContext httpContext, ApplicationDbContext applicationDbContext) =>
                {
                    ClaimsPrincipal user = httpContext.User;
                    string? userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

                    UserResponse? userResponse = await applicationDbContext.Users
                        .Where(u => u.IdentityId == userId)
                        .Select(u => new UserResponse(u.Id, u.Email))
                        .FirstOrDefaultAsync();

                    if (userResponse is null)
                    {
                        return Results.Unauthorized();
                    }

                    return Results.Ok(userResponse);
                })
            .RequireAuthorization();
    }

}
