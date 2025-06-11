// Yandex Pay InApps Plugin.

using System.Collections.Generic;
using UnityEngine;

namespace YPay.Android
{
    internal static class YPayActivity
    {
        const string YPayActivityClassName = "com.yandex.pay.inapps.YPayActivity";
        const string IntentClassName = "android.content.Intent";

        internal static void SavePaymentSession(string paymentSessionKey, PaymentSession paymentSession)
        {
            var ypayActivityClass = new AndroidJavaClass(YPayActivityClassName);
            ypayActivityClass.CallStatic("savePaymentSession", paymentSessionKey, paymentSession.Instance);
        }

        internal static bool IsPaymentSessionExists(string paymentSessionKey)
        {
            var ypayActivityClass = new AndroidJavaClass(YPayActivityClassName);
            return ypayActivityClass.CallStatic<bool>("isPaymentSessionExists", paymentSessionKey);
        }

        internal static void RemovePaymentSession(string paymentSessionKey)
        {
            var ypayActivityClass = new AndroidJavaClass(YPayActivityClassName);
            ypayActivityClass.CallStatic("removePaymentSession", paymentSessionKey);
        }

        internal static void SaveResultListener(string paymentSessionKey, YPayResultListenerProxy resultListener)
        {
            var ypayActivityClass = new AndroidJavaClass(YPayActivityClassName);
            ypayActivityClass.CallStatic("saveResultListener", paymentSessionKey, resultListener);
        }

        internal static void Launch(Dictionary<string, string> extras)
        {
            using var currentActivity = Utils.GetCurrentActivity();
            using var context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
            var intentClass = new AndroidJavaClass(IntentClassName);
            var intent = new AndroidJavaObject(IntentClassName, context, new AndroidJavaClass(YPayActivityClassName));
            foreach (var extra in extras)
            {
                intent.Call<AndroidJavaObject>("putExtra", extra.Key, extra.Value);
            }
            currentActivity.Call("startActivity", intent);
        }

        internal static void ClearStatic()
        {
            var ypayActivityClass = new AndroidJavaClass(YPayActivityClassName);
            ypayActivityClass.CallStatic("clearPaymentSessions");
            ypayActivityClass.CallStatic("clearResultListeners");
        }
    }
}
