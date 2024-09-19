namespace WebApi.DependencyInjections;

public static class ApplyExpceptionPage
{
    public static void UseExceptionPageInLocal(this IApplicationBuilder app)
    {
        var env = app.ApplicationServices.GetRequiredService<IHostEnvironment>();

        if (env.EnvironmentName == "Local")
        {
            app.UseDeveloperExceptionPage();
        }
    }
}
