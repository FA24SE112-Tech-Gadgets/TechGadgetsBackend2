using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using WebApi.Common.Endpoints;
using WebApi.Common.Exceptions;
using WebApi.Data;
using WebApi.Data.Entities;
using WebApi.Features.Auth.Mappers;
using WebApi.Features.Auth.Models;
using WebApi.Services.Auth;

namespace WebApi.Features.Auth;

public class SignupUser
{
    public record Request(string FullName, string Password, string Email);
    private const int SaltSize = 16; // 128 bit 
    private const int KeySize = 32;  // 256 bit
    private const int Iterations = 10000; // Number of PBKDF2 iterations

    public sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(r => r.FullName).NotEmpty();
            RuleFor(r => r.Password).NotEmpty().MinimumLength(8);
            RuleFor(r => r.Email).NotEmpty().EmailAddress();
        }
    }

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("auth/signup", Handler)
            .WithTags("Auths")
                .WithDescription("This API is for user signup")
            .WithSummary("Signup user")
            .Produces<TokenResponse>(StatusCodes.Status200OK);
        }
    }

    public static async Task<IResult> Handler([FromBody] Request request, AppDbContext context, IValidator<Request> validator, [FromServices] TokenService tokenService)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }

        bool isExist = await context.Users.AnyAsync(user => user.Email == request.Email);
        if (isExist)
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WES_0000)
                .AddReason("Lỗi đăng ký tài khoản", "Email đã tồn tại")
                .Build();
        }
        var tokenInfo = TokenMapper.MapToTokenCreateRequest(user);
        string token = tokenService.CreateToken(tokenInfo);
        string rfToken = tokenService.CreateRefreshToken(tokenInfo);
        return Results.Ok(new TokenResponse(token, rfToken));
    }

    private async Task<User> RegisterAccountAsync(Request userRequest, AccountLoginMethod accountLoginMethod)
    {
        var account = new AccountDto
        {
            Name = registerAccountRequest.Username,
            Role = registerAccountRequest.Role,
            LoginMethod = accountLoginMethod.ToString(),
        };

        if (accountLoginMethod == AccountLoginMethod.NORMAL)
        {
            account.Status = AccountStatus.PENDING.ToString();
            account.Password = HashPassword(registerAccountRequest.Password);
        }
        else
        {
            account.Status = AccountStatus.ACTIVE.ToString();
        }

        var accountMapper = _mapper.Map<Entities.Account.Account>(account);

        if (await _accountRepository.AccountExistsByEmailAndStatus(account.Name, AccountStatus.PENDING))
        {
            throw ProjectException.NewBuilder()
                .WithCode(ProjectErrorCode.WEV_0008)
                .AddReason("account", "Account exists but not verified")
                .Build();
        }

        var check = await _accountRepository.GetAccountByName(account.Name);
        if (check != null)
        {
            throw ProjectException.NewBuilder()
                .WithCode(ProjectErrorCode.WEV_0007)
                .AddReason("email", "This email has been previously registered.")
                .Build();
        }

        var createdAccount = await _accountRepository.CreateAccount(accountMapper);

        if (createdAccount == null)
        {
            return null;
        }

        if (accountLoginMethod == AccountLoginMethod.NORMAL)
        {
            await _accountVerificationService.SendVerificationCodeAsync(accountMapper);
        }

        return account;
    }

    private static string HashPassword(string password)
    {
        using (var algorithm = new Rfc2898DeriveBytes(password, SaltSize, Iterations, HashAlgorithmName.SHA256))
        {
            var salt = algorithm.Salt;
            var key = algorithm.GetBytes(KeySize);

            var hash = new byte[SaltSize + KeySize];
            Array.Copy(salt, 0, hash, 0, SaltSize);
            Array.Copy(key, 0, hash, SaltSize, KeySize);

            return Convert.ToBase64String(hash);
        }
    }
}
