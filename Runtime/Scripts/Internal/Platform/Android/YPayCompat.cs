// Yandex Pay InApps Plugin.

using System.Collections.Generic;
using UnityEngine;

namespace YPay.Android
{
    internal static class YPayCompat
    {
        private const string ClassName = "com.yandex.pay.inapps.YPayCompat";

        internal static List<YPayResultSuccess> GetUnhandledPaymentSuccesses(AndroidJavaObject context, PaymentSession paymentSession)
        {
            return GetUnhandledPaymentSuccesses(context, paymentSession.Instance);
        }

        private static List<YPayResultSuccess> GetUnhandledPaymentSuccesses(AndroidJavaObject context, AndroidJavaObject paymentSession)
        {
            var successes = new List<YPayResultSuccess>();
            AndroidJNI.AttachCurrentThread();

            try
            {
                using var ypay = new AndroidJavaObject(ClassName);
                using var javaList = ypay.Call<AndroidJavaObject>("getUnhandledPaymentSuccessesCompat", context, paymentSession);

                var size = javaList.Call<int>("size");
                for (int i = 0; i < size; i++)
                {
                    using var successObject = javaList.Call<AndroidJavaObject>("get", i);
                    successes.Add(new YPayResultSuccess(successObject));
                }

            }
            catch (System.Exception e)
            {
                Debug.LogError("Error while getting unhandled payment successes: " + e.Message);
            }
            finally
            {
                AndroidJNI.DetachCurrentThread();
            }

            return successes;
        }
    }
}