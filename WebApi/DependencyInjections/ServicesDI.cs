using WebApi.Services.Auth;
using WebApi.Services.Mail;
using WebApi.Services.VerifyCode;

namespace WebApi.DependencyInjections;

public static class ServicesDI
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<TokenService>();
        services.AddScoped<MailService>();
        services.AddScoped<VerifyCodeService>();
    }
}
