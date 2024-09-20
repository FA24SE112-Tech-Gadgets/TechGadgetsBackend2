using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Common.Endpoints;
using WebApi.Common.Exceptions;
using WebApi.Common.Filters;
using WebApi.Data;
using WebApi.Data.Entities;
using WebApi.Features.Auth.Mappers;
using WebApi.Features.Auth.Models;
using WebApi.Services.Auth;

namespace WebApi.Features.Specification;

public class CreateSpecificationUnit
{
    public record Request(string Name);

    public sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage("Tên không được để trống");
        }
    }

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("specification/unit", Handler)
                .WithTags("SpecificationUnit")
                .WithDescription("This API is for Admin create specification unit")
                .WithSummary("Create specification unit")
                .Produces<SpecificationUnit>(StatusCodes.Status200OK)
                .WithJwtValidation()
                .WithRequestValidation<Request>()
                .WithRolesValidation(Role.Admin, Role.Buyer);
        }
    }

    public static async Task<IResult> Handler([FromBody] Request request, AppDbContext context, [FromServices] TokenService tokenService)
    {
        var user = await context.Users.FirstOrDefaultAsync(user => user.Email == request.Name);

        if (user == null)
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEB_0002)
                .AddReason("Lỗi người dùng", "Người dùng không tồn tại")
                .Build();
        }

        if (user.Status == UserStatus.Pending)
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEA_0001)
                .AddReason("Lỗi người dùng", "Người dùng chưa xác thực")
                .Build();
        }

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
