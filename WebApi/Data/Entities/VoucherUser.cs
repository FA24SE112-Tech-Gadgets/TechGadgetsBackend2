namespace WebApi.Data.Entities;

public class VoucherUser
{
    public int VoucherId { get; set; }
    public int UserId { get; set; }
    public VoucherUserStatus Status { get; set; }

    public Voucher? Voucher { get; set; }
    public User? User { get; set; }
}

public enum VoucherUserStatus
{
    Expired, OutOfStock, Used
}
