using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;

namespace WebApi.DependencyInjections;

public static class JsonOptionsDI
{
    public static void AddJsonOptions(this IServiceCollection services)
    {
        services.Configure<JsonOptions>(o => o.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
    }
}
