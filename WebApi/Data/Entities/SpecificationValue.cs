namespace WebApi.Data.Entities;

public class SpecificationValue
{
    public int Id { get; set; }
    public int SpecificationDefinitionId { get; set; }
    public int? SellerId { get; set; }
    public string Value { get; set; } = default!;
    public int SpecificationUnitId { get; set; }

    public SpecificationDefinition? SpecificationDefinition { get; set; }
    public Seller? Seller { get; set; }
    public SpecificationUnit? SpecificationUnit { get; set; }
    public ICollection<GadgetRequestSpecification> GadgetRequestSpecifications { get; set; } = [];
}
