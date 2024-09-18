namespace WebApi.Data.Entities;

public class FavoriteGadget
{
    public int UserId { get; set; }
    public int GadgetId { get; set; }

    public User? User { get; set; }
    public Gadget? Gadget { get; set; }
}
