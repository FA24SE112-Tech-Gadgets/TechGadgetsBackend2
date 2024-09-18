namespace WebApi.Data.Entities;

public class BillingMailApplication
{
    public int Id { get; set; }
    public string Mail { get; set; } = default!;
    public int SellerApplicationId { get; set; }

    public SellerApplication? SellerApplication { get; set; }
}
