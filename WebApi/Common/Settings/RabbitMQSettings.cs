namespace WebApi.Common.Settings;

public class RabbitMQSettings
{
    public static readonly string Section = "RabbitMQ";

    public string Host { get; set; } = default!;

    public string UserName { get; set; } = default!;

    public string Password { get; set; } = default!;
}
