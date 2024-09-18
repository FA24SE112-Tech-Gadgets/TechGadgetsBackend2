namespace WebApi.Data.Entities;

public class CartGadget
{
    public int CartId { get; set; }
    public int GadgetId { get; set; }
    public int Quantity { get; set; }

    public Cart? Cart { get; set; }
    public Gadget? Gadget { get; set; }
}
