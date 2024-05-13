namespace Food_Ordering_Application.Models
{
    public class UserFavoriteMenuItems
    {
        public int UserFavoriteMenuItemId { get; set; }
        public string? Id { get; set; }
        public User? User { get; set; }
        public int MenuItemId {  get; set; }
        public MenuItem? MenuItem { get; set; }
    }
}
