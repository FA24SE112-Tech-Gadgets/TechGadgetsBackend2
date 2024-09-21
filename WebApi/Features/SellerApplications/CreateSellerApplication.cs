using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Endpoints;
using WebApi.Common.Exceptions;
using WebApi.Common.Filters;
using WebApi.Data.Entities;
using WebApi.Data;
using WebApi.Features.SpecificationUnits.Models;
using Microsoft.EntityFrameworkCore;
using WebApi.Features.SpecificationUnits.Mappers;
using WebApi.Services.Auth;

namespace WebApi.Features.SellerApplications;

public class CreateSellerApplication
{
    public class Request
    {
        public int UserId { get; set; }
        public string? CompanyName { get; set; }
        public string ShopName { get; set; }
        public string ShippingAddress { get; set; }
        public int BusinnessModelId { get; set; }
        public string? BusinessRegistrationCertificateUrl { get; set; }
        public string TaxCode { get; set; }
        public string? RejectReason { get; set; }
        public string Type { get; set; }
    }

    public sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(sp => sp.CompanyName)
                .NotEmpty()
                .When(sp => RequiresCompanyName(sp.BusinnessModelId))
                .WithMessage("Tên công ty không được để trống");
        }
        private bool RequiresCompanyName(int businessModelId)
        {
            var modelsRequiringCompanyName = new List<int> { 2, 3 }; //Hộ kinh doanh, Công ty
            return modelsRequiringCompanyName.Contains(businessModelId);
        }
    }


    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("seller-application", Handler)
                .WithTags("Seller Application")
                .WithDescription("API is for register seller")
                .WithSummary("Seller application")
                .Produces<SpecificationUnitResponse>(StatusCodes.Status200OK)
                .WithJwtValidation()
                .WithRolesValidation(Role.Admin)
                .WithRequestValidation<Request>();
        }
    }

    public static async Task<IResult> Handler([FromBody] Request request, AppDbContext context, [FromServices] TokenService tokenService)
    {
        var isDuplicated = await context.SpecificationUnits.AnyAsync(u => u.Name == request.CompanyName);
        if (isDuplicated)
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEB_01)
                .AddReason("name", "Tên này đã được tạo trước đó.")
                .Build();
        }
        var specificationUnit = new SpecificationUnit
        {
            Name = request.Name,
        };
        context.SpecificationUnits.Add(specificationUnit);
        await context.SaveChangesAsync();
        return Results.Created("Created", specificationUnit.ToSpecificationUnitResponse());
    }
}
