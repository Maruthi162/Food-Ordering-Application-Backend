namespace Food_Ordering_Application.Models
{
    public class UserFavoriteRestaurants
    {
        public int userFavoriteRestId {  get; set; }
        public string? Id { get; set; }
        public User? User { get; set; }

        public int RestaurantId {  get; set; }
        public Restaurant? Restaurant { get; set;}
    }
}
