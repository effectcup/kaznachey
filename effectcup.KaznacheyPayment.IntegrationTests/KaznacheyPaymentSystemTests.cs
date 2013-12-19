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

        private KaznacheyPaymentSystem kaznacheyPaymentSystem;

        [TestInitialize]
        public void InitializeContext()
        {
            kaznacheyPaymentSystem = new KaznacheyPaymentSystem(MerchantGuid, MerchantSecretKey);
        }

        [TestMethod]
        public void GetMerchantInformation()
        {
            var information = kaznacheyPaymentSystem.GetMerchantInformation();

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
            var data = new PaymentRequest(1)
                {
                    Products = new List<Product>
                        {
                            new Product
                                {
                                    ProductItemsNum = 1,
                                    ProductName = "Модель танка Т34-85 ",
                                    ProductId = "123",
                                    ProductPrice = 500
                                },
                            new Product
                                {
                                    ProductItemsNum = 2,
                                    ProductName = "Модель танка Т34-76 ",
                                    ProductId = "124",
                                    ProductPrice = 400
                                }
                        },
                    Fields = new List<Field>
                        {
                            new Field
                                {
                                    FieldTag = "E-Mail",
                                    FieldValue = "somemail@gmail.com"
                                }
                        },
                    PaymentDetails = new PaymentDetails { MerchantInternalUserId ="21", MerchantInternalPaymentId = "1234"}
                };

            var response = kaznacheyPaymentSystem.CreatePayment(data);

            Assert.IsNotNull(response);
            Assert.AreEqual(0, response.ErrorCode);
            Assert.IsTrue(string.IsNullOrWhiteSpace(response.DebugMessage));
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.ExternalForm));
        }
    }
}
