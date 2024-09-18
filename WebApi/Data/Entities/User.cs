namespace WebApi.Data.Entities;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string AvatarUrl { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string CCCD { get; set; } = default!;
    public string Gender { get; set; } = default!;
    public DateOnly DateOfBirth { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public Role Role { get; set; }
    public string Email { get; set; } = default!;
    public UserStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Cart? Cart { get; set; }
    public Seller? Seller { get; set; }
    public ICollection<Notification> Notifications { get; set; } = [];
    public ICollection<KeywordHistory> KeywordHistories { get; set; } = [];
    public ICollection<FavoriteGadget> FavoriteGadgets { get; set; } = [];
    public ICollection<SearchHistory> SearchHistories { get; set; } = [];
    public ICollection<Order> Orders { get; set; } = [];
    public ICollection<SellerApplication> SellerApplications { get; set; } = [];
    public ICollection<VoucherUser> VoucherUsers { get; set; } = [];
}

public enum Role
{
    Admin, Buyer, Seller
}

public enum UserStatus
{
    Active, Inactive, Pending
}