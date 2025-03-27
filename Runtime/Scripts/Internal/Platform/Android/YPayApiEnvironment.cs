// Yandex Pay InApps Plugin.

using UnityEngine;

namespace YPay.Android
{
    internal enum YPayApiEnvironment
    {
        Production,
        Sandbox
    }

    internal static class YPayApiEnvironmentExtensions
    {
        private const string ClassName = "com.yandex.pay.inapps.YPayApiEnvironment";

        internal static AndroidJavaObject GetInstance(this YPayApiEnvironment environment)
        {
            var apiEnvironmentClass = new AndroidJavaClass(ClassName);
            return apiEnvironmentClass.GetStatic<AndroidJavaObject>(environment.GetName());
        }

        private static string GetName(this YPayApiEnvironment environment)
        {
            return environment switch
            {
                YPayApiEnvironment.Production => "PROD",
                YPayApiEnvironment.Sandbox => "SANDBOX",
                _ => throw new System.InvalidOperationException("Unknown environment"),
            };
        }
    }
}
