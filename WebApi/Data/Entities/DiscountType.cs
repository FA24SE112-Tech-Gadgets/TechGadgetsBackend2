namespace WebApi.Data.Entities;

public class DiscountType
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;

    public ICollection<Voucher> Vouchers { get; set; } = [];
}
