using WebApi.Data.Entities;
using WebApi.Features.Specification.Models;

namespace WebApi.Features.Specification.Mappers;

public static class SpecificaitonUnitMapper
{
    public static SpecificationUnitResponse? ToSpecificationUnitResponse(this SpecificationUnit? specificationUnit)
    {
        if (specificationUnit != null)
        {
            return new SpecificationUnitResponse
            {
                Id = specificationUnit.Id,
                Name = specificationUnit.Name,
            };
        }
        return null;
    }
}
