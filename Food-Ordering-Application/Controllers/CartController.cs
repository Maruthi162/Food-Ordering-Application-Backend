using Food_Ordering_Application.DTO_s;
using Food_Ordering_Application.Models;
using Food_Ordering_Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Food_Ordering_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly FlashFoodsContext _context;
        private readonly ILogger<CartController> _logger;
        private readonly ICartRepo _cartRepo;

        public CartController(FlashFoodsContext context, ILogger<CartController> logger, ICartRepo cartRepo)
        {
            _context = context;
            _logger = logger;
            _cartRepo = cartRepo;
        }
        [HttpGet]
        [Route("cart")]
        public async Task<ActionResult<IEnumerable<CartItems>>> GetCart(string userId)
        {
            var user = await _context.Users.Where(u=>u.Id==userId).FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest("Invalid usrId");
            }
            var cart= await _cartRepo.GetUserCart(userId);
            return Ok(cart);
        }

        [HttpPost]
        [Route("add-to-cart")]
        public async Task<ActionResult> AddToCart([FromBody] CartDto cartItem)
        {
            try
            {
                var existingCartItem = await _context.CartItems
                    .Where(c => c.MenuItemId == cartItem.menuItemId && c.UserId == cartItem.UserId)
                    .FirstOrDefaultAsync();

                if (existingCartItem != null)
                {
                    return BadRequest(new { message = "Item already added to cart" });
                }

                var result = await _cartRepo.AddItemToCart(cartItem);
                if (result != "Added to cart successfully")
                {
                    return BadRequest(new { message = result });
                }

                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError($"Error adding item to cart: {ex.Message}", ex);
                return StatusCode(500, new { message = "An error occurred while adding the item to the cart" });
            }
        }

        [HttpPatch]
        [Route("update-cart")]
        public async Task<ActionResult> UpdateCart([FromBody] CartDto cartItem)
        {
            var citem=await _context.CartItems.FirstOrDefaultAsync(c=>c.MenuItemId==cartItem.menuItemId && c.UserId==cartItem.UserId);
            if (citem != null)
            {
                var msg =await _cartRepo.UpdateCartItem(cartItem);
                return Ok(new {message= msg});
                
            }
            return BadRequest(new { message = "not foumd in cart" });
        }
        [HttpDelete]
        [Route("delete-cart")]
        public async Task<ActionResult> RenoveFromCart(int cartItemId)
        {
            var ci = await _context.CartItems.FirstOrDefaultAsync(c => c.CartItemId==cartItemId);
            if (ci == null)
            {
                return BadRequest(new { message = "item not exists" });
            }
            var msg=await _cartRepo.RemoveItemFromCart(cartItemId);

            return Ok(new {message=msg});
        }
    }
}
