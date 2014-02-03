namespace effectcup.KaznacheyPayment
{
    /// <summary>
    /// Represent responce from CreatePayment operation
    /// </summary>
    public class CreatePaymentResponse : BaseResponse
    {
        /// <summary>
        /// Base64encoded html form with redirection. Hiden.
        /// </summary>
        public string ExternalForm { get; set; }
    }
}
