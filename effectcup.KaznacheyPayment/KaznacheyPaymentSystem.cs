using System;
using System.Globalization;
using RestSharp;

namespace effectcup.KaznacheyPayment
{
    public class KaznacheyPaymentSystem : IPaymentSystem
    {
        public const string ApiUrl = "http://payment.kaznachey.net/api/PaymentInterface";

        private readonly Guid merchantGuid;
        private readonly string merchantSecretKey;
        private readonly string baseUrl;

        public KaznacheyPaymentSystem(Guid merchantGuid, string merchantSecretKey)
            : this(merchantGuid, merchantSecretKey, ApiUrl)
        {   
        }

        public KaznacheyPaymentSystem(Guid merchantGuid, string merchantSecretKey, string apiUrl)
        {
            this.merchantGuid = merchantGuid;
            this.merchantSecretKey = merchantSecretKey;
            baseUrl = apiUrl;
        }

        public MerchantInformation GetMerchantInformation()
        {
            var signature = (merchantGuid.ToString("D") + merchantSecretKey).ToUpper().GetMd5Hash();
            var request = CreateRequest("GetMerchatInformation");
            request.AddParameter("MerchantGuid", merchantGuid);
            request.AddParameter("Signature", signature);

            return Execute<MerchantInformation>(request);
        }

        private static IRestRequest CreateRequest(string resource)
        {
            return new RestRequest
            {
                Resource = resource,
                Method = Method.POST,
                RequestFormat = DataFormat.Json
            };
        }

        private T Execute<T>(IRestRequest request) where T : new()
        {
            var client = new RestClient(baseUrl);

            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response. Check inner details for more info.";
                var kaznacheyException = new ApplicationException(message, response.ErrorException);
                throw kaznacheyException;
            }
            return response.Data;
        }

        public PaymentResponse CreatePayment(PaymentRequest payment)
        {
            var signature = CalculatePaymentSignature(payment);

            var request = CreateRequest("CreatePayment");
            request.AddBody(new
            {
                payment.SelectedPaySystemId,
                payment.Products,
                payment.Fields,
                payment.PaymentDetails,
                MerchantGuid = merchantGuid,
                Signature = signature
            });

            return Execute<PaymentResponse>(request);
        }

        private string CalculatePaymentSignature(PaymentRequest payment)
        {
            var totalSumm = decimal.Zero;
            var productsCount = 0.0;

            foreach (var product in payment.Products)
            {
                totalSumm += product.ProductPrice * (decimal)product.ProductItemsNum;
                productsCount += product.ProductItemsNum;
            }

            var signatureString = merchantGuid.ToString("D").ToUpper() +
                                  totalSumm.ToString("F2", NumberFormatInfo.InvariantInfo) +
                                  productsCount.ToString("F2", NumberFormatInfo.InvariantInfo) +
                                  payment.PaymentDetails.MerchantInternalUserId +
                                  payment.PaymentDetails.MerchantInternalPaymentId +
                                  payment.SelectedPaySystemId + merchantSecretKey;

            return signatureString.GetMd5Hash();
        }
    }
}
