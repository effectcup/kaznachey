using System;
using System.Collections.Generic;

namespace effectcup.KaznacheyPayment
{
    /// <summary>
    /// Represents request for creation payment
    /// </summary>
    public class PaymentRequest
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="selectedPaySystemId">Select payment system Id</param>
        public PaymentRequest(int selectedPaySystemId)
        {
            SelectedPaySystemId = selectedPaySystemId;
        }

        /// <summary>
        /// Select payment system Id
        /// </summary>
        public Int32 SelectedPaySystemId { get; set; }


        /// <summary>
        /// Products list
        /// </summary>
        public List<Product> Products { get; set; }

        /// <summary>
        /// Represents payment detail
        /// </summary>
        public PaymentDetails PaymentDetail { get; set; }
    }
}
