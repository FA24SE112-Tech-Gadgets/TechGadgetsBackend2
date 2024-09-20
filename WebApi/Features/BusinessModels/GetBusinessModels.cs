using Microsoft.EntityFrameworkCore;
using WebApi.Common.Endpoints;
using WebApi.Common.Filters;
using WebApi.Common.Paginations;
using WebApi.Data;
using WebApi.Features.BusinessModels.Mappers;
using WebApi.Features.BusinessModels.Models;

namespace WebApi.Features.BusinessModels;

public class GetBusinessModels
{
    public class Requestt : PaginationRequest
    {
        public string? Name { get; set; }
    }

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("business-model", Handler)
                .WithTags("Business Model")
                .WithDescription("Get list of business models or get by business model's name")
                .WithSummary("List of business models")
                .Produces<BusinessModelsResponse>(StatusCodes.Status200OK)
                .WithJwtValidation();
        }
    }

    public static async Task<IResult> Handler([AsParameters] Requestt request, AppDbContext context)
    {
        var businessModelsData = await context.BusinessModels
                 .Where(u => u.Name.Contains(request.Name ?? ""))
                 .Skip(request.Page * request.Limit)
                 .Take(request.Limit)
                 .ToListAsync();
        int count = await context.BusinessModels.CountAsync();
        var businessModels = businessModelsData.ToListBusinessModelsResponse();

        BusinessModelsResponse businessModelsResponse = new BusinessModelsResponse(businessModels!, count);
        businessModelsResponse.SetPage(request.Page);
        businessModelsResponse.SetLimit(request.Limit);

        return Results.Ok(businessModelsResponse);
    }
}
