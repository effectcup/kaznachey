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
            var merchantInternalPaymentId = new Guid("{36D4E2A4-81E2-4622-9F4E-7EDEF28682B9}").ToString();
            var merchantInternalUserId = new Guid("{8841F897-867D-4540-A360-A6E9C3C3DC33}").ToString();
            var data = new PaymentRequest(1)
                {
                    Products = new List<Product>
                        {
                            new Product
                                {
                                    ProductItemsNum = 1,
                                    ProductName = "Модель танка Т34-85 ",
                                    ProductId = new Guid("{0029D9E3-0ABC-40D4-BDE1-6467A83B4464}").ToString(),
                                    ProductPrice = 500
                                },
                            new Product
                                {
                                    ProductItemsNum = 2,
                                    ProductName = "Модель танка Т34-76 ",
                                    ProductId = new Guid("{6F5D38D9-18D2-4B90-8FF7-F04D47F0324C}").ToString(),
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
                    PaymentDetails = new PaymentDetails
                        {
                            MerchantInternalUserId = merchantInternalUserId,
                            MerchantInternalPaymentId = merchantInternalPaymentId
                        }
                };

            var response = kaznacheyPaymentSystem.CreatePayment(data);

            Assert.IsNotNull(response);
            Assert.AreEqual(0, response.ErrorCode);
            Assert.IsTrue(string.IsNullOrWhiteSpace(response.DebugMessage));
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.ExternalForm));
        }
    }
}
