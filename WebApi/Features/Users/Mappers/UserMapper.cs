using WebApi.Data.Entities;
using WebApi.Features.Users.Models;

namespace WebApi.Features.Users.Mappers;

public static class UserMapper
{
    public static UserResponse? ToUserResponse(this User? user)
    {
        if (user != null)
        {
            return new UserResponse
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
                LoginMethod = user.LoginMethod
            };
        }
        return null;
    }
}
