using Microsoft.EntityFrameworkCore;
using WebApi.Common.Endpoints;
using WebApi.Common.Exceptions;
using WebApi.Common.Filters;
using WebApi.Data;
using WebApi.Data.Entities;
using WebApi.Services.Storage;

namespace WebApi.Features.Brands;

public class DeleteBrandById
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("brands/{id}", Handler)
                .WithTags("Brands")
                .WithDescription("This API is to delete brand by id")
                .WithSummary("Delete Brand")
                .Produces(StatusCodes.Status204NoContent)
                .WithJwtValidation()
                .WithRolesValidation(Role.Admin);
        }
    }

    public static async Task<IResult> Handler(int id, AppDbContext context, GoogleStorageService storageService)
    {
        var brand = await context.Brands.FirstOrDefaultAsync(b => b.Id == id);
        if (brand is null)
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEB_00)
                .AddReason("brand", "Không tìm thấy thương hiệu")
                .Build();
        }

        var anyGadget = await context.Gadgets.AnyAsync(g => g.BrandId == id);
        var anyGadgetRequestBrand = await context.GadgetRequestBrands.AnyAsync(g => g.BrandId == id);

        if (anyGadget || anyGadgetRequestBrand)
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEB_02)
                .AddReason("brand", "Không thể xóa thương hiệu này do nó đang được tham chiếu")
                .Build();
        }

        await context.Brands.Where(b => b.Id == id).ExecuteDeleteAsync();
        await storageService.DeleteFileFromCloudStorage(brand.LogoUrl);

        return Results.NoContent();
    }
}
