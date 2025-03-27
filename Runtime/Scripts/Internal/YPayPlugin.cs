// Yandex Pay InApps Plugin.

using UnityEngine;
using YPay.Android;

namespace YPay
{
    internal class YPayPlugin
    {
        internal YPayPlugin(YPayConfig config)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                var androidMerchantData = new Android.MerchantData(config.MerchantId, config.MerchantName, config.MerchantUrl);
                var androidConfig = new Android.YPayConfig(androidMerchantData, config.IsSandbox ? YPayApiEnvironment.Sandbox : YPayApiEnvironment.Production);
                var paymentSession = Android.YPay.GetYandexPaymentSession(androidConfig, config.PaymentSessionKey);
                Android.Utils.SavePaymentSession(paymentSession);
            } else if (Application.platform != RuntimePlatform.OSXEditor) {
                Debug.LogError($"YPay Plugin is not supported on platform: {Application.platform}");
            }
        }

        internal void StartPayment(string paymentUrl)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                Android.Utils.LaunchYPayActivity(new() {
                    { "paymentUrl", paymentUrl }
                });
            } else if (Application.platform != RuntimePlatform.OSXEditor) {
                Debug.LogError($"YPay Plugin is not supported on platform: {Application.platform}");
            } 
        }
    }
}
