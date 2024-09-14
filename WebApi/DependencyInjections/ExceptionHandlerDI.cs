using WebApi.Common.Exceptions;

namespace WebApi.DependencyInjections;

public static class ExceptionHandlerDI
{
    public static void UseTechGadgetsExceptionHandler(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<TechGadgetExceptionHandler>();
    }
}
