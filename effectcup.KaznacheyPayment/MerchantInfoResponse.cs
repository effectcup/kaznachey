using System;
using System.Collections.Generic;

namespace effectcup.KaznacheyPayment
{
    /// <summary>
    /// Merchant information
    /// </summary>
    public class MerchantInfoResponse : BaseResponse
    {
        /// <summary>
        /// Merchant payment system list
        /// </summary>
        public List<PaySystem> PaySystems { get; set; }

        /// <summary>
        /// Term to use file refer
        /// </summary>
        public String TermToUse { get; set; }
    }
}
