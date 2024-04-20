namespace Food_Ordering_Application.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string? UserId { get; set; } // Foreign key to link with User
        public User? User { get; set; } // If you want to store user addresses as well
        public int? RestaurantId { get; set; } // Foreign key to link with Restaurant
        public Restaurant? Restaurant { get; set; } // Navigation property
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
    }
}
