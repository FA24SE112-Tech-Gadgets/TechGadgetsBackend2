using Microsoft.EntityFrameworkCore;
using WebApi.Common.Endpoints;
using WebApi.Common.Exceptions;
using WebApi.Data;
using WebApi.Features.Brands.Mapper;
using WebApi.Features.Brands.Models;

namespace WebApi.Features.Brands;

public class GetBrandById
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("brands/{id}", Handler)
                .WithTags("Brands")
                .WithDescription("This API is to get brand by id")
                .WithSummary("Get Brand")
                .Produces<BrandResponse>(StatusCodes.Status200OK);
        }
    }

    public static async Task<IResult> Handler(int id, AppDbContext context)
    {
        var brand = await context.Brands.FirstOrDefaultAsync(b => b.Id == id);

        if (brand is null)
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEB_00)
                .AddReason("brand", "Không tìm thấy thương hiệu")
                .Build();
        }

        return Results.Ok(brand.ToBrandResponse());
    }
}
