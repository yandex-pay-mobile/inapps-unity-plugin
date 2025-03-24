// Yandex Pay InApps Plugin.

using UnityEngine;

namespace YPay.Android
{
    internal static class YPay
    {
        private const string ClassName = "com.yandex.pay.inapps.YPay";

        internal static PaymentSession GetYandexPaymentSession(YPayConfig config, string paymentSessionKey)
        {
            return GetYandexPaymentSession(Utils.GetCurrentActivity(), config.Instance, new PaymentSessionKey(paymentSessionKey).Instance);
        }

        private static PaymentSession GetYandexPaymentSession(AndroidJavaObject context, AndroidJavaObject config, AndroidJavaObject paymentSessionKey)
        {
            var ypay = new AndroidJavaObject(ClassName);
            return new(ypay.Call<AndroidJavaObject>("getYandexPaymentSession", context, config, paymentSessionKey));
        }
    }
}
