using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Common.Endpoints;
using WebApi.Common.Exceptions;
using WebApi.Common.Filters;
using WebApi.Data;
using WebApi.Data.Entities;
using WebApi.Features.SpecificationUnits.Mappers;
using WebApi.Features.SpecificationUnits.Models;

namespace WebApi.Features.SpecificationUnits;

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
            app.MapPost("specification-unit", Handler)
                .WithTags("Specification Unit")
                .WithDescription("This API is for Admin create specification unit")
                .WithSummary("Create specification unit")
                .Produces<SpecificationUnitResponse>(StatusCodes.Status200OK)
                .WithJwtValidation()
                .WithRolesValidation(Role.Admin)
                .WithRequestValidation<Request>();
        }
    }

    public static async Task<IResult> Handler([FromBody] Request request, AppDbContext context)
    {
        var isDuplicated = await context.SpecificationUnits.AnyAsync(u => u.Name == request.Name);
        if (isDuplicated)
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEB_01)
                .AddReason("name", "Tên này đã được tạo trước đó.")
                .Build();
        }
        var specificationUnit = new SpecificationUnit
        {
            Name = request.Name,
        };
        context.SpecificationUnits.Add(specificationUnit);
        await context.SaveChangesAsync();
        return Results.Created("Created", specificationUnit.ToSpecificationUnitResponse());
    }
}
