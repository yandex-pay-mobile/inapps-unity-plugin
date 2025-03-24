// Yandex Pay InApps Plugin.

using UnityEngine;

namespace YPay.Base 
{
    internal abstract class IAndroidObject
    {
        internal AndroidJavaObject Instance { get; private set; }

        internal IAndroidObject(AndroidJavaObject androidJavaObject)
        {
            Instance = androidJavaObject;
        }
    }
}
