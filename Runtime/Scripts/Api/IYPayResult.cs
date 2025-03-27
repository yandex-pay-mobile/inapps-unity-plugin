// Yandex Pay InApps Plugin.

using UnityEngine;

namespace YPay
{
    /// <summary>
    /// Result passed by Yandex Pay Library.
    /// </summary>
    public interface IYPayResult
    {
        /// <summary>
        /// Payment flow completed successfully.
        /// </summary>
        public class Success : IYPayResult
        {
            /// <summary>
            /// Order identifier
            /// </summary>
            public string OrderId;
        }

        /// <summary>
        /// Payment flow was completed with an error.
        /// </summary>
        public record Failure : IYPayResult
        {
            /// <summary>
            /// Failure details
            /// </summary>
            public string ErrorMessage;
        }

        /// <summary>
        /// Payment flow was canceled.
        /// </summary>
        public class Cancelled : IYPayResult { }
    }
}
