using WebApi.Data.Entities;
using WebApi.Features.BusinessModels.Models;
using WebApi.Features.SpecificationUnits.Models;

namespace WebApi.Features.BusinessModels.Mappers;

public static class BusinessModelMapper
{
    public static BusinessModelResponse? ToBusinessModelResponse(this BusinessModel businessModel)
    {
        if (businessModel == null)
        {
            return null;
        }

        return new BusinessModelResponse
        {
            Id = businessModel.Id,
            Name = businessModel.Name,
        };
    }

    public static List<BusinessModelResponse>? ToListBusinessModelsResponse(this List<BusinessModel> businessModels)
    {
        if (businessModels != null)
        {
            return businessModels.Select(bu => new BusinessModelResponse
            {
                Id = bu.Id,
                Name = bu.Name
            }).ToList();
        }
        return null;
    }
}
