using WebApi.Data.Entities;
using WebApi.Features.Auth.Models;
using WebApi.Services.Auth.Models;

namespace WebApi.Features.Auth.Mappers;

public static class GoogleLoginMapper
{
    public static User ToUserRequest(this RegisterUserRequest? registerUserRequest)
    {
        if (registerUserRequest == null)
        {
            return null;
        }
        return new User
        {
            FullName = registerUserRequest.FullName,
            Email = registerUserRequest.Email,
            AvatarUrl = registerUserRequest.AvatarUrl,
            Role = registerUserRequest.Role,
            LoginMethod = registerUserRequest.LoginMethod,
            Status = registerUserRequest.Status,
            CreatedAt = registerUserRequest.CreatedAt,
            UpdatedAt = registerUserRequest.UpdatedAt,
        };
    }
}
