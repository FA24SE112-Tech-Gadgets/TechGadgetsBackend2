using WebApi.Data.Entities;
using WebApi.Features.Brands.Models;

namespace WebApi.Features.Brands.Mapper;

public static class BrandMapper
{
    public static BrandResponse? ToBrandResponse(this Brand? brand)
    {
        if (brand != null)
        {
            return new BrandResponse
            {
                Id = brand.Id,
                LogoUrl = brand.LogoUrl,
                Name = brand.Name,
            };
        }
        return null;
    }
}
