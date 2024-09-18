namespace WebApi.Data.Entities;

public class GadgetRequestCategory
{
    public int Id { get; set; }
    public int GadgetRequestId { get; set; }
    public int? CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public int? CategoryLevel;

    public GadgetRequest? GadgetRequest { get; set; }
    public Category? Category { get; set; }
}
