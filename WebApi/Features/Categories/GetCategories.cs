using WebApi.Common.Endpoints;
using WebApi.Common.Filters;
using WebApi.Common.Paginations;
using WebApi.Data;
using WebApi.Features.Categories.Mappers;
using WebApi.Features.Categories.Models;

namespace WebApi.Features.Categories;

public class GetCategories
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
            app.MapGet("categories", Handler)
                .WithTags("Categories")
                .WithDescription("This API is to get categories")
                .WithSummary("Get Categories")
                .Produces<PagedList<CategoryResponse>>(StatusCodes.Status200OK)
                .WithRequestValidation<Request>();
        }
    }

    public static async Task<IResult> Handler([AsParameters] Request request, AppDbContext context)
    {
        var response = await context.Categories
                                .Where(c => c.Name.Contains(request.Name ?? ""))
                                .Select(c => c.ToCategoryResponse())
                                .ToPagedListAsync(request);

        return Results.Ok(response);
    }
}
