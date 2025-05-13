// Yandex Pay InApps Plugin.

using System.Collections.Generic;
using UnityEngine;

namespace YPay.Android
{
    internal static class Utils
    {
        const string UnityPlayerClassName = "com.unity3d.player.UnityPlayer";

        internal static AndroidJavaObject GetCurrentActivity()
        {
            using var unityPlayer = new AndroidJavaClass(UnityPlayerClassName);
            return unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        }
    }
}
