using WebApi.Common.Endpoints;
using WebApi.Features.Users.Mappers;
using WebApi.Features.Users.Models;
using WebApi.Services.Auth;

namespace WebApi.Features.Users;

public class GetCurrentUser
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("users/current", Handler)
                .WithTags("Users")
                .WithDescription("This API is to get current user")
                .WithSummary("Current user")
                .Produces<UserResponse>(StatusCodes.Status200OK);
        }
    }

    public static async Task<IResult> Handler(CurrentUserService service)
    {
        var user = await service.GetCurrentUser();
        return Results.Ok(user.ToUserResponse());
    }
}
