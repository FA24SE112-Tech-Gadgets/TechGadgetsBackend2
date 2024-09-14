namespace WebApi.DependencyInjections;

public static class DbConnectionDI
{
    public static void AddDbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<AppDbContext>(options =>
        //{
        //    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        //});
    }
}
