namespace effectcup.KaznacheyPayment
{
    public class PaymentResult
    {
        public int ErrorCode { get; set; }

        public string DebugMessage { get; set; }

        public PaymentDetails PaymentDetail { get; set; }
    }
}
