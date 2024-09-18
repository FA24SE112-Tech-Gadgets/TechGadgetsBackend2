namespace WebApi.Data.Entities;

public class BillingMail
{
    public int Id { get; set; }
    public string Mail { get; set; } = default!;
    public int SellerId { get; set; }

    public Seller? Seller { get; set; }
}
