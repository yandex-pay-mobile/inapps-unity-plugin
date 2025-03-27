// Yandex Pay InApps Plugin.

using UnityEngine;
using YPay.Base;

namespace YPay.Android
{
    internal class PaymentSession : IAndroidObject
    {
        internal PaymentSession(AndroidJavaObject androidJavaObject) : base(androidJavaObject) { }
    }
}
