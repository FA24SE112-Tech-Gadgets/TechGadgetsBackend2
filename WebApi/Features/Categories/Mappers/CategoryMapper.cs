using WebApi.Data.Entities;
using WebApi.Features.Categories.Models;

namespace WebApi.Features.Categories.Mappers;

public static class CategoryMapper
{
    public static CategoryResponse? ToCategoryResponse(this Category? category)
    {
        if (category != null)
        {
            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                ParentId = category.ParentId,
                IsAdminCreated = category.IsAdminCreated,
            };
        }
        return null;
    }
}
