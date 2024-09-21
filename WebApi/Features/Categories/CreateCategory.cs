using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebApi.Common.Endpoints;
using WebApi.Common.Exceptions;
using WebApi.Common.Filters;
using WebApi.Data;
using WebApi.Data.Entities;
using WebApi.Features.Categories.Mappers;
using WebApi.Features.Categories.Models;

namespace WebApi.Features.Categories;

public class CreateCategory
{
    public class Request
    {
        public int? ParentId { get; set; }
        public string Name { get; set; } = default!;
    }
    public sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage("Tên không được để trống");
        }
    }

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("categories", Handler)
                .WithTags("Categories")
                .WithDescription("This API is to create a category")
                .WithSummary("Create Category")
                .Produces<CategoryResponse>(StatusCodes.Status201Created)
                .WithJwtValidation()
                .WithRequestValidation<Request>()
                .WithRolesValidation(Role.Admin);
        }
    }

    public static async Task<IResult> Handler(Request request, AppDbContext context)
    {
        if (request.ParentId is not null
            && !await context.Categories.AnyAsync(c => c.Id == request.ParentId))
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEB_00)
                .AddReason("category", "Không tìm thấy thể loại")
                .Build();
        }

        if (await context.Categories.AnyAsync(c => c.Name == request.Name))
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEB_01)
                .AddReason("category", "Thể loại đã tồn tại")
                .Build();
        }

        var category = new Category
        {
            Name = request.Name,
            ParentId = request.ParentId,
            IsAdminCreated = true
        };

        context.Categories.Add(category);
        await context.SaveChangesAsync();

        return Results.Json(category.ToCategoryResponse(), statusCode: StatusCodes.Status201Created);
    }
}
