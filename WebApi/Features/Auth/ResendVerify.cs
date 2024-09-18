using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Common.Endpoints;
using WebApi.Common.Exceptions;
using WebApi.Common.Filters;
using WebApi.Data;
using WebApi.Services.VerifyCode;

namespace WebApi.Features.Auth;

public class ResendVerify
{
    public record Request(string Email);

    public sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(r => r.Email).NotEmpty().EmailAddress();
        }
    }

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("auth/resend", Handler)
                .WithTags("Auths")
                .WithDescription("This API is for resend verify code")
                .WithSummary("Resend verify code")
                .Produces<NoContentResult>(StatusCodes.Status200OK)
                .WithRequestValidation<Request>();
        }
    }

    public static async Task<IResult> Handler([FromBody] Request request, AppDbContext context, [FromServices] VerifyCodeService verifyCodeService)
    {
        var user = await context.Users.FirstOrDefaultAsync(user => user.Email == request.Email);
        if (user == null)
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEV_0000)
                .AddReason("user", "Người dùng không tồn tại")
                .Build();
        }

        await verifyCodeService.ResendVerifyCodeAsync(user);

        return Results.NoContent();
    }
}
