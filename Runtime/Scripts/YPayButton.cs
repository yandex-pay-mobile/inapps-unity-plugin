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
        /// Merchant identifier
        /// </summary>
        public string MerchantId;

        /// <summary>
        /// Merchant name
        /// </summary>
        public string MerchantName;

        /// <summary>
        /// Merchant URL
        /// </summary>
        public string MerchantUrl;

        /// <summary>
        /// Is sandbox mode enabled
        /// </summary>
        public bool IsSandbox;

        YPayPlugin plugin;
        bool isPaymentInProgress;
        IYPayResultListener _paymentResultListener;
        IYPayDataProvider _paymentDataProvider;

        protected override void Start()
        {
            base.Start();
            onClick.AddListener(async () => await StartPaymentAsync());

            plugin = InitPlugin();
        }

        protected override void Awake()
        {
            _paymentResultListener = GetComponent<IYPayResultListener>();
            _paymentDataProvider = GetComponent<IYPayDataProvider>();
            
            if (_paymentDataProvider == null)
            {
                UnityEngine.Debug.LogError($"The implementation of the IYPayResultListener was not found for the {name}.");
            }
            if (_paymentDataProvider == null)
            {
                UnityEngine.Debug.LogError($"The implementation of the IYPayDataProvider was not found for the {name}.");
            }
        }

        public void OnResult(string result)
        {
            isPaymentInProgress = false;
            _paymentResultListener.OnPaymentResult(YPayResultParser.Parse(result));
        }

        private YPayPlugin InitPlugin()
        {
            var config = new YPayConfig(
                MerchantId: MerchantId,
                MerchantName: MerchantName,
                MerchantUrl: MerchantUrl,
                PaymentSessionKey: _paymentDataProvider.GetPaymentSessionKey(),
                IsSandbox: IsSandbox);
            return new YPayPlugin(config);
        }

        private async Task StartPaymentAsync()
        {
            if (isPaymentInProgress) return;
            isPaymentInProgress = true;
            plugin.StartPayment(paymentUrl: await _paymentDataProvider.GetPaymentUrlAsync());
        }
    }
}
