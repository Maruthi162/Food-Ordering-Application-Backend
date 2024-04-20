namespace Food_Ordering_Application.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public int Rating {  get; set; }
        public string? Comment { get; set; }
    }
}
