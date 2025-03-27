// Yandex Pay InApps Plugin.

using System.Collections.Generic;
using UnityEngine;

namespace YPay.Android
{
    internal static class Utils
    {
        const string YPayActivityClassName = "com.yandex.pay.inapps.YPayActivity";
        const string UnityPlayerClassName = "com.unity3d.player.UnityPlayer";
        const string IntentClassName = "android.content.Intent";

        internal static AndroidJavaObject GetCurrentActivity()
        {
            using var unityPlayer = new AndroidJavaClass(UnityPlayerClassName);
            return unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        }

        internal static void SavePaymentSession(PaymentSession paymentSession)
        {
            var ypayActivityClass = new AndroidJavaClass(YPayActivityClassName);
            ypayActivityClass.CallStatic("setPaymentSession", paymentSession.Instance);
        }

        internal static void LaunchYPayActivity(Dictionary<string, string> extras)
        {
            using var currentActivity = GetCurrentActivity();
            using var context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
            var intentClass = new AndroidJavaClass(IntentClassName);
            var intent = new AndroidJavaObject(IntentClassName, context, new AndroidJavaClass(YPayActivityClassName));
            foreach (var extra in extras)
            {
                intent.Call<AndroidJavaObject>("putExtra", extra.Key, extra.Value);
            }
            currentActivity.Call("startActivity", intent);
        }
    }
}
