// Yandex Pay InApps Plugin.

using UnityEngine;

namespace YPay
{
    internal static class YPayResultParser
    {
        internal static IYPayResult Parse(string result)
        {
            var index = result.IndexOf(' ');
            var status = result.Substring(0, index).ToLower();
            var json = result.Substring(index + 1);
            switch (status)
            {
                case "success":
                    return JsonUtility.FromJson<IYPayResult.Success>(json);
                case "failure":
                    return JsonUtility.FromJson<IYPayResult.Failure>(json);
                case "cancelled":
                    return new IYPayResult.Cancelled();
                default:
                    Debug.LogError($"Unknown status: {status}");
                    return null;
            }
        }
    }
}
