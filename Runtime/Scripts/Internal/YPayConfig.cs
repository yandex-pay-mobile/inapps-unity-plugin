// Yandex Pay InApps Plugin.

namespace YPay
{
    internal record YPayConfig(string MerchantId, string MerchantName, string MerchantUrl, string PaymentSessionKey, bool IsSandbox);
}
