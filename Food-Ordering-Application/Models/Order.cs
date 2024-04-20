namespace Food_Ordering_Application.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }

        // Foreign key to link with User
        public string? UserId { get; set; }
        public User? User { get; set; } // Navigation property

        // Foreign key to link with Restaurant
        public int RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; } // Navigation property

        // Navigation property to access the order details
        public ICollection<OrderDetails>? OrderDetails { get; set; }

        public decimal? TotalPrice { get; set; } // Total price of the order

        public int PaymentId { get; set; }
        public Payment? Payment { get; set; }
        public string? PaymentStatus { get; set; } // Payment status of the order (e.g., "Pending", "Paid")

        public string? DeliveryStatus { get; set; } // Delivery status of the order (e.g., "Pending", "Delivered")

        public bool ViewedByRestaurant { get; set; } // Indicates whether the order has been viewed by the restaurant
    }
}
