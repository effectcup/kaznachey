
using System;

namespace effectcup.KaznacheyPayment
{
    /// <summary>
    /// Base inforamtion object
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// Error code. 0 - if success, otherwise - fail
        /// </summary>
        public Int32 ErrorCode { get; set; }

        /// <summary>
        /// Debug message information.
        /// </summary>
        public String DebugMessage { get; set; }
    }
}