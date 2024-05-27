using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Food_Ordering_Application.Models
{
    public class CartItems
    {
        [Key]
        public int CartItemId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int MenuItemId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [ForeignKey("MenuItemId")]
        public virtual MenuItem MenuItem { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
