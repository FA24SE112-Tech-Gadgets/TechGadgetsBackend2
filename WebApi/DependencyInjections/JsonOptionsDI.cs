using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;

namespace WebApi.DependencyInjections;

public static class JsonOptionsDI
{
    public static void AddJsonOptions(this IServiceCollection services)
    {
        services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
    }
}
