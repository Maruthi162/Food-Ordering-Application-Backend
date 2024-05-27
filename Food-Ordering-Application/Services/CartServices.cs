using Food_Ordering_Application.DTO_s;
using Food_Ordering_Application.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Food_Ordering_Application.Services
{
    public class CartServices:ICartRepo

    {
        private readonly FlashFoodsContext _context;
        public CartServices(FlashFoodsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartItems>> GetUserCart(string userId)
        {
            var cart=await _context.CartItems.Where(c=>c.UserId == userId).Include(c=>c.MenuItem).Include(c=>c.User).ToListAsync();
            return cart;

        }
        public async Task<string> AddItemToCart(CartDto cartItem)
        {
            var cartItemToAdd = new CartItems
            {
                MenuItemId = cartItem.menuItemId,
                UserId=cartItem.UserId,
                Quantity=cartItem.quantity
                
            };
            _context.CartItems.Add(cartItemToAdd);
            await _context.SaveChangesAsync();
            return "Added to cart successfully";

        }
        public async Task<string> RemoveItemFromCart(CartDto cartItem)
        {
            // Find the entity object to remove based on the DTO
            var cartEntity = await _context.CartItems
                                         .FirstOrDefaultAsync(c => c.UserId == cartItem.UserId && c.MenuItemId == cartItem.menuItemId);
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
