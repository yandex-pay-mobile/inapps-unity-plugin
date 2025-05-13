// Yandex Pay InApps Plugin.

namespace YPay.Android
{
    internal class MerchantData : IAndroidObject
    {
        private const string ClassName = "com.yandex.pay.inapps.MerchantData";

        internal MerchantData(string id, string name, string url) : base(
            new(ClassName, new MerchantId(id).Instance, new MerchantName(name).Instance, new MerchantUrl(url).Instance)
        ) { }
    }
}
