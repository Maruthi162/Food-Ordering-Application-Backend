namespace Food_Ordering_Application.DTO_s
{
    public class CartDto
    {
        public int cartId;
        public string? UserId {  get; set; }

        public int menuItemId {  get; set; }
        public int quantity {  get; set; }
    }
}
