// Yandex Pay InApps Plugin.

using System.Collections.Generic;
using UnityEngine;

namespace YPay.Android
{
    internal static class YPayActivity
    {
        const string YPayActivityClassName = "com.yandex.pay.inapps.YPayActivity";
        const string IntentClassName = "android.content.Intent";

        internal static void SavePaymentSession(PaymentSession paymentSession)
        {
            var ypayActivityClass = new AndroidJavaClass(YPayActivityClassName);
            ypayActivityClass.CallStatic("setPaymentSession", paymentSession.Instance);
        }

        internal static void SaveResultListener(YPayResultListenerProxy resultListener)
        {
            var ypayActivityClass = new AndroidJavaClass(YPayActivityClassName);
            ypayActivityClass.CallStatic("setResultListener", resultListener);
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
            ypayActivityClass.CallStatic("clearPaymentSession");
            ypayActivityClass.CallStatic("clearResultListener");
        }
    }
}
