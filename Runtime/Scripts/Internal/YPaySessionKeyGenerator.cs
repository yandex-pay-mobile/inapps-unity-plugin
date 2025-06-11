// Yandex Pay InApps Plugin.

using System;

namespace YPay
{
    internal static class YPaySessionKeyGenerator
    {
        internal static string GenerateSessionKey(string merchantId, bool isSandbox)
        {
            string sandbox = isSandbox ? "sandbox" : "prod";
            return $"{merchantId}.{sandbox}";
        }
    }
}
