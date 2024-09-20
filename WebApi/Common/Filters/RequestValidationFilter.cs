using FluentValidation;
using WebApi.Common.Exceptions;

namespace WebApi.Common.Filters;

public class RequestValidationFilter<TRequest>(IValidator<TRequest>? validator) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        if (validator is null)
        {
            return await next(context);
        }

        var request = context.Arguments.OfType<TRequest>().First();

        var result = await validator.ValidateAsync(request, context.HttpContext.RequestAborted);

        if (!result.IsValid)
        {
            var errorResponse = new TechGadgetErrorResponse
            {
                Code = TechGadgetErrorCode.WEV_00.Code,
                Title = TechGadgetErrorCode.WEV_00.Title,
                Reasons = result.Errors.Select(err => new Reason(err.PropertyName, err.ErrorMessage)).ToList()
            };
            return Results.Json(errorResponse, statusCode: (int)TechGadgetErrorCode.WEV_00.Status);
        }

        return await next(context);
    }
}

public static class RequestValidationExtensions
{
    public static RouteHandlerBuilder WithRequestValidation<TRequest>(this RouteHandlerBuilder builder)
    {
        return builder.AddEndpointFilter<RequestValidationFilter<TRequest>>();
    }
}
