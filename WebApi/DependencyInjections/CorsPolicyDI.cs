namespace WebApi.DependencyInjections;

public static class CorsPolicyDI
{
    public static void AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });
    }

    public static void UseCorsPolicy(this IApplicationBuilder app)
        => app.UseCors();
}
