namespace WebApi.Data.Entities;

public class SpecificationUnit
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;

    public ICollection<GadgetRequestSpecification> GadgetRequestSpecifications { get; set; } = [];
    public ICollection<SpecificationValue> SpecificationValues { get; set; } = [];
}
