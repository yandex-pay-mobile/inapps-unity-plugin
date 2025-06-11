// Yandex Pay InApps Plugin.

using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace YPay
{
    /// <summary>
    /// Yandex Pay Button.
    /// 
    /// Use the prefab (Packages/Yandex Pay InApps/Runtime/Prefabs/YPay Button) to add the button to the stage.
    /// We recommend to specify the button parameters (such as MerchandId, MerchantName, MerchantUrl, and IsSandbox) via the Unity Editor.
    /// </summary>
    public class YPayButton : Button
    {
        /// <summary>
        /// The unique identifier for the merchant.
        /// </summary>
        public string MerchantId;

        /// <summary>
        /// The name of the merchant.
        /// </summary>
        public string MerchantName;

        /// <summary>
        /// The URL associated with the merchant.
        /// </summary>
        public string MerchantUrl;

        /// <summary>
        /// Indicates whether the configuration is for the sandbox environment.
        /// </summary>
        public bool IsSandbox;

        private static Dictionary<string, int> paymentSessionsCount = new Dictionary<string, int>();

        IYPayResultListener paymentResultListener;
        IYPayDataProvider paymentDataProvider;
        string paymentSessionKey;

        protected override void Start()
        {
            base.Start();

            onClick.RemoveAllListeners(); 
            onClick.AddListener(async () => await StartPaymentAsync());

            InitPlugin();
        }

        protected override void Awake()
        {
            paymentResultListener = GetComponent<IYPayResultListener>();
            paymentDataProvider = GetComponent<IYPayDataProvider>();

            if (paymentResultListener == null)
            {
                UnityEngine.Debug.LogError($"The implementation of the IYPayResultListener was not found for the {name}.");
            }
            if (paymentDataProvider == null)
            {
                UnityEngine.Debug.LogError($"The implementation of the IYPayDataProvider was not found for the {name}.");
            }
        }

        protected override void OnDestroy()
        {
            DecrementPaymentSessionsCount();
            if (paymentSessionKey != null && !paymentSessionsCount.ContainsKey(paymentSessionKey))
            {
                YPayPlugin.Deinitialize(paymentSessionKey);
            }
        }

        private void InitPlugin()
        {
            var config = new YPayConfig(
                MerchantId: MerchantId,
                MerchantName: MerchantName,
                MerchantUrl: MerchantUrl ?? "",
                IsSandbox: IsSandbox);
            paymentSessionKey = YPayPlugin.Init(config);
            IncrementPaymentSessionsCount();
        }

        private async Task StartPaymentAsync()
        {
            var paymentUrl = await paymentDataProvider.GetPaymentUrlAsync();
            YPayPlugin.StartPayment(paymentResultListener, paymentUrl, paymentSessionKey);
        }

        private void IncrementPaymentSessionsCount()
        {
            if (paymentSessionKey == null) return;

            if (!paymentSessionsCount.ContainsKey(paymentSessionKey))
            {
                paymentSessionsCount[paymentSessionKey] = 0;
            }
            paymentSessionsCount[paymentSessionKey]++;
        }

        private void DecrementPaymentSessionsCount()
        {
            if (paymentSessionKey == null) return;

            if (paymentSessionsCount.ContainsKey(paymentSessionKey))
            {
                paymentSessionsCount[paymentSessionKey]--;
                if (paymentSessionsCount[paymentSessionKey] == 0)
                {
                    paymentSessionsCount.Remove(paymentSessionKey);
                }
            }
        }
    }
}
