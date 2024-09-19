using WebApi.Data.Entities;

namespace WebApi.Features.Auth.Models;

public class RegisterUserRequest
{
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? AvatarUrl { get; set; }
    public Role Role { get; set; }
    public LoginMethod LoginMethod { get; set; }
    public UserStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
