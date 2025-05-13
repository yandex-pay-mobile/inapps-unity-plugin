// Yandex Pay InApps Plugin.

using UnityEngine;
using YPay;

internal class YPayResultListenerProxy : AndroidJavaProxy
{
    private const string ClassName = "com.yandex.pay.inapps.YPayActivityResultListener";
    private IYPayResultListener listener;

    internal YPayResultListenerProxy(IYPayResultListener listener) : base(ClassName)
    {
        this.listener = listener;
    }

    internal void OnResult(string message)
    {
        YPayPlugin.OnPaymentCompleted();
        var result = YPayResultParser.Parse(message);
        listener.OnPaymentResult(result);
    }
}
