using Microsoft.Extensions.Options;
using WebApi.Common.Settings;
using WebApi.Common.Utils;
using WebApi.Common.Utils.VnPay;
using WebApi.Services.Payment.Models;
using WebApi.Services.Server;

namespace WebApi.Services.Payment;

public class VnPayPaymentService(
    IOptions<VnPaySettings> vnPaySettings, CurrentServerService currentServerService)
{
    private const string DefaultPaymentInfo = "Thanh toán với VnPay";

    private readonly VnPaySettings _vnPaySettings = vnPaySettings.Value;

    public async Task<string> CreatePaymentAsync(VnPayPayment payment)
    {
        var pay = new VnPayLibrary();
        pay.AddRequestData("vnp_ReturnUrl", $"{currentServerService.ServerUrl}/{_vnPaySettings.CallbackUrl}?returnUrl={payment.returnUrl}");
        pay.AddRequestData("vnp_Version", _vnPaySettings.Version);
        pay.AddRequestData("vnp_Command", _vnPaySettings.Command);
        pay.AddRequestData("vnp_TmnCode", _vnPaySettings.TmnCode);
        pay.AddRequestData("vnp_CurrCode", _vnPaySettings.CurrCode);
        pay.AddRequestData("vnp_Locale", _vnPaySettings.Locale);

        pay.AddRequestData("vnp_Amount", ((int)payment.Amount * 100).ToString());
        pay.AddRequestData("vnp_CreateDate", payment.Time.ToString("yyyyMMddHHmmss"));
        pay.AddRequestData("vnp_IpAddr", UtilityExtensions.GetIpAddress());
        pay.AddRequestData("vnp_OrderInfo", payment.Info ?? DefaultPaymentInfo);
        pay.AddRequestData("vnp_OrderType", "deposit");
        pay.AddRequestData("vnp_TxnRef", payment.PaymentReferenceId);

        var paymentUrl = pay.CreateRequestUrl(
            _vnPaySettings.PaymentEndpoint,
            _vnPaySettings.HashSecret);

        return await Task.FromResult(paymentUrl);
    }
}
