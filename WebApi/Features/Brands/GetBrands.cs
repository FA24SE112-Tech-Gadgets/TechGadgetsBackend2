using FluentValidation;
using WebApi.Common.Endpoints;
using WebApi.Common.Filters;
using WebApi.Common.Paginations;
using WebApi.Data;
using WebApi.Features.Brands.Mapper;
using WebApi.Features.Brands.Models;

namespace WebApi.Features.Brands;

public class GetBrands
{
    public class Request : IPagedRequest
    {
        public string? Name { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }

    public class RequestValidator : PagedRequestValidator<Request>;

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("brands", Handler)
                .WithTags("Brands")
                .WithDescription("This API is to get brands")
                .WithSummary("Get Brands")
                .Produces<PagedList<BrandResponse>>(StatusCodes.Status200OK)
                .WithJwtValidation()
                .WithRequestValidation<Request>();
        }
    }

    public static async Task<IResult> Handler([AsParameters] Request request, AppDbContext context)
    {
        var response = await context.Brands
                            .Where(b => b.Name.Contains(request.Name ?? ""))
                            .Select(b => b.ToBrandResponse())
                            .ToPagedListAsync(request);

        return Results.Ok(response);
    }
}
