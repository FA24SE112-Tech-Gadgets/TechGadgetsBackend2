namespace WebApi.Data.Entities;

public class Category
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string Name { get; set; } = default!;
    public bool IsAdminCreated { get; set; }

    public Category? Parent { get; set; }
    public ICollection<Category> Children { get; set; } = [];
    public ICollection<Gadget> Gadgets { get; set; } = [];
    public ICollection<GadgetRequestCategory> GadgetRequestCategories { get; set; } = [];
    public ICollection<BrandCategory> BrandCategories { get; set; } = [];
    public ICollection<SpecificationDefinition> SpecificationDefinitions { get; set; } = [];
}
