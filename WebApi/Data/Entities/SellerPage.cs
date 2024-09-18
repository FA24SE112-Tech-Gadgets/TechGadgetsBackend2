namespace WebApi.Data.Entities;

public class SellerPage
{
    public int Id { get; set; }
    public int SellerId { get; set; }
    public string BannerUrl { get; set; } = default!;

    public Seller? Seller { get; set; }
    public ICollection<SellerPageImage> Images { get; set; } = [];
}
