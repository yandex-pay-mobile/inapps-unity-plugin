// Yandex Pay InApps Plugin.

using UnityEngine;

namespace YPay
{
    /// <summary>
    /// Represents the result passed by Yandex Pay Library.
    /// </summary>
    public interface IYPayResult
    {
        /// <summary>
        /// Represents a successful payment result.
        /// </summary>
        public class Success : IYPayResult
        {
            /// <summary>
            /// Order identifier
            /// </summary>
            public string OrderId;

            public Success(string orderId) => OrderId = orderId;
        }

        /// <summary>
        /// Represents a failed payment result.
        /// </summary>
        public class Failure : IYPayResult
        {
            /// <summary>
            /// Failure details
            /// </summary>
            public string ErrorMessage;

            public Failure(string errorMessage) => ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Represents a cancelled payment result.
        /// </summary>
        public class Cancelled : IYPayResult { }
    }
}
