using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Data.Entities;

namespace WebApi.Services.Background.UserVerifies;

public class UserVerifyStatusCheckService(IServiceProvider serviceProvider) : BackgroundService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                await context.UserVerify
                    .Where(a => a.CreatedAt.AddMinutes(1) < DateTime.UtcNow && a.Status == VerifyStatus.Pending)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(a => a.Status, VerifyStatus.Expired), stoppingToken);
            }

            await Task.Delay(TimeSpan.FromSeconds(60 * 5), stoppingToken);
        }
    }

}
