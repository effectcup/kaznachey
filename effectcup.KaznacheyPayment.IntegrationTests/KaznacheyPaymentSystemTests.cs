using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace effectcup.KaznacheyPayment.IntegrationTests
{
    [TestClass]
    public class KaznacheyPaymentSystemTests
    {
        private static readonly Guid MerchantGuid = new Guid("36432BD4-F1F3-4C51-A063-B3F352A1348C");
        private const string MerchantSecretKey = "D65BFFE8-B7B2-4CBF-B54B-6F76A611621E";
        private IPaymentSystem _kaznacheyPaymentSystem;

        const String MerchantInternalPaymentId = "36D4E2A4-81E2-4622-9F4E-7EDEF28682B9";
        const String MerchantInternalUserId = "8841F897-867D-4540-A360-A6E9C3C3DC33";
        const Int32 PaymentSystemId = 7;

        [TestInitialize]
        public void InitializeContext()
        {
            _kaznacheyPaymentSystem = new KaznacheyPaymentSystem(MerchantGuid, MerchantSecretKey);
        }

        [TestMethod]
        public void GetMerchantInformation()
        {
            var information = _kaznacheyPaymentSystem.GetMerchantInformation();

            Assert.IsNotNull(information);
            Assert.AreEqual(0, information.ErrorCode);
            Assert.IsTrue(string.IsNullOrWhiteSpace(information.DebugMessage));
            Assert.IsFalse(string.IsNullOrWhiteSpace(information.TermToUse));
            Assert.IsNotNull(information.TermToUse);
            Assert.IsTrue(information.TermToUse.Length > 0);
        }

        [TestMethod]
        public void CreatePayment()
        {
            const Int32 resultErrorCode = 0;
            
            
            var data = new PaymentRequest(PaymentSystemId)
                {
                    Products = new List<Product>
                        {
                            new Product
                                {
                                    ProductItemsNum = 1.2m,
                                    ProductName = "Песок на развес",
                                    ProductId = "0029D9E3-0ABC-40D4-BDE1-6467A83B4464",
                                    ProductPrice = 500.2m
                                },
                            new Product
                                {
                                    ProductItemsNum = 2,
                                    ProductName = "Модель танка Т34-76 ",
                                    ProductId = "6F5D38D9-18D2-4B90-8FF7-F04D47F0324C",
                                    ProductPrice = 400.4m
                                }
                        },
                    PaymentDetail = new PaymentDetails{
                            MerchantInternalUserId = MerchantInternalUserId,
                            MerchantInternalPaymentId = MerchantInternalPaymentId
                        }
                };

            var response = _kaznacheyPaymentSystem.CreatePayment(data);

            Assert.IsNotNull(response);
            Assert.AreEqual(resultErrorCode, response.ErrorCode);
            Assert.IsTrue(String.IsNullOrWhiteSpace(response.DebugMessage));
            Assert.IsFalse(String.IsNullOrWhiteSpace(response.ExternalForm));
        }

        [TestMethod]
        public void CheckResponce()
        {
            var responceObject = new PaymentResponse()
            {
                CustomMerchantInfo = "Some custom merchant info",
                ErrorCode = 0,
                MerchantInternalPaymentId = MerchantInternalPaymentId,
                MerchantInternalUserId = MerchantInternalUserId,
                Signature = "b8d8f2dd2d4829e5d31a033c3687565b",
                Sum = 100500.5m
            };
            Assert.IsTrue(_kaznacheyPaymentSystem.ValidateResponse(responceObject));
        }
    }
}
