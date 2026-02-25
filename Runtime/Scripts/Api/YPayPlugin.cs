// Yandex Pay InApps Plugin.

using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace YPay
{
    /// <summary>
    /// Provides functions to initialize and manage payments using the Yandex Pay plugin.
    /// This class handles the initialization of the plugin with a configuration and starting payments on supported platforms.
    /// </summary>
    public static class YPayPlugin
    {
        private static readonly object syncLock = new();

        /// <summary>
        /// Initializes the YPay plugin with the provided configuration.
        /// After initialization, it is recommended to call `YPayPlugin.GetUnhandledPaymentSuccessesAsync` function to handle any previous unprocessed successful payments.
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
        /// Gets a list of successful payment operations that were not handled previously (e.g., if the user exited the app during the payment process).
        /// It is recommended to call this function first to handle any previous unprocessed successful payments before starting new payments.
        /// </summary>
        /// <param name="paymentSessionKey">The session key for the payment session. You get this value as a result of calling the `YPayPlugin.Init` function.</param>
        /// <returns>A list of successful payment results that have not been handled yet.</returns>
        public static async Task<List<IYPayResult.Success>> GetUnhandledPaymentSuccessesAsync(string paymentSessionKey)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                return await YPayAndroidPlugin.GetUnhandledPaymentSuccessesAsync(paymentSessionKey);
            }
            else if (!Application.platform.IsEditor())
            {
                Debug.LogError($"YPay Plugin is not supported on platform: {Application.platform}");
            }

            return null;
        }

        /// <summary>
        /// Starts a payment process using the provided payment URL and result listener.
        /// It is recommended to call `YPayPlugin.GetUnhandledPaymentSuccessesAsync` function first to handle any previous unprocessed successful payments before starting new payments.
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
