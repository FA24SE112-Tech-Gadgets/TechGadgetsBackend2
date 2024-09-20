namespace WebApi.Data.Entities;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = default!;
    public string? Password { get; set; }
    public string? AvatarUrl { get; set; }
    public string? Address { get; set; }
    public string? CCCD { get; set; }
    public string? Gender { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; } = default!;
    public Role Role { get; set; }
    public string Email { get; set; } = default!;
    public UserStatus Status { get; set; }
    public LoginMethod LoginMethod { get; set; }
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
    public ICollection<UserVerify> UserVerify { get; set; } = [];
}

public enum LoginMethod
{
    Google, Default
}

public enum Role
{
    Admin, Buyer
}

public enum UserStatus
{
    Active, Inactive, Pending
}