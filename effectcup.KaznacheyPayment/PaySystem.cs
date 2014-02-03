using System;
using System.Collections.Generic;

namespace effectcup.KaznacheyPayment
{
    /// <summary>
    /// Payment system information
    /// </summary>
    public class PaySystem
    {
        /// <summary>
        /// Payment system Id
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// Payment system name
        /// </summary>
        public String PaySystemName { get; set; }


        /// <summary>
        /// Payment system tag(use for css style). Sample: webMoney,VisaMc,liqPay, etc
        /// </summary>
        public String PaySystemTag { get; set; }
    }
}
