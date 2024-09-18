namespace WebApi.Features.Auth.Models;

public record TokenResponse(string Token, string RefreshToken);
