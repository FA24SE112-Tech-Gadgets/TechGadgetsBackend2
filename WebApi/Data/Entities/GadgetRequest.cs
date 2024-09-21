namespace WebApi.Data.Entities;

public class GadgetRequest
{
    public int Id { get; set; }
    public int SellerId { get; set; }
    public string GadgetName { get; set; } = default!;
    public int Price { get; set; }
    public GadgetRequestStatus Status { get; set; }
    public GadgetRequestType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Seller? Seller { get; set; }
    public GadgetRequestBrand? GadgetRequestBrand { get; set; }
    public ICollection<GadgetRequestCategory> GadgetRequestCategories { get; set; } = [];
    public ICollection<GadgetRequestSpecification> GadgetRequestSpecifications { get; set; } = [];
}

public enum GadgetRequestStatus
{
    Approved, Rejected, Pending
}

public enum GadgetRequestType
{
    Create, Update
}