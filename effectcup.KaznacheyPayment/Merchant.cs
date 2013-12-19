using System;

namespace effectcup.KaznacheyPayment
{
    public class Merchant
    {
        public Guid MerchantGuid { get; set; }

        public string Signature { get; set; }
    }
}
