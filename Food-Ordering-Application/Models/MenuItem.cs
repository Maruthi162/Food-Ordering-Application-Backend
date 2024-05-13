namespace Food_Ordering_Application.Models
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }

        public string? Description { get; set; }
        public string? Availability {  get; set; }
        public string? Img { get; set; }
        // Add other menu item properties as needed

        // Foreign key to link with Category
        public int CategoryId { get; set; }
        public Category? Category { get; set; } // Navigation property

        // Foreign key to link with Restaurant
        public int RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }
        public ICollection<UserFavoriteMenuItems> FavoriteMenuItems { get; set;}
    }
}
