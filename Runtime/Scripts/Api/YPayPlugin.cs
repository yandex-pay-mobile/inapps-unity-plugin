// Yandex Pay InApps Plugin.

using System.Collections.Generic;
using UnityEngine;

namespace YPay
{
    /// <summary>
    /// Provides methods to initialize and manage payments using the Yandex Pay plugin.
    /// This class handles the initialization of the plugin with a configuration and starting payments on supported platforms.
    /// </summary>
    public static class YPayPlugin
    {
        private static readonly object syncLock = new();

        /// <summary>
        /// Initializes the YPay plugin with the provided configuration.
        /// </summary>
        /// <param name="config">The configuration settings for initializing the YPay plugin.</param>
        /// <returns>The session key for the payment session. Use it to start the payment when calling the `YPayPlugin.StartPayment` function.</returns>
        public static string Init(YPayConfig config)
        {
            var paymentSessionKey = YPaySessionKeyGenerator.GenerateSessionKey(config.MerchantId, config.IsSandbox);

            lock (syncLock)
            {
                if (Application.platform == RuntimePlatform.Android)
                {
                    YPayAndroidPlugin.Init(config, paymentSessionKey);
                    return paymentSessionKey;
                }
                else if (!Application.platform.IsEditor())
                {
                    Debug.LogError($"YPay Plugin is not supported on platform: {Application.platform}");
                }
            }

            return null;
        }

        /// <summary>
        /// Starts a payment process using the provided payment URL and result listener.
        /// </summary>
        /// <param name="resultListener">The listener to handle the payment result.</param>
        /// <param name="paymentUrl">The URL to start the payment process.</param>
        /// <param name="paymentSessionKey">The session key for the payment session. You get this value as a result of calling the `YPayPlugin.Init` function.</param>
        public static void StartPayment(IYPayResultListener resultListener, string paymentUrl, string paymentSessionKey)
        {
            lock (syncLock)
            {
                if (Application.platform == RuntimePlatform.Android)
                {
                    YPayAndroidPlugin.StartPayment(resultListener, paymentUrl, paymentSessionKey);
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
        /// <param name="paymentSessionKey">The session key for the payment session. You get this value as a result of calling the `YPayPlugin.Init` function.</param>
        public static void Deinitialize(string paymentSessionKey)
        {
            lock (syncLock)
            {
                if (Application.platform == RuntimePlatform.Android)
                {
                    YPayAndroidPlugin.Deinitialize(paymentSessionKey);
                }
                else if (!Application.platform.IsEditor())
                {
                    Debug.LogError($"YPay Plugin is not supported on platform: {Application.platform}");
                }
            }
        }
    }
}
