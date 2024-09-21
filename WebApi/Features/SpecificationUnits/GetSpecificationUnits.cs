using FluentValidation;
using WebApi.Common.Endpoints;
using WebApi.Common.Paginations;
using WebApi.Data;
using WebApi.Features.SpecificationUnits.Models;
using WebApi.Features.SpecificationUnits.Mappers;
using WebApi.Common.Filters;
using WebApi.Features.Brands.Models;

namespace WebApi.Features.SpecificationUnits;

public class GetSpecificationUnits
{
    public class Request : IPagedRequest
    {
        public string? Name { get; set; }

        public int? Page { get; set; }

        public int? PageSize { get; set; }
    }

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("specification-unit", Handler)
                .WithTags("Specification Unit")
                .WithDescription("Get list of specification units or get by unit's name")
                .WithSummary("List of specification units")
                .Produces<PagedList<SpecificationUnitResponse>>(StatusCodes.Status200OK);
        }
    }

    public static async Task<IResult> Handler([AsParameters] Request request, AppDbContext context)
    {
        var response = await context.SpecificationUnits
                 .Where(u => u.Name.Contains(request.Name ?? ""))
                 .Select(u => u.ToSpecificationUnitResponse())
                 .ToPagedListAsync(request);

        return Results.Ok(response);
    }
}
