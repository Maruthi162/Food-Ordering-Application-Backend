namespace Food_Ordering_Application.DTO_s
{
    public class MenuItemDto
    {
        public int MenuItemId {  get; set; }
        public string? MenuItemName { get; set; }
        public decimal MenuItemPrice {  get; set; }
        public string? MenuItemDescription { get; set; }
        public string? MenuItemAvilability {  get; set; }
        public string? MenuItemUrl {  get; set; }
        public bool IsFavorite {  get; set; }

    }
}
