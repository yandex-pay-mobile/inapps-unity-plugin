// Yandex Pay InApps Plugin.

using System.Threading.Tasks;

namespace YPay
{
    /// <summary>
    /// Class for processing the payment result.
    /// </summary>
    public interface IYPayResultListener
    {
        /// <summary>
        /// Function for processing the payment result.
        /// </summary>
        /// <param name="result">Payment result: IYPayResult.Success, IYPayResult.Failure or IYPayResult.Cancelled</param>
        public void OnPaymentResult(IYPayResult result);
    }   
}
