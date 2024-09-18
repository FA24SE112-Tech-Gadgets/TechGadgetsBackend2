namespace WebApi.Data.Entities;

public class GadgetImage
{
    public int Id { get; set; }
    public int GadgetId { get; set; }
    public string ImageUrl { get; set; } = default!;

    public Gadget? Gadget { get; set; }
}
