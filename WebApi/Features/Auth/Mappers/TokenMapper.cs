using WebApi.Data.Entities;
using WebApi.Services.Auth.Models;

namespace WebApi.Features.Auth.Mappers;

public static class TokenMapper
{
    public static TokenRequest? ToTokenRequest(this User? user)
    {
        if (user != null)
        {
            return new TokenRequest
            {
                AvatarUrl = user.AvatarUrl,
                Email = user.Email,
                FullName = user.FullName,
                Id = user.Id,
                Role = user.Role,
                Status = user.Status,
            };
        }
        return null;
    }

    public static User? ToUser(this TokenRequest? tokenRequest)
    {
        if (tokenRequest == null)
        {
            return null;
        }
        return new User
        {
            AvatarUrl = tokenRequest.AvatarUrl,
            Email = tokenRequest.Email,
            FullName = tokenRequest.FullName,
            Id = tokenRequest.Id,
            Role = tokenRequest.Role,
            Status = tokenRequest.Status,
        };
    }
}
