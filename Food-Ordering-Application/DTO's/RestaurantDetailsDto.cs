namespace Food_Ordering_Application.DTO_s
{
    public class RestaurantDetailsDto
    {
        public int RestaurantId { get; set; }
        public string? RestaurantName { get; set; }
        public string? RestaurantDescription { get; set; }

        public string? PhoneNum {  get; set; }   
        public bool IsFavorite {  get; set; }
        public string? url { get; set; }

        
    }
}
