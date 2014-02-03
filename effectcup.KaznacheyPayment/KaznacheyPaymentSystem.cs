using System;
using System.Globalization;
using RestSharp;

namespace effectcup.KaznacheyPayment
{
    /// <summary>
    /// Kaznachey service client implementation
    /// </summary>
    public class KaznacheyPaymentSystem : IPaymentSystem
    {
        public const string ApiUrl = "http://payment.kaznachey.net/api/PaymentInterface";

        private readonly Guid _merchantGuid;
        private readonly String _merchantSecretKey;
        private readonly String _baseUrl;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="merchantGuid">Merchant GUID</param>
        /// <param name="merchantSecretKey">Merchant secret key</param>
        public KaznacheyPaymentSystem(Guid merchantGuid, string merchantSecretKey)
            : this(merchantGuid, merchantSecretKey, ApiUrl) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="merchantGuid">Merchant GUID</param>
        /// <param name="merchantSecretKey">Mechant secret key</param>
        /// <param name="apiUrl">Api url</param>
        private KaznacheyPaymentSystem(Guid merchantGuid, String merchantSecretKey, String apiUrl)
        {
            _merchantGuid = merchantGuid;
            _merchantSecretKey = merchantSecretKey;
            _baseUrl = apiUrl;
        }

        /// <summary>
        /// Gets merchants payment system information
        /// </summary>
        /// <returns>MerchantInfoResponse represents payment systems information</returns>
        public MerchantInfoResponse GetMerchantInformation()
        {
            var signature = (_merchantGuid.ToString("D") + _merchantSecretKey).ToUpper().GetMd5Hash();
            var request = CreateRequest("GetMerchatInformation");
            request.AddParameter("MerchantGuid", _merchantGuid);
            request.AddParameter("Signature", signature);

            return Execute<MerchantInfoResponse>(request);
        }

        private static IRestRequest CreateRequest(string resource)
        {
            var request = new RestRequest
            {
                Resource = resource,
                Method = Method.POST,
                RequestFormat = DataFormat.Json,
                JsonSerializer = { ContentType = "application/json; charset=utf-8" }
            };
            return request;
        }

        private T Execute<T>(IRestRequest request) where T : new()
        {
            var client = new RestClient(_baseUrl);

            var response = client.Execute<T>(request);

            if (response.ErrorException == null)
                return response.Data;
            const string message = "Error retrieving response. Check inner details for more info.";
            throw new ApplicationException(message, response.ErrorException);
        }

        /// <summary>
        /// Send request create payment 
        /// </summary>
        /// <param name="payment">Payment information</param>
        /// <returns>CreatePaymentResponse represent object from service</returns>
        public CreatePaymentResponse CreatePayment(PaymentRequest payment)
        {
            var signature = CalculatePaymentSignature(payment);

            var request = CreateRequest("CreatePayment");
            request.AddBody(new
            {
                payment.SelectedPaySystemId,
                payment.Products,
                PaymentDetails = payment.PaymentDetail,
                MerchantGuid = _merchantGuid,
                Signature = signature
            });

            return Execute<CreatePaymentResponse>(request);
        }

        /// <summary>
        /// Check responce from Kaznachey server api
        /// </summary>
        /// <param name="responce">Payment responce object</param>
        /// <returns>True if signature valid, false - otherwise</returns>
        public Boolean ValidateResponse(PaymentResponse responce)
        {
            var sourceStr = responce.ErrorCode.ToString(CultureInfo.InvariantCulture)
                 + (responce.MerchantInternalPaymentId == null ?
                 String.Empty : responce.MerchantInternalPaymentId.ToString(CultureInfo.InvariantCulture))
                 + (responce.MerchantInternalUserId == null ?
                 String.Empty : responce.MerchantInternalUserId.ToString(CultureInfo.InvariantCulture))
                 + responce.Sum.ToString("F2", CultureInfo.InvariantCulture)
                 + responce.CustomMerchantInfo
                 + _merchantSecretKey.ToUpper();
            return sourceStr.GetMd5Hash() == responce.Signature;
        }

        private string CalculatePaymentSignature(PaymentRequest payment)
        {
            var totalSumm = decimal.Zero;
            var productsCount = 0.0m;

            foreach (var product in payment.Products)
            {
                totalSumm += product.ProductPrice * product.ProductItemsNum;
                productsCount += product.ProductItemsNum;
            }

            var signatureString = _merchantGuid.ToString("D").ToUpper() +
                                  totalSumm.ToString("F2", NumberFormatInfo.InvariantInfo) +
                                  productsCount.ToString("F2", NumberFormatInfo.InvariantInfo) +
                                  payment.PaymentDetail.MerchantInternalUserId +
                                  payment.PaymentDetail.MerchantInternalPaymentId +
                                  payment.SelectedPaySystemId + _merchantSecretKey;

            return signatureString.GetMd5Hash();
        }
    }
}
