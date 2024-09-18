using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Common.Endpoints;
using WebApi.Common.Filters;
using WebApi.Data;
using WebApi.Features.Auth.Mappers;
using WebApi.Features.Auth.Models;
using WebApi.Services.Auth;

namespace WebApi.Features.Auth;
public class LoginUser
{
    public record Request(string Email, string Password);

    public sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(r => r.Email).NotEmpty().EmailAddress();
            RuleFor(r => r.Password).NotEmpty().MinimumLength(8);
        }
    }

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("auth/login", Handler)
                .WithTags("Auths")
                .WithDescription("This API is for user login")
                .WithSummary("Login user")
                .Produces<TokenResponse>(StatusCodes.Status200OK)
                .WithRequestValidation<Request>();
        }
    }

    public static async Task<IResult> Handler([FromBody] Request request, AppDbContext context, [FromServices] TokenService tokenService)
    {
        var user = await context.Users.FirstOrDefaultAsync(user => user.Email == request.Email);
        var tokenInfo = user.ToTokenRequest();
        string token = tokenService.CreateToken(tokenInfo!);
        string rfToken = tokenService.CreateRefreshToken(tokenInfo!);
        return Results.Ok(new TokenResponse
        {
            Token = token,
            RefreshToken = rfToken
        });
    }
}
