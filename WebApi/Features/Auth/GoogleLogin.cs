using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using WebApi.Common.Endpoints;
using WebApi.Common.Exceptions;
using WebApi.Data;
using WebApi.Data.Entities;
using WebApi.Features.Auth.Models;
using WebApi.Services.Auth;

namespace WebApi.Features.Auth;

public class GoogleLogin
{
    private class RegisterUserRequest
    {
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? AvatarUrl { get; set; }
        public Role Role { get; set; }
        public LoginMethod LoginMethod { get; set; }
        public UserStatus Status { get; set; }
    }

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
                .FirstOrDefaultAsync(); ;
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
                    user = await _accountService.RegisterAccountAsync(registerUserRequest, AccountLoginMethod.GOOGLE);
                    return new TokenResponse
                    {
                        Token = _tokenService.CreateToken(user),
                        RefreshToken = _tokenService.CreateRefreshToken(user)
                    };
                }

                return new TokenResponse
                {
                    Token = _tokenService.CreateToken(user),
                    RefreshToken = _tokenService.CreateRefreshToken(user)
                };
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
        catch (Exception ex)
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEA_0000)
                .AddReason("Ngoại lệ trái phép của Google", ex.Message)
                .Build();
        }
    }
}
