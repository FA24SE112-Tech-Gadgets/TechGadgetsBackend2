namespace WebApi.Features.Categories.Models;

public class CategoryResponse
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string Name { get; set; } = default!;
    public bool IsAdminCreated { get; set; }
}
