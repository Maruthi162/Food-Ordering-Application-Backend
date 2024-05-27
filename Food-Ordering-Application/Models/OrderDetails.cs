namespace Food_Ordering_Application.Models
{
    public class OrderDetails
    {
        public int OrderDetailsId { get; set; }
        public int OrderId { get; set; }
        public int menuItemId {  get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public Order? Order { get; set; }
        public MenuItem? MenuItem { get; set; }
        
    }
}
