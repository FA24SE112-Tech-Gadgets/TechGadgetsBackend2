using WebApi.Common.Exceptions;
using WebApi.Data.Entities;
using WebApi.Services.Auth;

namespace WebApi.Common.Filters;

public class RolesFilter(CurrentUserService currentUserService, Role[] acceptedRoles) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var user = await currentUserService.GetCurrentUser();
        if (acceptedRoles.Contains(user!.Role))
        {
            return await next(context);
        }
        else
        {
            var reason = new Reason("role", "Tài khoản không đủ thẩm quyền để truy cập API này.");
            var reasons = new List<Reason> { reason };
            var errorResponse = new TechGadgetErrorResponse
            {
                Code = TechGadgetErrorCode.WEA_01.Code,
                Title = TechGadgetErrorCode.WEA_01.Title,
                Reasons = reasons
            };
            return Results.Json(errorResponse, statusCode: (int)TechGadgetErrorCode.WEA_01.Status);
        }
    }
}

public static class RolesValidationExtensions
{
    public static RouteHandlerBuilder WithRolesValidation(this RouteHandlerBuilder builder, params Role[] acceptedRoles)
    {
        return builder.AddEndpointFilter((context, next) =>
        {
            var currentUserService = context.HttpContext.RequestServices.GetRequiredService<CurrentUserService>();
            return new RolesFilter(currentUserService, acceptedRoles).InvokeAsync(context, next);
        });
    }
}
