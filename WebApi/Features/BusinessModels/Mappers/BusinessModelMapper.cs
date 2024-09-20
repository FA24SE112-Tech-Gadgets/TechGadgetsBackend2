using WebApi.Data.Entities;
using WebApi.Features.BusinessModels.Models;

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
}
