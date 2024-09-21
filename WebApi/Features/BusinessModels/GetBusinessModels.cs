using WebApi.Common.Endpoints;
using WebApi.Common.Paginations;
using WebApi.Data;
using WebApi.Features.BusinessModels.Mappers;
using WebApi.Features.BusinessModels.Models;

namespace WebApi.Features.BusinessModels;

public class GetBusinessModels
{
    public class Requestt : IPagedRequest
    {
        public string? Name { get; set; }

        public int? Page { get; set; }

        public int? PageSize { get; set; }
    }

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("business-model", Handler)
                .WithTags("Business Model")
                .WithDescription("Get list of business models or get by business model's name")
                .WithSummary("List of business models")
                .Produces<PagedList<BusinessModelResponse>>(StatusCodes.Status200OK);
        }
    }

    public static async Task<IResult> Handler([AsParameters] Requestt request, AppDbContext context)
    {
        var response = await context.BusinessModels
                 .Where(bu => bu.Name.Contains(request.Name ?? ""))
                 .Select(bu => bu.ToBusinessModelResponse())
                 .ToPagedListAsync(request);

        return Results.Ok(response);
    }
}
