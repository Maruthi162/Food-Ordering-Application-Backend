using Food_Ordering_Application.Authentication;
using Food_Ordering_Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Food_Ordering_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly FlashFoodsContext _context;
        private readonly ILogger<OrdersController> _logger;

        public UserDetailsController(FlashFoodsContext context, ILogger<OrdersController> logger)
        {
            _context = context;
            _logger = logger;
        }

       /* [HttpGet("MyProfile")]
        public async Task<IActionResult> GetUserDetailsByUserID(string userId)
        {
            var userExists= await _context.Users.FindAsync(userId);
            if(userExists == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                   new ResponseMessage { status = "Error", Message = "User Not Found" });
            }

            Address address = await _context.Users.
        }*/
    }
}
