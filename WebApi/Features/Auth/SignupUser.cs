using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Security.Cryptography;
using WebApi.Common.Endpoints;
using WebApi.Common.Exceptions;
using WebApi.Common.Utils;
using WebApi.Data;
using WebApi.Data.Entities;
using WebApi.Services.Auth;
using WebApi.Services.Mail;

namespace WebApi.Features.Auth;

public class SignupUser
{
    public record Requestt(string FullName, string Password, string Email, string LoginMethod);
    private const int SaltSize = 16; // 128 bit 
    private const int KeySize = 32;  // 256 bit
    private const int Iterations = 10000; // Number of PBKDF2 iterations
    private static readonly string EmailTitle = @"Xác thực tài khoản - {0}";
    private static readonly string EmailBody = @"
                <html>
                    <body>
                        <p>Chào mừng bạn đến với Tech Gadget VN!</p>
                        <br />
                        <p>Hãy lướt qua lại một vài thông tin cơ bản nhé:</p>
                        <ul>
                            <li>ID: {0}</li>
                            <li>Email: {1}</li>
                        </ul>
                        <br />
                        <p>Để tiếp tục trải nghiệm mọi thứ mà ứng dụng chúng tôi cung cấp, mời bạn nhập mã xác thực sau đây vào
                        ứng dụng của chúng tôi:</p>
                        <br />
                        <p><strong>Mã xác thực: {2}</strong></p>
                    </body>
                </html>
                ";

    public sealed class Validator : AbstractValidator<Requestt>
    {
        public Validator()
        {
            RuleFor(r => r.FullName).NotEmpty();
            RuleFor(r => r.Password).NotEmpty().MinimumLength(8);
            RuleFor(r => r.Email).NotEmpty().EmailAddress();
            RuleFor(r => r.LoginMethod)
            .NotEmpty()
            .Must(BeAValidLoginMethod)
            .WithMessage("Invalid LoginMethod, must be 'Google' or 'Default'.");
        }

        private bool BeAValidLoginMethod(string loginMethod)
        {
            return Enum.TryParse(typeof(LoginMethod), loginMethod, true, out _);
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
            .Produces(StatusCodes.Status200OK);
        }
    }

    public static async Task<IResult> Handler([FromBody] Requestt request, AppDbContext context, IValidator<Requestt> validator, [FromServices] TokenService tokenService, [FromServices] MailService mailService)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }

        var user = new User
        {
            FullName = request.FullName,
            Email = request.Email,
            Role = Role.Buyer,
            LoginMethod = LoginMethod.Default,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        if (LoginMethod.Default == LoginMethod.Default)
        {
            user.Status = UserStatus.Pending;
            user.Password = HashPassword(request.Password);
        }
        else
        {
            user.Status = UserStatus.Active;
        }

        if (await context.Users.AnyAsync(us => us.Email == user.Email && us.Status == UserStatus.Pending))
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WES_0000)
                .AddReason("Lỗi người dùng", "Người dùng chưa xác thực tài khoản")
                .Build();
        }

        if (await context.Users.AnyAsync(us => us.Email == user.Email && us.Status == UserStatus.Pending))
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WES_0000)
                .AddReason("Lỗi Email", "Email này đã được đăng ký trước đó.")
                .Build();
        }

        context.Users.Add(user);
        await context.SaveChangesAsync();

        if (LoginMethod.Default == LoginMethod.Default)
        {
            var code = VerifyCodeGenerator.Generate();

            var userVerify = new UserVerify
            {
                VerifyCode = code,
                Status = VerifyStatus.Pending,
                User = user,
                //CreatedAt = DateTime.UtcNow,
            };

            context.UserVerify.Add(userVerify);
            await context.SaveChangesAsync();

            var emailBody = string.Format(EmailBody, user.Id, user.FullName, code);

            await mailService.SendVerifyCode(string.Format(EmailTitle, user.FullName), user.FullName, emailBody);
        }
        return Results.Ok();
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
