// Yandex Pay InApps Plugin.

namespace YPay.Android
{
    internal class MerchantId : IAndroidObject
    {
        private const string ClassName = "com.yandex.pay.inapps.MerchantId";

        internal MerchantId(string id) : base(new(ClassName, id)) { }
    }
}
