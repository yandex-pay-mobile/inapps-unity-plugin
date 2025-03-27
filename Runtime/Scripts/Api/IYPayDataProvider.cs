// Yandex Pay InApps Plugin.

using System.Threading.Tasks;

namespace YPay
{
    /// <summary>
    /// Class for providing payment data to the Yandex Pay Button.
    /// </summary>
    public interface IYPayDataProvider
    {
        /// <summary>
        /// Function for generating payment session key.
        /// </summary>
        /// <returns>Payment session key</returns>
        public string GetPaymentSessionKey();

        /// <summary>
        /// Function for generating payment URL.
        /// </summary>
        /// <returns>Payment URL</returns>
        public Task<string> GetPaymentUrlAsync();
    }   
}
