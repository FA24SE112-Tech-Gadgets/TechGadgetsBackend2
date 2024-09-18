namespace WebApi.Data.Entities;

public class GadgetInformation
{
    public int Id { get; set; }
    public int OrderDetailId { get; set; }
    public int GadgetId { get; set; }
    public string GadgetName { get; set; } = default!;
    public int GadgetPrice { get; set; }
    public int? DiscountAmount { get; set; }
    public int GadgetQuantity { get; set; }

    public Gadget? Gadget { get; set; }
    public OrderDetail? OrderDetail { get; set; }
}
