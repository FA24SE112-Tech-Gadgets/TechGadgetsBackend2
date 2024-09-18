namespace WebApi.Data.Entities;

public class SpecificationDefinition
{
    public int Id { get; set; }
    public int? SellerId { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = default!;

    public Seller? Seller { get; set; }
    public Category? Category { get; set; }
    public ICollection<SpecificationValue> SpecificationValues { get; set; } = [];
    public ICollection<GadgetRequestSpecification> GadgetRequestSpecifications { get; set; } = [];
}
