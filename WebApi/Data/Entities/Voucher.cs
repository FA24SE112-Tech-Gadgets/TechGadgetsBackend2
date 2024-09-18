namespace WebApi.Data.Entities;

public class Voucher
{
    public int Id { get; set; }
    public int SellerId { get; set; }
    public int VoucherTypeId { get; set; }
    public int DiscountTypeId { get; set; }
    public int Value { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public VoucherStatus Status { get; set; }
    public int Quantity { get; set; }
    public int MinAmount { get; set; }
    public int MaxAmount { get; set; }
    public DateTime CreatedAt { get; set; }

    public VoucherType? VoucherType { get; set; }
    public DiscountType? DiscountType { get; set; }
    public Seller? Seller { get; set; }
    public ICollection<VoucherUser> VoucherUsers { get; set; } = [];
}

public enum VoucherStatus
{
    Expired, OutOfStock
}
