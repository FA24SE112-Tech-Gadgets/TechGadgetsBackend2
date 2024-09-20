using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebApi.Common.Exceptions;
using WebApi.Common.Settings;

namespace WebApi.Common.Filters;

public class JwtValidationFilter(IOptions<JwtSettings> jwtSettings) : IEndpointFilter
{
    private const string AuthorizationHeader = "Authorization";
    private const string BearerPrefix = "Bearer ";
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;
    private readonly SymmetricSecurityKey _key = new(Encoding.UTF8.GetBytes(jwtSettings.Value.SigningKey));

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {

        // Extract JWT from Authorization header
        if (!context.HttpContext.Request.Headers.TryGetValue(AuthorizationHeader, out var authHeader) ||
            !authHeader.ToString().StartsWith(BearerPrefix))
        {
            var reason = new Reason("Thiếu mã Token", "Thiếu mã Token");
            var reasons = new List<Reason> { reason };
            var errorResponse = new TechGadgetErrorResponse
            {
                Code = TechGadgetErrorCode.WEA_0000.Code,
                Title = TechGadgetErrorCode.WEA_0000.Title,
                Reasons = reasons
            };
            return Results.Json(errorResponse, statusCode: (int)TechGadgetErrorCode.WEA_0000.Status);
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
        string token;
        try
        {
            token = context.HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            var reason = new Reason("Lỗi xác thực", "Mã Token không hợp lệ.");
            var reasons = new List<Reason> { reason };
            var errorResponse = new TechGadgetErrorResponse
            {
                Code = TechGadgetErrorCode.WEA_0000.Code,
                Title = TechGadgetErrorCode.WEA_0000.Title,
                Reasons = reasons
            };
            return Results.Json(errorResponse, statusCode: (int)TechGadgetErrorCode.WEA_0000.Status);
        }
        var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

        // Extract the UserInfo claim
        var userInfoJson = principal.Claims.FirstOrDefault(c => c.Type == "UserInfo")?.Value;

        if (string.IsNullOrEmpty(userInfoJson))
        {
            var reason = new Reason("Lỗi xác thực", "Không có thông tin người dùng trong mã Token.");
            var reasons = new List<Reason> { reason };
            var errorResponse = new TechGadgetErrorResponse
            {
                Code = TechGadgetErrorCode.WEA_0000.Code,
                Title = TechGadgetErrorCode.WEA_0000.Title,
                Reasons = reasons
            };
            return Results.Json(errorResponse, statusCode: (int)TechGadgetErrorCode.WEA_0000.Status);
        }

        var checkClaim = principal.Claims.FirstOrDefault(c => c.Type == "TokenClaim" && c.Value == "ForVerifyOnly")?.Value;

        if (string.IsNullOrEmpty(checkClaim))
        {
            var reason = new Reason("Lỗi xác thực", "Thiếu thông tin xác thực trong mã Token.");
            var reasons = new List<Reason> { reason };
            var errorResponse = new TechGadgetErrorResponse
            {
                Code = TechGadgetErrorCode.WEA_0000.Code,
                Title = TechGadgetErrorCode.WEA_0000.Title,
                Reasons = reasons
            };
            return Results.Json(errorResponse, statusCode: (int)TechGadgetErrorCode.WEA_0000.Status);
        }

        return await next(context);
    }
}

public static class JwtValidationExtensions
{
    public static RouteHandlerBuilder WithJwtValidation(this RouteHandlerBuilder builder)
    {
        return builder.AddEndpointFilter<JwtValidationFilter>();
    }
}
