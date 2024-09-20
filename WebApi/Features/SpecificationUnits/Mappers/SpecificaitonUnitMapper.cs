using WebApi.Data.Entities;
using WebApi.Features.SpecificationUnits.Models;

namespace WebApi.Features.SpecificationUnits.Mappers;

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

    public static List<SpecificationUnitResponse>? ToListSpecificationUnitsResponse(this List<SpecificationUnit> specificationUnits)
    {
        if (specificationUnits != null)
        {
            return specificationUnits.Select(unit => new SpecificationUnitResponse
            {
                Id = unit.Id,
                Name = unit.Name
            }).ToList();
        }
        return null;
    }
}
