using Food_Ordering_Application.DTO_s;
using Food_Ordering_Application.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Food_Ordering_Application.Services
{
    public class CartServices:ICartRepo

    {
        private readonly FlashFoodsContext _context;
        private readonly ILogger<CartServices> _logger;
        public CartServices(FlashFoodsContext context, ILogger<CartServices> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<CartItems>> GetUserCart(string userId)
        {
            var cart=await _context.CartItems.Where(c=>c.UserId == userId).Include(c=>c.MenuItem).Include(c=>c.User).ToListAsync();
            return cart;

        }
        public async Task<string> AddItemToCart(CartDto cartItem)
        {
            try
            {
                var cartItemToAdd = new CartItems
                {
                    MenuItemId = cartItem.menuItemId,
                    UserId = cartItem.UserId,
                    Quantity = cartItem.quantity
                   
                };

                _context.CartItems.Add(cartItemToAdd);
                await _context.SaveChangesAsync();

                return "Added to cart successfully";
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError($"Error adding item to cart: {ex.Message}", ex);
                return "Failed to add item to cart";
            }

        }
        public async Task<string> RemoveItemFromCart(int cartItemId)
        {
            // Find the entity object to remove based on the DTO
            var cartEntity = await _context.CartItems
                                         .FirstOrDefaultAsync(c=>c.CartItemId==cartItemId);
            if (cartEntity == null)
            {
                return "Item not found in cart";
            }

            // Remove the entity object from the context and save changes
            _context.CartItems.Remove(cartEntity);
            await _context.SaveChangesAsync();

            return "Removed from cart";
        }
        public async Task<string> UpdateCartItem(CartDto cartItem)
        {
            var itemToUpdate= await _context.CartItems.Where(c=>c.MenuItemId==cartItem.menuItemId && c.UserId==cartItem.UserId).FirstOrDefaultAsync();
            if (itemToUpdate == null)
            {
                return "Cart item not found";
            }

            itemToUpdate.Quantity = cartItem.quantity;
            _context.CartItems.Update(itemToUpdate);
            await _context.SaveChangesAsync();

            return "cart item updated";
        }
    }
}
