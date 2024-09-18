namespace WebApi.Data.Entities;

public class BrandCategory
{
    public int CategoryId { get; set; }
    public int BrandId { get; set; }

    public Category? Category { get; set; }
    public Brand? Brand { get; set; }
}
