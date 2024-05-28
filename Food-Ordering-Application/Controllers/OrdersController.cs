using Food_Ordering_Application.Models;
using Food_Ordering_Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Food_Ordering_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly FlashFoodsContext _context;
        private readonly ILogger<OrdersController> _logger;
        private readonly IOrderRepo _orderRepo;
        public OrdersController(FlashFoodsContext context, ILogger<OrdersController> logger, IOrderRepo orderRepo)
        {
            _context = context;
            _logger = logger;
            _orderRepo = orderRepo;
        }
        [HttpPost("place-order")]
        public async Task<IActionResult> PlaceOrder([FromQuery] string userId, string paymentMethod)
        {
            var result = await _orderRepo.PlaceOrderAsync(userId, paymentMethod);

            if (result == "Cart is empty.")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

    }
}
