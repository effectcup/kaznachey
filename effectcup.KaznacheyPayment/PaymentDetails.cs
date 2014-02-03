using System;

namespace effectcup.KaznacheyPayment
{
    /// <summary>
    /// Payment details
    /// </summary>
    public class PaymentDetails
    {
        /// <summary>
        /// Merchant cms internal payment id
        /// </summary>
        public String MerchantInternalPaymentId { get; set; }

        /// <summary>
        /// Merchant internal user id
        /// </summary>
        public String MerchantInternalUserId { get; set; }

        /// <summary>
        /// Client email. Required
        /// </summary>
        public String EMail { get; set; }

        /// <summary>
        /// Client phone number. Required
        /// </summary>
        public String PhoneNumber { get; set; }

        /// <summary>
        /// Merchant cms custom information
        /// </summary>
        public String CustomMerchantInfo { get; set; }

        /// <summary>
        /// Status URL. Used for status delivery
        /// </summary>
        public String StatusUrl { get; set; }

        /// <summary>
        /// Return Url. User will be redirected here after payment processing
        /// </summary>
        public String ReturnUrl { get; set; }

        public String BuyerLastname { get; set; }
        public String BuyerFirstname { get; set; }
        public String BuyerPatronymic { get; set; }
        public String BuyerStreet { get; set; }
        public String BuyerCity { get; set; }
        public String BuyerZone { get; set; }
        public String BuyerZip { get; set; }
        public String BuyerCountry { get; set; }

        public String DeliveryLastname { get; set; }
        public String DeliveryFirstname { get; set; }
        public String DeliveryPatronymic { get; set; }
        public String DeliveryStreet { get; set; }
        public String DeliveryCity { get; set; }
        public String DeliveryZone { get; set; }
        public String DeliveryZip { get; set; }
        public String DeliveryCountry { get; set; }
    }
}
