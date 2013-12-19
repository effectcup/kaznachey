using System.Collections.Generic;

namespace effectcup.KaznacheyPayment
{
    public class MerchantInformation
    {
        public List<PaySystem> PaySystems { get; set; }

        public int ErrorCode { get; set; }

        public string DebugMessage { get; set; }

        public string TermToUse { get; set; }
    }
}
