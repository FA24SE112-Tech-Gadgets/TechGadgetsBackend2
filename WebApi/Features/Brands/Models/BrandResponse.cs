namespace WebApi.Features.Brands.Models;

public class BrandResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string LogoUrl { get; set; } = default!;
}
