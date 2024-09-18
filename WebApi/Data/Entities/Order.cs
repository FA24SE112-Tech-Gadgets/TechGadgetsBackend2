namespace WebApi.Data.Entities;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public DateTime? PaymentDate { get; set; }
    public DateTime CreatedAt { get; set; }

    public User? User { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; } = [];
}

public enum PaymentMethod
{
    VnPay, Momo, PayOS
}

public enum PaymentStatus
{
    Paid, Unpaid, Cancelled, Failed
}