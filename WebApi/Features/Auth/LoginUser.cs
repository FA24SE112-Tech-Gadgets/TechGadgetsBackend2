using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Common.Endpoints;
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
            RuleFor(r => r.Email).NotEmpty();
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
            .Produces<TokenResponse>(StatusCodes.Status200OK);
        }
    }

    public static async Task<IResult> Handler([FromBody] Request request, AppDbContext context, IValidator<Request> validator, [FromServices] TokenService tokenService)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }

        var user = await context.Users.FirstOrDefaultAsync(user => user.Email == request.Email);
        var tokenInfo = TokenMapper.MapToTokenCreateRequest(user);
        string token = tokenService.CreateToken(tokenInfo);
        string rfToken = tokenService.CreateRefreshToken(tokenInfo);
        return Results.Ok(new TokenResponse(token, rfToken));
    }
}
