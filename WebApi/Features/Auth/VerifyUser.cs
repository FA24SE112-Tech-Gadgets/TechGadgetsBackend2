using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Common.Endpoints;
using WebApi.Common.Exceptions;
using WebApi.Common.Filters;
using WebApi.Data;
using WebApi.Features.Auth.Mappers;
using WebApi.Features.Auth.Models;
using WebApi.Services.Auth;
using WebApi.Services.VerifyCode;

namespace WebApi.Features.Auth;

public class VerifyUser
{
    public record Request(string Email, string Code);

    public sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage("Email không được để trống")
                .EmailAddress()
                .WithMessage("Email không hợp lệ");
            RuleFor(r => r.Code)
                .NotEmpty()
                .WithMessage("Mã xác thực không được để trống");
        }
    }

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("auth/verify", Handler)
                .WithTags("Auths")
                .WithDescription("This API is for verify user")
                .WithSummary("Verify user")
                .Produces<TokenResponse>(StatusCodes.Status200OK)
                .WithRequestValidation<Request>();
        }
    }

    public static async Task<IResult> Handler([FromBody] Request request, AppDbContext context
        , [FromServices] VerifyCodeService verifyCodeService, [FromServices] TokenService tokenService)
    {
        var user = await context.Users.FirstOrDefaultAsync(user => user.Email == request.Email);
        if (user == null)
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEV_0000)
                .AddReason("Lỗi người dùng", "Người dùng không tồn tại")
                .Build();
        }

        await verifyCodeService.VerifyUserAsync(user, request.Code);

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
