// Yandex Pay InApps Plugin.

using System.Threading.Tasks;
using UnityEngine.Events;
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

        IYPayResultListener paymentResultListener;
        IYPayDataProvider paymentDataProvider;

        protected override void Start()
        {
            base.Start();
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
            YPayPlugin.Deinitialize();
        }

        private void InitPlugin()
        {
            var config = new YPayConfig(
                MerchantId: MerchantId,
                MerchantName: MerchantName,
                MerchantUrl: MerchantUrl ?? "",
                IsSandbox: IsSandbox);
            YPayPlugin.Init(config);
        }

        private async Task StartPaymentAsync()
        {
            YPayPlugin.StartPayment(resultListener: paymentResultListener, paymentUrl: await paymentDataProvider.GetPaymentUrlAsync());
        }
    }
}
