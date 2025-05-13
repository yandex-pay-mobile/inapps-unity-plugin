// Yandex Pay InApps Plugin.

namespace YPay.Android
{
    internal class YPayConfig : IAndroidObject
    {
        private const string ClassName = "com.yandex.pay.inapps.YPayConfig";

        internal YPayConfig(MerchantData merchantData, YPayApiEnvironment environment) : base(
            new(ClassName, merchantData.Instance, environment.GetInstance())
        ) { }
    }
}
