using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Common.Endpoints;
using WebApi.Common.Exceptions;
using WebApi.Common.Filters;
using WebApi.Data;
using WebApi.Features.SpecificationUnits.Mappers;
using WebApi.Features.SpecificationUnits.Models;

namespace WebApi.Features.SpecificationUnits;

public class GetSpecificationUnitDetail
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("specification-unit/{id}", Handler)
                .WithTags("Specification Unit")
                .WithDescription("This API is for getting specification unit detail")
                .WithSummary("Get specification unit by Id")
                .Produces<SpecificationUnitResponse>(StatusCodes.Status200OK)
                .WithJwtValidation();
        }
    }

    public static async Task<IResult> Handler([FromRoute] int id, AppDbContext context)
    {
        var specificationUnit = await context.SpecificationUnits.FirstOrDefaultAsync(su => su.Id == id);
        if (specificationUnit == null)
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEB_00)
                .AddReason("specificationUnit", "Không tìm thấy đơn vị")
                .Build();
        }
        return Results.Ok(specificationUnit.ToSpecificationUnitResponse());
    }
}
