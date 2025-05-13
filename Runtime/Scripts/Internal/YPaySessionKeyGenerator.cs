// Yandex Pay InApps Plugin.

using System;

namespace YPay
{
    internal static class YPaySessionKeyGenerator
    {
        internal static string GenerateSessionKey(string prefix)
        {
            return $"{prefix}.{Guid.NewGuid()}";
        }
    }
}
