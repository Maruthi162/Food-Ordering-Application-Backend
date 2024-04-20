namespace Food_Ordering_Application.Models
{
    public class Payment
    {
        public int? PaymentId { get; set; }
        public string UserId { get; set; }
        public User? User { get; set; }
        public decimal Amount { get; set; }
        public string? PaymentMethod {  get; set; }  
        public string? TransactionId {  get; set; }

        public DateTime PaymentDate { get; set; }
        public string? status { get; set; }
    }
}
