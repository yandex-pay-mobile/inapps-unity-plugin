// Yandex Pay InApps Plugin.

namespace YPay
{
    /// <summary>
    /// Represents the configuration settings for YPay.
    /// </summary>
    /// <param name="MerchantId">The unique identifier for the merchant.</param>
    /// <param name="MerchantName">The name of the merchant.</param>
    /// <param name="MerchantUrl">The URL associated with the merchant.</param>
    /// <param name="IsSandbox">Indicates whether the configuration is for the sandbox environment.</param>
    public record YPayConfig(string MerchantId, string MerchantName, string MerchantUrl, bool IsSandbox);
}
