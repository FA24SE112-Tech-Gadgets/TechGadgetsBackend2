using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Endpoints;
using WebApi.Common.Exceptions;
using WebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Features.BusinessModels;

public class DeleteBusinessModel
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("business-model/{id}", Handler)
                .WithTags("Business Model")
                .WithDescription("This API is for delete business model by Id")
                .WithSummary("Delete business model by Id")
                .Produces(StatusCodes.Status204NoContent);
                //.WithJwtValidation()
                //.WithRolesValidation(Role.Admin);
        }
    }

    public static async Task<IResult> Handler([FromRoute] int id, AppDbContext context)
    {
        var businessModel = await context.BusinessModels.AnyAsync(u => u.Id == id);
        if (!businessModel)
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEB_00)
                .AddReason("businessModel", "Không tìm thấy mô hình kinh doanh")
                .Build();
        }

        var isExist = await context.SellerApplications.AnyAsync(va => va.BusinessModelId == id);

        if (!isExist)
        {
            await context.BusinessModels.Where(u => u.Id == id).ExecuteDeleteAsync();
        }
        else
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEB_02)
                .AddReason("businessModel", "Không thể xóa mô hình kinh doanh này")
                .Build();
        }

        return Results.NoContent();
    }
}
