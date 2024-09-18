namespace WebApi.Data.Entities;

public class BusinessModel
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;

    public ICollection<SellerApplication> SellerApplications { get; set; } = [];
    public ICollection<Seller> Sellers { get; set; } = [];
}
