using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Endpoints;
using WebApi.Common.Paginations;
using WebApi.Data;
using WebApi.Services.Auth;
using WebApi.Features.SpecificationUnits.Models;
using Microsoft.EntityFrameworkCore;
using WebApi.Features.SpecificationUnits.Mappers;

namespace WebApi.Features.SpecificationUnits;

public class GetSpecificationUnits
{
    public class Requestt : PaginationRequest
    {
        public string? Name { get; set; }
    }

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("specification-unit", Handler)
                .WithTags("Specification Unit")
                .WithDescription("Get list of specification units or get by unit's name")
                .WithSummary("List of specification units")
                .Produces<SpecificationUnitsResponse>(StatusCodes.Status200OK);
        }
    }

    public static async Task<IResult> Handler([AsParameters] Requestt request, AppDbContext context, [FromServices] TokenService tokenService)
    {
        var specificationUnitsData = await context.SpecificationUnits
                 .Where(u => u.Name.Contains(request.Name ?? ""))
                 .Skip(request.Page * request.Limit)
                 .Take(request.Limit)
                 .ToListAsync();
        int count = await context.SpecificationUnits.CountAsync();
        var specificationUnits = specificationUnitsData.ToListSpecificationUnitsResponse();

        SpecificationUnitsResponse specificationUnitsResponse = new SpecificationUnitsResponse(specificationUnits!, count);
        specificationUnitsResponse.SetPage(request.Page);
        specificationUnitsResponse.SetLimit(request.Limit);

        return Results.Ok(specificationUnitsResponse);
    }
}
