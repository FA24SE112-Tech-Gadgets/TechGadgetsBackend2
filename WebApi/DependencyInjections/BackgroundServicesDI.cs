using WebApi.Services.Background.UserVerifyBG;

namespace WebApi.DependencyInjections;

public static class BackgroundServicesDI
{
    public static void AddBackgroundServices(this IServiceCollection services)
    {
        services.AddHostedService<UserVerifyStatusCheckService>();
    }
}
