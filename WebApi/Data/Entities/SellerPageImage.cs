namespace WebApi.Data.Entities;

public class SellerPageImage
{
    public int Id { get; set; }
    public int SellerPageId { get; set; }
    public string ImageUrl { get; set; } = default!;

    public SellerPage? SellerPage { get; set; }
}
