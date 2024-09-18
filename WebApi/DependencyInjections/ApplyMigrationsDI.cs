using Microsoft.EntityFrameworkCore;
using WebApi.Data;

namespace WebApi.DependencyInjections;

public static class ApplyMigrationsDI
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<AppDbContext>();
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }
}
