namespace WebApi.Data.Entities;

public class SellerApplication
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? CompanyName { get; set; }
    public string ShopName { get; set; } = default!;
    public string ShippingAddress { get; set; } = default!;
    public string ShopAddress { get; set; } = default!;
    public BusinessModel BusinessModel { get; set; }
    public string? BusinessRegistrationCertificateUrl { get; set; }
    public string TaxCode { get; set; } = default!;
    public string RejectReason { get; set; } = default!;
    public SellerApplicationStatus Status { get; set; }
    public SellerApplicationType Type { get; set; }
    public DateTime CreatedAt { get; set; }

    public User? User { get; set; }
    public ICollection<BillingMailApplication> BillingMailApplications { get; set; } = [];
}

public enum SellerApplicationStatus
{
    Approved, Rejected, Pending
}

public enum SellerApplicationType
{
    Create, Update
}