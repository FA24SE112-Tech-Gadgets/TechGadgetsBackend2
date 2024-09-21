using Microsoft.EntityFrameworkCore;
using Npgsql;
using WebApi.Common.Endpoints;
using WebApi.Common.Exceptions;
using WebApi.Data;
using WebApi.Data.Entities;
using WebApi.Features.Categories.Mappers;
using WebApi.Features.Categories.Models;

namespace WebApi.Features.Categories;

public class GetCategoryById
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("categories/{id}", Handler)
                .WithTags("Categories")
                .WithDescription("This API is to get categories")
                .WithSummary("Get Categories")
                .Produces<CategoryDetailResponse>(StatusCodes.Status200OK);
        }
    }

    public static async Task<IResult> Handler(int id, AppDbContext context)
    {
        var category = await context.Categories
                            .Include(c => c.Parent)
                            .FirstOrDefaultAsync(c => c.Id == id);

        if (category is null)
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEB_00)
                .AddReason("category", "Không tìm thấy thể loại")
                .Build();
        }

        var parents = new List<Category>();

        if (category.ParentId.HasValue)
        {
            //parents = await LoadParentsAsync(category.ParentId.Value, context);
            parents = await GetAllParentsRawSqlAsync(category.ParentId.Value, context);
        }

        var response = new CategoryDetailResponse
        {
            Name = category.Name,
            Id = id,
            IsAdminCreated = true,
            Parent = parents.Select(p => p.ToCategoryResponse()).ToList(),
        };

        return Results.Ok(response);
    }

    private static async Task<List<Category>> LoadParentsAsync(int categoryId, AppDbContext context)
    {
        var category = await context.Categories
            .Include(c => c.Parent)
            .FirstOrDefaultAsync(c => c.Id == categoryId);

        if (category == null) return [];

        var parents = new List<Category>();

        if (category.ParentId.HasValue)
        {
            var parentCategories = await LoadParentsAsync(category.ParentId.Value, context);
            parents.AddRange(parentCategories);
        }

        parents.Add(category);
        return parents;
    }

    private static async Task<List<Category>> GetAllParentsRawSqlAsync(int categoryId, AppDbContext context)
    {
        var sql = @"
            WITH RECURSIVE CategoryHierarchy AS (
                SELECT * FROM ""Category"" WHERE ""Id"" = @categoryId
                UNION ALL
                SELECT c.* FROM ""Category"" c
                INNER JOIN CategoryHierarchy ch ON c.""Id"" = ch.""ParentId""
            )
            SELECT * FROM CategoryHierarchy;";

        var categories = await context.Categories
            .FromSqlRaw(sql, new NpgsqlParameter("categoryId", categoryId))
            .AsNoTracking()
            .ToListAsync();

        categories.Reverse();
        return categories;
    }
}
