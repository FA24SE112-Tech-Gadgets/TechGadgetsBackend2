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
                Address = user.Address,
                AvatarUrl = user.AvatarUrl,
                CCCD = user.CCCD,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                FullName = user.FullName,
                Gender = user.Gender,
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                Status = user.Status,
            };
        }
        return null;
    }
}
