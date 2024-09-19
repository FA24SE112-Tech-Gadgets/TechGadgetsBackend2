using WebApi.Data.Entities;

namespace WebApi.Services.Auth.Models;

public record TokenRequest
{
    public int Id { get; set; }
    public string FullName { get; set; } = default!;
    public string? AvatarUrl { get; set; } = default!;
    public string? Address { get; set; } = default!;
    public string? CCCD { get; set; } = default!;
    public string? Gender { get; set; } = default!;
    public DateOnly? DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; } = default!;
    public LoginMethod LoginMethod { get; set; }
    public Role Role { get; set; }
    public string Email { get; set; } = default!;
    public UserStatus Status { get; set; }
}
