using WebApi.Services.Background.UserVerifies;

namespace WebApi.DependencyInjections;

public static class BackgroundServicesDI
{
    public static void AddBackgroundServices(this IServiceCollection services)
    {
        services.AddHostedService<UserVerifyStatusCheckService>();
    }
}
