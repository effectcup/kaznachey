using System.Collections.Generic;

namespace effectcup.KaznacheyPayment
{
    public class PaymentRequest
    {
        public PaymentRequest(int selectedPaySystemId)
        {
            SelectedPaySystemId = selectedPaySystemId;
        }

        public int SelectedPaySystemId { get; set; }

        public List<Product> Products { get; set; }

        public List<Field> Fields { get; set; }

        public PaymentDetails PaymentDetails { get; set; }
    }
}
