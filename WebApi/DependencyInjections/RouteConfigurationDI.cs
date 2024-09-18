namespace WebApi.DependencyInjections;

public static class RouteConfigurationDI
{
    public static void AddThrowOnBadRequest(this IServiceCollection services)
    {
        services.Configure<RouteHandlerOptions>(o => o.ThrowOnBadRequest = true);
    }
}
