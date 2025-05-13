// Yandex Pay InApps Plugin.

namespace YPay.Android
{
    internal class MerchantUrl : IAndroidObject
    {
        private const string ClassName = "com.yandex.pay.inapps.MerchantUrl";

        internal MerchantUrl(string url) : base(new(ClassName, url)) { }
    }
}
