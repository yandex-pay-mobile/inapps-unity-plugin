// Yandex Pay InApps Plugin.

using YPay.Base;

namespace YPay.Android
{
    internal class PaymentSessionKey : IAndroidObject
    {
        private const string ClassName = "com.yandex.pay.inapps.PaymentSessionKey";

        internal PaymentSessionKey(string name) : base(new(ClassName, name)) { }
    }
}
