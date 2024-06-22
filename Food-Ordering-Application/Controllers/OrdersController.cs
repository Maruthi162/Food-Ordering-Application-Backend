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
        [HttpPost]
        [Route("place-order")]
        public async Task<IActionResult> PlaceOrder([FromQuery] string userId, string paymentMethod)
        {
            var result = await _orderRepo.PlaceOrderAsync(userId, paymentMethod);

            if (result == "Cart is empty.")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("get-all-orders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrder()
        {
            try
            {
                var orders = await _orderRepo.GetAllOrders();
                if (orders == null || orders.Count() == 0)
                {
                    _logger.LogInformation("No orders found.");
                    return NotFound(new { message = "No orders found." });
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all orders.");
                return StatusCode(500, "Internal server error");
            }

        }
        //getting orders of the user by providing the user, we will get the userId when user logins we can get that from auth services in angular
        [HttpGet]
        [Route("get-orders-by-userId")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersbyUserId( [FromQuery] string userId)
        {
            try
            {
                var orders = await _orderRepo.GetOrdersByUserId(userId);
                if (orders == null || orders.Count() == 0)
                {
                    _logger.LogInformation("No orders found.");
                    return NotFound(new { message = "No orders found." });
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all orders.");
                return StatusCode(500, "Internal server error");
            }

        }
            



    }
}
