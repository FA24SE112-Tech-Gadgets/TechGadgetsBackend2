using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using WebApi.Common.Endpoints;
using WebApi.Common.Exceptions;
using WebApi.Data;
using WebApi.Data.Entities;
using WebApi.Features.Auth.Mappers;
using WebApi.Features.Auth.Models;
using WebApi.Services.Auth;

namespace WebApi.Features.Auth;

public class GoogleLogin
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("auth/google/{accessToken}", Handler)
            .WithTags("Auths")
                .WithDescription("This API is for user login with Google")
            .WithSummary("Google login user")
            .Produces<TokenResponse>(StatusCodes.Status200OK);
        }
    }

    public static async Task<IResult> Handler([FromRoute] string accessToken, AppDbContext context, [FromServices] TokenService tokenService)
    {
        try
        {
            using (var client = new HttpClient())
            {
                // Set the Authorization header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Make the GET request
                var response = await client.GetAsync($"https://www.googleapis.com/oauth2/v1/userinfo?access_token={accessToken}");

                // Ensure the request was successful
                response.EnsureSuccessStatusCode();

                // Read the response content as a string
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseContent);

                var ggResponse = JsonConvert.DeserializeObject<LoginGoogleResponse>(responseContent);
                var user = await context.Users
                    .Where(a => a.Email == ggResponse.Email)
                    .FirstOrDefaultAsync();

                //TH Mail chưa tồn tại
                if (user == null)
                {
                    var registerUserRequest = new RegisterUserRequest
                    {
                        Email = ggResponse.Email,
                        Role = Role.Buyer,
                        FullName = ggResponse.Name,
                        AvatarUrl = ggResponse.Picture,
                        LoginMethod = LoginMethod.Google,
                        Status = UserStatus.Active
                    };
                    context.Users.Add(registerUserRequest.ToUserRequest());
                    await context.SaveChangesAsync();

                    user = await context.Users
                        .Where(a => a.Email == ggResponse.Email)
                        .FirstOrDefaultAsync();

                    var tokenInfo = user.ToTokenRequest();
                    string token = tokenService.CreateToken(tokenInfo!);
                    string rfToken = tokenService.CreateRefreshToken(tokenInfo!);
                    return Results.Ok(new TokenResponse
                    {
                        Token = token,
                        RefreshToken = rfToken
                    });
                }

                //TH đăng nhập bằng phương thức GG
                user = await context.Users
                    .Where(u => u.Email == ggResponse.Email && u.LoginMethod == LoginMethod.Google)
                    .FirstOrDefaultAsync();
                if (user != null)
                {
                    var tokenInfo = user.ToTokenRequest();
                    string token = tokenService.CreateToken(tokenInfo!);
                    string rfToken = tokenService.CreateRefreshToken(tokenInfo!);
                    return Results.Ok(new TokenResponse
                    {
                        Token = token,
                        RefreshToken = rfToken
                    });
                }

                //TH mail này LoginMethod.Default
                user = await context.Users
                    .Where(u => u.Email == ggResponse.Email && u.LoginMethod == LoginMethod.Default)
                    .FirstOrDefaultAsync();
                if (user != null)
                {
                    throw TechGadgetException.NewBuilder()
                        .WithCode(TechGadgetErrorCode.WEA_0000)
                        .AddReason("Sai phương thức đăng nhập", "Người dùng này đã đăng nhập bình thường")
                        .Build();
                }

                throw TechGadgetException.NewBuilder()
                        .WithCode(TechGadgetErrorCode.WEB_0000)
                        .AddReason("Lỗi lạ không xác định", "Lỗi lạ không xác định")
                        .Build();
            }
        }
        catch (HttpRequestException ex)
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEA_0000)
                .AddReason("Google HTTP không được phép", ex.Message)
                .Build();
        }
        catch (JsonException ex)
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEA_0000)
                .AddReason("JSON trái phép của Google", ex.Message)
                .Build();
        }
    }
}
