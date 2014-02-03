using System;

namespace effectcup.KaznacheyPayment
{
    /// <summary>
    /// PaymentResponse represent information about payment processing
    /// </summary>
    public class PaymentResponse : BaseResponse
    {
        /// <summary>
        /// Merchant cms internal payment id
        /// </summary>
        public String MerchantInternalPaymentId { get; set; }

        /// <summary>
        /// Merchant cms user internal id 
        /// </summary>
        public String MerchantInternalUserId { get; set; }

        /// <summary>
        /// Order total sum
        /// </summary>
        public Decimal Sum { get; set; }

        /// <summary>
        /// Merchant cms custom merchant info
        /// </summary>
        public String CustomMerchantInfo { get; set; }

        /// <summary>
        /// Signature. Shoud be validated. Sample
        /// 
        ///             request.ErrorCode.ToString(CultureInfo.InvariantCulture)
        ///              + (request.MerchantInternalPaymentId==null?
        ///                     String.Empty:request.MerchantInternalPaymentId.ToString(CultureInfo.InvariantCulture))
        ///              + (request.MerchantInternalUserId==null?
        ///                     String.Empty:request.MerchantInternalUserId.ToString(CultureInfo.InvariantCulture))
        ///              + request.Sum.ToString("0.00",CultureInfo.InvariantCulture);
        ///              + request.CustomMerchantInfo
        ///              + MerchantSecretKey.ToString().ToUpper();
        /// </summary>
        public String Signature { get; set; }
    }
}
