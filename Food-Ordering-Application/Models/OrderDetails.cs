namespace Food_Ordering_Application.Models
{
    public class OrderDetails
    {
        public int OrderDetailsId { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
