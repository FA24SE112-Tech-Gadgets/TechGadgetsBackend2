using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Common.Exceptions;
using WebApi.Common.Settings;
using WebApi.Services.Auth.Models;


namespace WebApi.Services.Auth;

public class TokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly SymmetricSecurityKey _key;

    public TokenService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Value.SigningKey));
    }

    public string CreateToken(TokenRequest tokenRequest)
    {
        // Serialize customUserInfo to JSON
        var userInfoJson = JsonConvert.SerializeObject(tokenRequest);

        // Create the JWT payload
        var claims = new List<Claim>
        {
            new Claim("UserInfo", userInfoJson),
            new Claim("TokenClaim", "ForVerifyOnly")
        };

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(30),
            SigningCredentials = creds,
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string CreateRefreshToken(TokenRequest tokenRequest)
    {
        // Serialize customUserInfo to JSON
        var userInfoJson = JsonConvert.SerializeObject(tokenRequest);

        // Create the JWT payload
        var claims = new List<Claim>
        {
            new Claim("UserInfo", userInfoJson),
            new Claim("RFTokenClaim", "ForVerifyOnly")
        };

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds,
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public bool ValidateToken(HttpRequest request)
    {
        var token = request?.Headers.Authorization.ToString().Replace("Bearer ", "");
        if (string.IsNullOrEmpty(token))
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEA_0000)
                .AddReason("Lỗi xác thực", "Thiếu mã Token")
                .Build();
        }
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _key,
            ValidateIssuer = true,
            ValidIssuer = _jwtSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = _jwtSettings.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

            // Extract the UserInfo claim
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

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEA_0000)
                .AddReason("Lỗi xác thực", "Mã Token không hợp lệ.")
                .Build();
        }
    }

    public bool ValidateRefreshToken(HttpRequest request)
    {
        var token = request?.Headers.Authorization.ToString().Replace("Bearer ", "");
        if (string.IsNullOrEmpty(token))
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEA_0000)
                .AddReason("Lỗi xác thực", "Thiếu mã Token")
                .Build();
        }
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _key,
            ValidateIssuer = true,
            ValidIssuer = _jwtSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = _jwtSettings.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

            // Extract the UserInfo claim
            var accountInfoJson = principal.Claims.FirstOrDefault(c => c.Type == "UserInfo")?.Value;

            if (string.IsNullOrEmpty(accountInfoJson))
                throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEA_0000)
                .AddReason("Lỗi xác thực", "Không có thông tin người dùng trong mã Token.")
                .Build();

            var checkClaim = principal.Claims.FirstOrDefault(c => c.Type == "RFTokenClaim" && c.Value == "ForVerifyOnly")?.Value;

            if (string.IsNullOrEmpty(checkClaim))
                throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEA_0000)
                .AddReason("Lỗi xác thực", "Thiếu thông tin xác thực trong mã Token.")
                .Build();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEA_0000)
                .AddReason("Lỗi xác thực", "Mã Token không hợp lệ.")
                .Build();
        }
    }
}
