// Yandex Pay InApps Plugin.

namespace YPay.Android
{
    internal class PaymentSessionKey : IAndroidObject
    {
        private const string ClassName = "com.yandex.pay.inapps.PaymentSessionKey";

        internal PaymentSessionKey(string name) : base(new(ClassName, name)) { }
    }
}
