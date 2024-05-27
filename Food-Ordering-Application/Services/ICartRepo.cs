using Food_Ordering_Application.DTO_s;
using Food_Ordering_Application.Models;

namespace Food_Ordering_Application.Services
{
    public interface ICartRepo
    {
        public Task<IEnumerable<CartItems>> GetUserCart(string userId);
        public Task<string> AddItemToCart(CartDto cartItem);
        public Task<string> RemoveItemFromCart(CartDto cartItem);
        public Task<string> UpdateCartItem(CartDto cartItem);
    }
}
