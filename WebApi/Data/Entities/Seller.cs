namespace WebApi.Data.Entities;

public class Seller
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string CompanyName { get; set; } = default!;
    public string ShopName { get; set; } = default!;
    public string ShippingAddress { get; set; } = default!;
    public string ShopAddress { get; set; } = default!;
    public BusinessModel BusinessModel { get; set; }
    public string BusinessRegistrationCertificateUrl { get; set; } = default!;
    public string TaxCode { get; set; } = default!;
    public SellerStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User? User { get; set; }
    public SellerPage? SellerPage { get; set; }
    public ICollection<Gadget> Gadgets { get; set; } = [];
    public ICollection<Voucher> Vouchers { get; set; } = [];
    public ICollection<BillingMail> BillingMails { get; set; } = [];
    public ICollection<GadgetRequest> GadgetRequests { get; set; } = [];
    public ICollection<SpecificationDefinition> SpecificationDefinitions { get; set; } = [];
    public ICollection<SpecificationValue> SpecificationValues { get; set; } = [];
}

public enum SellerStatus
{
    Active, Inactive
}

public enum BusinessModel
{
    Personal, BusinessHousehold, Company
}