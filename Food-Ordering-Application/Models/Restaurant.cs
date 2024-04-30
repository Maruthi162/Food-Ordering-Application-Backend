namespace Food_Ordering_Application.Models
{
    public class Restaurant
    {
       
        public int RestaurantId { get; set; }
        public string? Name { get; set; }

        public string? Description { get; set; }
        public string? PhoneNum {  get; set; }
        public string? Imgurl {  get; set; }

        public string? UserId { get; set; } // Foreign key

        // Navigation property
        public User? Owner { get; set; } // Reference to the owner (User)

        // Add other restaurant properties as needed
        public ICollection<MenuItem> MenuItems { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Order> Orders { get; set; } // Collection of orders for the restaurant
        public ICollection<Address> Addresses { get; set; }
    }
}
