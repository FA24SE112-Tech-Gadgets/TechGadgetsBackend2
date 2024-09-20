namespace WebApi.Services.Payment.Models;

public class VnPayPayment
{
    public string PaymentReferenceId { get; set; } = default!;

    public long Amount { get; set; }

    public string? Info { get; set; }

    public DateTimeOffset Time { get; set; }

    public string returnUrl { get; set; } = default!;
}
