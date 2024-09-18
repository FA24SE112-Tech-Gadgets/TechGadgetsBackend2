namespace WebApi.Data.Entities;

public class GadgetRequestSpecification
{
    public int Id { get; set; }
    public int GadgetRequestId { get; set; }
    public int? SpecificationDefinitionId { get; set; }
    public string? SpecificationDefinitionName { get; set; }
    public int? SpecificationValueId { get; set; }
    public string? SpecificationValueValue { get; set; }
    public int? SpecificationUnitId { get; set; }

    public GadgetRequest? GadgetRequest { get; set; }
    public SpecificationValue? SpecificationValue { get; set; }
    public SpecificationUnit? SpecificationUnit { get; set; }
    public SpecificationDefinition? SpecificationDefinition { get; set; }
}
