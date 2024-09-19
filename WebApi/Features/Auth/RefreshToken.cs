using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Endpoints;
using WebApi.Common.Filters;
using WebApi.Data;
using WebApi.Services.Auth;
using WebApi.Features.Auth.Models;
using WebApi.Features.Auth.Mappers;

namespace WebApi.Features.Auth;

public class RefreshToken
{
    public record Request(string RefreshToken);

    public sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(r => r.RefreshToken)
                .NotEmpty()
                .WithMessage("Token không được để trống");
        }
    }

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("auth/refresh", Handler)
                .WithTags("Auths")
                .WithDescription("This API is for refresh new token")
                .WithSummary("Refresh token")
                .Produces<TokenResponse>(StatusCodes.Status200OK)
                .WithRequestValidation<Request>();
        }
    }

    public static async Task<IResult> Handler([FromBody] Request request, AppDbContext context, [FromServices] TokenService tokenService)
    {
        var userInfo = await tokenService.ValidateRefreshToken(request.RefreshToken, context);

        var tokenInfo = userInfo.ToTokenRequest();
        string token = tokenService.CreateToken(tokenInfo!);
        string rfToken = tokenService.CreateRefreshToken(tokenInfo!);

        return Results.Ok(new TokenResponse
        {
            Token = token,
            RefreshToken = rfToken
        });
    }
}
