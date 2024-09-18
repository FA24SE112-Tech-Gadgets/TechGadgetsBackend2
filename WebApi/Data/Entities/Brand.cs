namespace WebApi.Data.Entities;

public class Brand
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string LogoUrl { get; set; } = default!;

    public ICollection<BrandCategory> BrandCategories { get; set; } = [];
    public ICollection<Gadget> Gadgets { get; set; } = [];
    public ICollection<GadgetRequestBrand> GadgetsRequestBrands { get; set; } = [];
}
