namespace Food_Ordering_Application.DTO_s
{
    public class OrderDto
    {
        public DateTime? OrderDate { get; set; }
        public string? userId {  get; set; }
        public int RestaurantId { get; set; }
        public decimal? TotalPrice { get; set; }
        public int PaymentId { get; set; }
        public string? PaymentStatus { get; set; }
        public string? DeliveryStatus { get; set; }
        public bool ViewedByRestaurant { get; set; }

    }
}
