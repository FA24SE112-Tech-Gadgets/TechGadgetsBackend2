using Microsoft.EntityFrameworkCore;
using WebApi.Common.Exceptions;
using WebApi.Common.Utils;
using WebApi.Data;
using WebApi.Data.Entities;
using WebApi.Services.Mail;

namespace WebApi.Services.VerifyCode;

public class VerifyCodeService(MailService mailService, AppDbContext context)
{
    private static readonly string EmailTitle = @"Xác thực tài khoản - {0}";
    private static readonly string EmailBody = @"
                <html>
                    <body>
                        <p>Chào mừng bạn đến với Tech Gadget VN!</p>
                        <br />
                        <p>Hãy lướt qua lại một vài thông tin cơ bản nhé:</p>
                        <ul>
                            <li>Email: {0}</li>
                            <li>Họ và tên: {1}</li>
                        </ul>
                        <br />
                        <p>Để tiếp tục trải nghiệm mọi thứ mà ứng dụng chúng tôi cung cấp, mời bạn nhập mã xác thực sau đây vào
                        ứng dụng của chúng tôi:</p>
                        <br />
                        <p><strong>Mã xác thực: {2}</strong></p>
                    </body>
                </html>
                ";

    private readonly MailService _mailService = mailService;
    private readonly long VerificationDuration = 1L * 60;
    private readonly AppDbContext _context = context;

    public async Task SendVerifyCodeAsync(User user)
    {
        var code = VerifyCodeGenerator.Generate();

        var userVerify = new UserVerify
        {
            VerifyCode = code,
            Status = VerifyStatus.Pending,
            User = user,
            CreatedAt = DateTime.UtcNow,
        };

        _context.UserVerify.Add(userVerify);
        await _context.SaveChangesAsync();

        var emailBody = string.Format(EmailBody, user.Email, user.FullName, code);

        await _mailService.SendMail(string.Format(EmailTitle, user.Email), user.Email, emailBody);
    }

    public async Task ResendVerifyCodeAsync(User user)
    {
        await _context.UserVerify
               .Where(a => a.UserId == user.Id)
               .ExecuteUpdateAsync(setters => setters.SetProperty(a => a.Status, VerifyStatus.Expired));

        await SendVerifyCodeAsync(user);
    }

    public async Task VerifyUserAsync(User user, string code)
    {
        if (user.Status != UserStatus.Pending)
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEB_03)
                .AddReason("user", "Tài khoản không trong trạng thái cần xác thực")
                .Build();
        }

        var userVerify = await _context.UserVerify
                .Where(a => a.VerifyCode == code && a.UserId == user.Id)
                .OrderByDescending(a => a.CreatedAt)
                .FirstOrDefaultAsync();

        if (userVerify == null)
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEB_02)
                .AddReason("verifyCode", "Mã xác thực không hợp lệ")
                .Build();
        }
        var maxTime = userVerify.CreatedAt.AddSeconds(VerificationDuration);
        if (maxTime < DateTime.UtcNow)
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WEB_02)
                .AddReason("verifyCode", "Mã xác thực hết hạn")
                .Build();
        }
        await _context.UserVerify
                .Where(a => a.Id == userVerify.Id)
                .ExecuteUpdateAsync(setters => setters.SetProperty(a => a.Status, VerifyStatus.Verified));

        await _context.Users
                .Where(a => a.Id == user.Id)
                .ExecuteUpdateAsync(setters => setters.SetProperty(a => a.Status, UserStatus.Active));
    }
}
