using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Endpoints;
using WebApi.Data;
using WebApi.Features.SpecificationUnits.Models;
using Microsoft.EntityFrameworkCore;
using WebApi.Features.SpecificationUnits.Mappers;
using WebApi.Common.Exceptions;

namespace WebApi.Features.SpecificationUnits;

public class GetSpecificationUnitDetail
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("specification-unit/{id}", Handler)
                .WithTags("Specification Unit")
                .WithDescription("This API is for getting specification unit detail")
                .WithSummary("Create specification unit")
                .Produces<SpecificationUnitResponse>(StatusCodes.Status200OK);
        }
    }

    public static async Task<IResult> Handler([FromRoute] int id, AppDbContext context)
    {
        var specificationUnit = await context.SpecificationUnits.FirstOrDefaultAsync(su => su.Id == id);
        if (specificationUnit == null)
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEB_0005)
                .AddReason("Lỗi đơn vị", "Không tìm thấy đơn vị")
                .Build();
        }
        return Results.Ok(specificationUnit.ToSpecificationUnitResponse());
    }
}
