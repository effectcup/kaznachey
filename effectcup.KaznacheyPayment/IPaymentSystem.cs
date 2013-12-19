namespace effectcup.KaznacheyPayment
{
    public interface IPaymentSystem
    {
        MerchantInformation GetMerchantInformation();
        PaymentResponse CreatePayment(PaymentRequest payment);
    }
}
