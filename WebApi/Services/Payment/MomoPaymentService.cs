using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
using WebApi.Common.Settings;
using WebApi.Common.Utils;
using WebApi.Services.Payment.Models;
using WebApi.Services.Server;

namespace WebApi.Services.Payment;

public class MomoPaymentService(
    IOptions<MomoSettings> momoSettings, CurrentServerService currentServerService)
{
    private const string DefaultOrderInfo = "Thanh toán với Momo";

    private readonly MomoSettings _momoSettings = momoSettings.Value;

    public async Task<string> CreatePaymentAsync(MomoPayment payment)
    {
        var requestType = "payWithATM";
        var request = new MomoPaymentRequest
        {
            OrderInfo = payment.Info ?? DefaultOrderInfo,
            PartnerCode = _momoSettings.PartnerCode,
            IpnUrl = _momoSettings.IpnUrl,
            RedirectUrl = $"{currentServerService.ServerUrl}/{_momoSettings.RedirectUrl}?returnUrl={payment.returnUrl}",

            Amount = payment.Amount,
            OrderId = payment.PaymentReferenceId,
            RequestId = Guid.NewGuid().ToString(),
            RequestType = requestType,
            ExtraData = "s",
            AutoCapture = true,
            Lang = "vi"
        };

        var rawSignature = $"accessKey={_momoSettings.AccessKey}&amount={request.Amount}&extraData={request.ExtraData}&ipnUrl={request.IpnUrl}&orderId={request.OrderId}&orderInfo={request.OrderInfo}&partnerCode={request.PartnerCode}&redirectUrl={request.RedirectUrl}&requestId={request.RequestId}&requestType={requestType}";
        request.Signature = GetSignature(rawSignature, _momoSettings.SecretKey);

        var httpContent = new StringContent(JsonSerializerUtils.Serialize(request), Encoding.UTF8, "application/json");
        using var httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromSeconds(30);
        var momoResponse = await httpClient.PostAsync(_momoSettings.PaymentEndpoint, httpContent);
        var responseContent = momoResponse.Content.ReadAsStringAsync().Result;

        if (momoResponse.IsSuccessStatusCode)
        {
            var momoPaymentResponse = JsonSerializerUtils.Deserialize<MomoPaymentResponse>(responseContent);
            if (momoPaymentResponse != null)
            {
                return momoPaymentResponse.PayUrl;
            }
        }

        throw new Exception($"[Momo payment] Error: There is some error when create payment with momo. {responseContent}");
    }

    private static string GetSignature(string text, string key)
    {
        var encoding = new UTF8Encoding();

        var textBytes = encoding.GetBytes(text);
        var keyBytes = encoding.GetBytes(key);

        byte[] hashBytes;

        using HMACSHA256 hash = new(keyBytes);
        hashBytes = hash.ComputeHash(textBytes);

        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }
}
