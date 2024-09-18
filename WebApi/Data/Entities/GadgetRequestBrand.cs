namespace WebApi.Data.Entities;

public class GadgetRequestBrand
{
    public int Id { get; set; }
    public int GadgetRequestId { get; set; }
    public int? BrandId { get; set; }
    public string? BrandName { get; set; }
    public string? LogoUrl { get; set; }

    public GadgetRequest? GadgetRequest { get; set; }
    public Brand? Brand { get; set; }
}
