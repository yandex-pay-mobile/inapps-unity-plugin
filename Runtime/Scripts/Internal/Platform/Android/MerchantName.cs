// Yandex Pay InApps Plugin.

using YPay.Base;

namespace YPay.Android
{
    internal class MerchantName : IAndroidObject
    {
        private const string ClassName = "com.yandex.pay.inapps.MerchantName";

        internal MerchantName(string name) : base(new(ClassName, name)) { }
    }
}
