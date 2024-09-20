using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Endpoints;
using WebApi.Common.Filters;
using WebApi.Data;
using WebApi.Data.Entities;
using WebApi.Features.Specification.Mappers;
using WebApi.Features.Specification.Models;
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
                .Produces<SpecificationUnitResponse>(StatusCodes.Status200OK)
                .WithJwtValidation()
                .WithRequestValidation<Request>()
                .WithRolesValidation(Role.Admin, Role.Buyer);
        }
    }

    public static async Task<IResult> Handler([FromBody] Request request, AppDbContext context, [FromServices] TokenService tokenService)
    {
        var specificationUnit = new SpecificationUnit
        {
            Name = request.Name,
        };
        context.SpecificationUnits.Add(specificationUnit);
        await context.SaveChangesAsync();
        return Results.Created("Created", specificationUnit.ToSpecificationUnitResponse());
    }
}
