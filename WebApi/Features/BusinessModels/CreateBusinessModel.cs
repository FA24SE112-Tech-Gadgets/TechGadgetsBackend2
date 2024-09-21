using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Endpoints;
using WebApi.Common.Filters;
using WebApi.Data.Entities;
using WebApi.Data;
using Microsoft.EntityFrameworkCore;
using WebApi.Common.Exceptions;
using WebApi.Features.BusinessModels.Mappers;
using WebApi.Features.BusinessModels.Models;

namespace WebApi.Features.BusinessModels;

public class CreateBusinessModel
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
            app.MapPost("business-model", Handler)
                .WithTags("Business Model")
                .WithDescription("This API is for Admin create business model")
                .WithSummary("Create business model")
                .Produces<BusinessModelResponse>(StatusCodes.Status200OK)
                .WithJwtValidation()
                .WithRolesValidation(Role.Admin)
                .WithRequestValidation<Request>();
        }
    }

    public static async Task<IResult> Handler([FromBody] Request request, AppDbContext context)
    {
        var isDuplicated = await context.BusinessModels.AnyAsync(u => u.Name == request.Name);
        if (isDuplicated)
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEB_01)
                .AddReason("name", "Tên này đã được tạo trước đó.")
                .Build();
        }
        var businessModel = new BusinessModel
        {
            Name = request.Name,
        };
        context.BusinessModels.Add(businessModel);
        await context.SaveChangesAsync();
        return Results.Created("Created", businessModel.ToBusinessModelResponse());
    }
}
