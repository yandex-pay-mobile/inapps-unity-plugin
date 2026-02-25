// Yandex Pay InApps Plugin.

using UnityEngine;

namespace YPay.Android
{
    internal class YPayResultSuccess
    {
        internal string OrderId;

        internal YPayResultSuccess(AndroidJavaObject androidJavaObject)
        {
            var orderIdObject = androidJavaObject.Call<AndroidJavaObject>("getOrderId");
            var orderId = orderIdObject.Call<string>("getValue");
            this.OrderId = orderId;
        }
    }
}
