using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Common.Endpoints;
using WebApi.Common.Exceptions;
using WebApi.Common.Filters;
using WebApi.Data;
using WebApi.Data.Entities;

namespace WebApi.Features.SpecificationUnits;

public class DeleteSpecificationUnitById
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("specification-unit/{id}", Handler)
                .WithTags("Specification Unit")
                .WithDescription("This API is for delete specification unit by Id")
                .WithSummary("Delete specification unit by Id")
                .Produces(StatusCodes.Status204NoContent)
                .WithJwtValidation()
                .WithRolesValidation(Role.Admin);
        }
    }

    public static async Task<IResult> Handler([FromRoute] int id, AppDbContext context)
    {
        var specificationUnit = await context.SpecificationUnits.AnyAsync(u => u.Id == id);
        if (!specificationUnit)
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEB_00)
                .AddReason("specificationUnit", "Không tìm thấy đơn vị")
                .Build();
        }

        var specificationValue = await context.SpecificationValues.AnyAsync(va => va.SpecificationUnitId == id);
        var gadgetRequestSpecifications = await context.GadgetRequestSpecifications.AnyAsync(re => re.SpecificationUnitId == id);

        if (!specificationValue && !gadgetRequestSpecifications)
        {
            await context.SpecificationUnits.Where(u => u.Id == id).ExecuteDeleteAsync();
        } else
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEB_02)
                .AddReason("specificationUnit", "Không thể xóa đơn vị này")
                .Build();
        }

        return Results.NoContent();
    }
}
