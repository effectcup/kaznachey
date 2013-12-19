namespace effectcup.KaznacheyPayment
{
    public class PaymentDetails
    {
        public string MerchantInternalPaymentId { get; set; }

        public string MerchantInternalUserId { get; set; }

        public string CustomMerchantInfo { get; set; }

        public string StatusUrl { get; set; }

        public string ReturnUrl { get; set; }

        public string BuyerLastname { get; set; }
        public string BuyerFirstname { get; set; }
        public string BuyerPatronymic { get; set; }
        public string BuyerStreet { get; set; }
        public string BuyerCity { get; set; }
        public string BuyerZone { get; set; }
        public string BuyerZip { get; set; }
        public string BuyerCountry { get; set; }

        public string DeliveryLastname { get; set; }
        public string DeliveryFirstname { get; set; }
        public string DeliveryPatronymic { get; set; }
        public string DeliveryStreet { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryZone { get; set; }
        public string DeliveryZip { get; set; }
        public string DeliveryCountry { get; set; }
    }
}
