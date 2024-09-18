using WebApi.Services.Auth;

namespace WebApi.DependencyInjections;

public static class ServicesDI
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<TokenService>();
    }
}
