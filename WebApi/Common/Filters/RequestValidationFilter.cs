using FluentValidation;

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
            return TypedResults.ValidationProblem(result.ToDictionary());
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
