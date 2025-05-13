// Yandex Pay InApps Plugin.

using UnityEngine;

namespace YPay
{
    /// <summary>
    /// Provides methods to initialize and manage payments using the Yandex Pay plugin.
    /// This class handles the initialization of the plugin with a configuration and starting payments on supported platforms.
    /// </summary>
    public static class YPayPlugin
    {
        private static volatile bool isInitialized = false;
        private static volatile bool isPaymentInProgress = false;
        private static volatile string paymentSessionKey = null;
        private static readonly object syncLock = new object();

        /// <summary>
        /// Initializes the YPay plugin with the provided configuration.
        /// </summary>
        /// <param name="config">The configuration settings for initializing the YPay plugin.</param>
        public static void Init(YPayConfig config)
        {
            if (isInitialized) return;

            lock (syncLock)
            {
                if (isInitialized) return;

                paymentSessionKey = YPaySessionKeyGenerator.GenerateSessionKey(config.MerchantId);
                if (Application.platform == RuntimePlatform.Android)
                {
                    YPayAndroidPlugin.Init(config, paymentSessionKey);
                    isInitialized = true;
                }
                else if (!Application.platform.IsEditor())
                {
                    Debug.LogError($"YPay Plugin is not supported on platform: {Application.platform}");
                }
            }
        }

        /// <summary>
        /// Starts a payment process using the provided payment URL and result listener.
        /// </summary>
        /// <param name="resultListener">The listener to handle the payment result.</param>
        /// <param name="paymentUrl">The URL to start the payment process.</param>
        public static void StartPayment(IYPayResultListener resultListener, string paymentUrl)
        {
            if (isPaymentInProgress) return;

            lock (syncLock)
            {
                if (!isInitialized)
                {
                    Debug.LogError("YPay Plugin is not initialized!");
                    resultListener.OnPaymentResult(new IYPayResult.Failure("config data not provided"));
                    return;
                }

                if (isPaymentInProgress) return;

                if (Application.platform == RuntimePlatform.Android)
                {
                    isPaymentInProgress = true;
                    YPayAndroidPlugin.StartPayment(resultListener, paymentUrl);
                }
                else if (!Application.platform.IsEditor())
                {
                    Debug.LogError($"YPay Plugin is not supported on platform: {Application.platform}");
                }
            }
        }

        /// <summary>
        /// Deinitializes the YPay plugin instance and cleans up resources.
        /// </summary>
        public static void Deinitialize()
        {
            if (!isInitialized) return;

            lock (syncLock)
            {
                if (!isInitialized) return;

                if (Application.platform == RuntimePlatform.Android)
                {
                    YPayAndroidPlugin.Deinitialize(paymentSessionKey);
                    paymentSessionKey = null;
                    isInitialized = false;
                }
                else if (!Application.platform.IsEditor())
                {
                    Debug.LogError($"YPay Plugin is not supported on platform: {Application.platform}");
                }
            }
        }

        internal static void OnPaymentCompleted()
        {
            isPaymentInProgress = false;
        }
    }
}
