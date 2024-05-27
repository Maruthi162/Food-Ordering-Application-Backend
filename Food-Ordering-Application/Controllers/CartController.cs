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
            var cItem=await _context.CartItems.Where(c=>c.MenuItemId==cartItem.menuItemId && c.UserId==cartItem.UserId).FirstOrDefaultAsync();
            if(cItem != null) 
            {
                return BadRequest(new { message = "Item already added to cart" });
            }
            _cartRepo.AddItemToCart(cartItem);

            return Ok(new { message = "Added to cart" });
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
        public async Task<ActionResult> RenoveFromCart(CartDto cartItem)
        {
            var ci = await _context.CartItems.FirstOrDefaultAsync(c => c.MenuItemId==cartItem.menuItemId && c.UserId == cartItem.UserId);
            if (ci == null)
            {
                return BadRequest(new { message = "item not exists" });
            }
            var msg=await _cartRepo.RemoveItemFromCart(cartItem);

            return Ok(new {message=msg});
        }
    }
}
