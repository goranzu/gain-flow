using System.Security.Claims;
using GainFlow.Api.Shared.Abstractions;
using GainFlow.Api.Shared.Contracts.Responses;
using GainFlow.Api.Shared.Domain.Entities;
using GainFlow.Api.Shared.Persistence.Queries;

namespace GainFlow.Api.Features.Users.GetMe;

public sealed class MeEndpoint : IEndpoint
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/api/me",
                async (HttpContext httpContext, IRepository<User> userRepository, CancellationToken cancellationToken) =>
                {
                    ClaimsPrincipal user = httpContext.User;
                    string? userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

                    if (string.IsNullOrEmpty(userId))
                    {
                        return Results.Unauthorized();
                    }

                    DataQuery<User> query = new DataQuery<User>()
                        .Add(new UserByIdentityId(userId));
                    var projection = new UserResponseProjection();

                    UserResponse? userResponse = await userRepository.FindAsync(query, projection, cancellationToken);

                    if (userResponse is null)
                    {
                        return Results.Unauthorized();
                    }

                    return Results.Ok(userResponse);
                })
            .RequireAuthorization();
    }

}
