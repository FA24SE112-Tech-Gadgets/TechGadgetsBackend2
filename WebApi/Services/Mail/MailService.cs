using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Org.BouncyCastle.Utilities;
using WebApi.Common.Settings;

namespace WebApi.Services.Mail;

public class MailService
{
    private readonly MailSettings _mailSettings;

    public MailService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public async Task SendVerifyCode(string subject, string mailTo, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Csharp-demo", _mailSettings.Mail));
        message.To.Add(new MailboxAddress("", mailTo));
        message.Subject = subject;

        message.Body = new TextPart("html")
        {
            Text = body
        };

        using var client = new SmtpClient();
        await client.ConnectAsync(_mailSettings.Host, Int32.Parse(_mailSettings.Port), MailKit.Security.SecureSocketOptions.StartTls);

        await client.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);

        await client.SendAsync(message);

        await client.DisconnectAsync(true);
    }
}
