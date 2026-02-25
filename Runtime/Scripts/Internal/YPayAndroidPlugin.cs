// Yandex Pay InApps Plugin.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YPay
{
    internal static class YPayAndroidPlugin
    {
        private const string PaymentUrlArg = "paymentUrl";
        private const string PaymentSessionKeyArg = "paymentSessionKey";

        internal static void Init(YPayConfig config, string paymentSessionKey)
        {
            if (YPay.Android.YPayActivity.IsPaymentSessionExists(paymentSessionKey)) return;

            var androidMerchantData = new YPay.Android.MerchantData(config.MerchantId, config.MerchantName, config.MerchantUrl);
            var androidConfig = new YPay.Android.YPayConfig(androidMerchantData, config.IsSandbox ? YPay.Android.YPayApiEnvironment.Sandbox : YPay.Android.YPayApiEnvironment.Production);

            var paymentSession = YPay.Android.YPay.GetYandexPaymentSession(androidConfig, paymentSessionKey);
            YPay.Android.YPayActivity.SavePaymentSession(paymentSessionKey, paymentSession);
        }

        internal static async Task<List<IYPayResult.Success>> GetUnhandledPaymentSuccessesAsync(string paymentSessionKey)
        {
            if (!YPay.Android.YPayActivity.IsPaymentSessionExists(paymentSessionKey)) return new();

            var paymentSession = YPay.Android.YPayActivity.GetPaymentSession(paymentSessionKey);
            if (paymentSession == null) return new();

            var activity = YPay.Android.Utils.GetCurrentActivity();
            var unhandledSuccesses = await Task.Run(() => YPay.Android.YPayCompat.GetUnhandledPaymentSuccesses(activity, paymentSession));

            return unhandledSuccesses
                .Select(success => new IYPayResult.Success(success.OrderId))
                .ToList();
        }

        internal static void StartPayment(IYPayResultListener resultListener, string paymentUrl, string paymentSessionKey)
        {
            YPay.Android.YPayActivity.SaveResultListener(paymentSessionKey, new YPay.Android.YPayResultListenerProxy(resultListener));
            YPay.Android.YPayActivity.Launch(new() {
                { PaymentUrlArg, paymentUrl },
                { PaymentSessionKeyArg, paymentSessionKey }
            });
        }

        internal static void Deinitialize(string paymentSessionKey)
        {
            YPay.Android.YPayActivity.RemovePaymentSession(paymentSessionKey);
            YPay.Android.YPay.RemovePaymentSession(paymentSessionKey);
        }
    }
}
