using System;

namespace effectcup.KaznacheyPayment
{

    public interface IPaymentSystem
    {
        MerchantInfoResponse GetMerchantInformation();

        CreatePaymentResponse CreatePayment(PaymentRequest payment);

        Boolean ValidateResponse(PaymentResponse resp);
    }
}
