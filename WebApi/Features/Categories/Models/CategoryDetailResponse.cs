namespace WebApi.Features.Categories.Models;

public class CategoryDetailResponse
{
    public int Id { get; set; }
    public ICollection<CategoryResponse?> Parent { get; set; } = [];
    public int? ParentId { get; set; }
    public string Name { get; set; } = default!;
    public bool IsAdminCreated { get; set; }
}
