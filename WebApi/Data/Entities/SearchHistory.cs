namespace WebApi.Data.Entities;

public class SearchHistory
{
    public int Id { get; set; }
    public int GadgetId { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }

    public User? User { get; set; }
    public Gadget? Gadget { get; set; }
}
