// Yandex Pay InApps Plugin.

using UnityEngine;

namespace YPay
{
    internal static class RuntimePlatformExtensions
    {
        internal static bool IsEditor(this RuntimePlatform platform) => platform == RuntimePlatform.OSXEditor ||
            platform == RuntimePlatform.WindowsEditor ||
            platform == RuntimePlatform.LinuxEditor;
    }
}
