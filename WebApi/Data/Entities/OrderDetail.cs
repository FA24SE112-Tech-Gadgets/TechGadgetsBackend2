namespace WebApi.Data.Entities;

public class OrderDetail
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string? Reason { get; set; }
    public int? DiscountAmount { get; set; }
    public OrderDetailStatus Status { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Order? Order { get; set; }
    public ICollection<GadgetInformation> GadgetInformation { get; set; } = [];
}

public enum OrderDetailStatus
{
    Success, Failed
}
