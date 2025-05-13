// Yandex Pay InApps Plugin.

using UnityEngine;

namespace YPay.Android
{
    internal class PaymentSession : IAndroidObject
    {
        internal PaymentSession(AndroidJavaObject androidJavaObject) : base(androidJavaObject) { }
    }
}
