using WebApi.Common.Settings;

namespace WebApi.DependencyInjections;

public static class SettingsDI
{
    public static void AddConfigureSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<VnPaySettings>(configuration.GetSection(VnPaySettings.Section));
        services.Configure<MomoSettings>(configuration.GetSection(MomoSettings.Section));
        services.Configure<GoogleStorageSettings>(configuration.GetSection(GoogleStorageSettings.Section));
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.Section));
        services.Configure<MailSettings>(configuration.GetSection(MailSettings.Section));
        services.Configure<PayOSSettings>(configuration.GetSection(PayOSSettings.Section));
        services.Configure<RabbitMQSettings>(configuration.GetSection(RabbitMQSettings.Section));
    }
}
