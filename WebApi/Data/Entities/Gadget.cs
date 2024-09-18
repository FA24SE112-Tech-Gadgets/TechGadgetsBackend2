namespace WebApi.Data.Entities;

public class Gadget
{
    public int Id { get; set; }
    public int SellerId { get; set; }
    public string Name { get; set; } = default!;
    public int Price { get; set; }
    public int BrandId { get; set; }
    public string ThumbnailUrl { get; set; } = default!;
    public int CategoryId { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; } = default!;
    public GadgetStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Seller? Seller { get; set; }
    public Category? Category { get; set; }
    public Brand? Brand { get; set; }
    public ICollection<FavoriteGadget> FavoriteGadgets { get; set; } = [];
    public ICollection<SearchHistory> SearchHistories { get; set; } = [];
    public ICollection<GadgetImage> GadgetImages { get; set; } = [];
    public ICollection<CartGadget> CartGadgets { get; set; } = [];
    public ICollection<GadgetInformation> GadgetInformation { get; set; } = [];
}

public enum GadgetStatus
{
    Active, Inactive
}