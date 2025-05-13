// Yandex Pay InApps Plugin.

namespace YPay
{
    internal static class YPayAndroidPlugin
    {
        internal static void Init(YPayConfig config, string paymentSessionKey)
        {
            var androidMerchantData = new YPay.Android.MerchantData(config.MerchantId, config.MerchantName, config.MerchantUrl);
            var androidConfig = new YPay.Android.YPayConfig(androidMerchantData, config.IsSandbox ? YPay.Android.YPayApiEnvironment.Sandbox : YPay.Android.YPayApiEnvironment.Production);

            var paymentSession = YPay.Android.YPay.GetYandexPaymentSession(androidConfig, paymentSessionKey);
            YPay.Android.YPayActivity.SavePaymentSession(paymentSession);
        }

        internal static void StartPayment(IYPayResultListener resultListener, string paymentUrl)
        {
            YPay.Android.YPayActivity.SaveResultListener(new YPayResultListenerProxy(resultListener));
            YPay.Android.YPayActivity.Launch(new() {
                { "paymentUrl", paymentUrl }
            });
        }

        internal static void Deinitialize(string paymentSessionKey)
        {
            YPay.Android.YPay.RemovePaymentSession(paymentSessionKey);
            YPay.Android.YPayActivity.ClearStatic();
        }
    }
}
