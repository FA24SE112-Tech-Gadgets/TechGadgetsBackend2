//using FluentValidation;
//using Microsoft.EntityFrameworkCore;
//using WebApi.Common.Endpoints;
//using WebApi.Common.Exceptions;
//using WebApi.Common.Filters;
//using WebApi.Data;
//using WebApi.Data.Entities;
//using WebApi.Features.Brands.Mapper;
//using WebApi.Features.Brands.Models;
//using WebApi.Services.Storage;

//namespace WebApi.Features.Categories;

//public class CreateCategory
//{
//    public class Request
//    {
//        public int? ParentId { get; set; }
//        public string Name { get; set; } = default!;
//    }
//    public sealed class Validator : AbstractValidator<Request>
//    {
//        public Validator()
//        {
//            RuleFor(r => r.Name)
//                .NotEmpty()
//                .WithMessage("Tên không được để trống");
//        }
//    }

//    public sealed class Endpoint : IEndpoint
//    {
//        public void MapEndpoint(IEndpointRouteBuilder app)
//        {
//            app.MapPost("categories", Handler)
//                .WithTags("Categories")
//                .WithDescription("This API is to create a category")
//                .WithSummary("Create Category")
//                .Produces<BrandResponse>(StatusCodes.Status201Created)
//                .WithJwtValidation()
//                .WithRequestValidation<Request>()
//                .WithRolesValidation(Role.Admin);
//        }
//    }

//    public static async Task<IResult> Handler(Request request, AppDbContext context, GoogleStorageService storageService)
//    {
//        if (await context.Brands.AnyAsync(b => b.Name == request.Name))
//        {
//            throw TechGadgetException.NewBuilder()
//                .WithCode(TechGadgetErrorCode.WEB_01)
//                .AddReason("name", "Tên thương hiệu đã tồn tại")
//                .Build();
//        }

//        string? logoUrl = null;
//        try
//        {
//            logoUrl = await storageService.UploadFileToCloudStorage(request.Logo, Guid.NewGuid().ToString());
//        }
//        catch (Exception)
//        {
//            if (logoUrl != null)
//            {
//                await storageService.DeleteFileFromCloudStorage(logoUrl);
//            }
//            throw TechGadgetException.NewBuilder()
//                .WithCode(TechGadgetErrorCode.WES_00)
//                .AddReason("logo", "Lỗi khi lưu logo")
//                .Build();
//        }

//        var brand = new Brand
//        {
//            Name = request.Name,
//            LogoUrl = logoUrl
//        };

//        context.Brands.Add(brand);
//        await context.SaveChangesAsync();

//        var response = brand.ToBrandResponse();

//        return Results.Json(response, statusCode: 201);
//    }
//}
