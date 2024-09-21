using Microsoft.EntityFrameworkCore;
using WebApi.Common.Endpoints;
using WebApi.Common.Exceptions;
using WebApi.Common.Filters;
using WebApi.Common.Paginations;
using WebApi.Data;
using WebApi.Features.Brands.Mapper;
using WebApi.Features.Brands.Models;

namespace WebApi.Features.Brands;

public class GetBrandsByCategoryId
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
            app.MapGet("brands/categories/{categoryId}", Handler)
                .WithTags("Brands")
                .WithDescription("This API is to get brands by category id")
                .WithSummary("Get Brands by Category Id")
                .Produces<PagedList<BrandResponse>>(StatusCodes.Status200OK)
                .WithRequestValidation<Request>();
        }
    }

    public static async Task<IResult> Handler(int categoryId, [AsParameters] Request request, AppDbContext context)
    {
        if (!await context.Categories.AnyAsync(c => c.Id == categoryId))
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEB_00)
                .AddReason("category", "Không tìm thấy thể loại")
                .Build();
        }

        var response = await context.Brands
                                .Where(b => b.BrandCategories.Any(bc => bc.CategoryId == categoryId)
                                        && b.Name.Contains(request.Name ?? ""))
                                .Select(b => b.ToBrandResponse())
                                .ToPagedListAsync(request);

        return Results.Ok(response);
    }
}
