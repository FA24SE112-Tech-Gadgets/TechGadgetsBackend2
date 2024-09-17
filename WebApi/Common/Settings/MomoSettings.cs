namespace WebApi.Common.Settings;

public class MomoSettings
{
    public static readonly string Section = "Payment:Momo";

    public string PartnerCode { get; set; } = default!;

    public string AccessKey { get; set; } = default!;

    public string SecretKey { get; set; } = default!;

    public string PaymentEndpoint { get; set; } = default!;

    public string IpnUrl { get; set; } = default!;

    public string RedirectUrl { get; set; } = default!;
}
