using WebApi.Data.Entities;
using WebApi.Features.Auth.Models;

namespace WebApi.Features.Auth.Mappers;

public class TokenMapper
{
    public static TokenCreateRequest MapToTokenCreateRequest(User user)
    {
        if (user == null)
        {
            return null;
        }
        return new TokenCreateRequest
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
            Username = user.Username
        };
    }
}
