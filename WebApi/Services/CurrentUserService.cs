﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebApi.Common.Exceptions;
using WebApi.Common.Settings;
using WebApi.Data.Entities;

namespace WebApi.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor, IOptions<JwtSettings> jwtSettings)
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;
    private readonly SymmetricSecurityKey _key = new(Encoding.UTF8.GetBytes(jwtSettings.Value.SigningKey));

    public User GetCurrentUser()
    {
        var request = httpContextAccessor.HttpContext?.Request;
        var authHeader = request?.Headers.Authorization.ToString();
        var token = authHeader?.Replace("Bearer ", string.Empty);

        if (string.IsNullOrEmpty(token))
        {
            throw TechGadgetException.NewBuilder()
                    .WithCode(TechGadgetErrorCode.WEA_0000)
                    .AddReason("Lỗi xác thực", "Token không tồn tại")
                    .Build();
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = _key,
            ValidIssuer = _jwtSettings.Issuer,
            ValidAudience = _jwtSettings.Audience,
            ClockSkew = TimeSpan.Zero
        };

        var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

        var userInfoJson = principal.Claims.FirstOrDefault(c => c.Type == "UserInfo")?.Value;

        if (string.IsNullOrEmpty(userInfoJson))
            throw TechGadgetException.NewBuilder()
            .WithCode(TechGadgetErrorCode.WEA_0000)
            .AddReason("Lỗi xác thực", "Không có thông tin người dùng trong mã Token.")
            .Build();

        var checkClaim = principal.Claims.FirstOrDefault(c => c.Type == "TokenClaim" && c.Value == "ForVerifyOnly")?.Value;

        if (string.IsNullOrEmpty(checkClaim))
            throw TechGadgetException.NewBuilder()
            .WithCode(TechGadgetErrorCode.WEA_0000)
            .AddReason("Lỗi xác thực", "Thiếu thông tin xác thực trong mã Token.")
            .Build();


    }
}
