using WebApi.Data.Entities;

namespace WebApi.Features.Users.Models;

public class UserResponse
{
    public int Id { get; set; }
    public string FullName { get; set; } = default!;
    public string? AvatarUrl { get; set; }
    public string? Address { get; set; }
    public string? CCCD { get; set; }
    public string? Gender { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    public LoginMethod LoginMethod { get; set; }
    public Role Role { get; set; }
    public string Email { get; set; } = default!;
    public UserStatus Status { get; set; }
}
