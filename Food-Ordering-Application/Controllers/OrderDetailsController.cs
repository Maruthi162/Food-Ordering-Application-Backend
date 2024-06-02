using Food_Ordering_Application.Models;
using Food_Ordering_Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;

namespace Food_Ordering_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {

        private readonly FlashFoodsContext _context;
        private readonly ILogger<OrderDetailsController> _logger;
        private readonly IOrderDetailsServices _orderDetailsServices;
        public OrderDetailsController(FlashFoodsContext context, ILogger<OrderDetailsController> logger, IOrderDetailsServices orderDetailsServices)
        {
            _context = context;
            _logger = logger;
            _orderDetailsServices = orderDetailsServices;
        }
        [HttpGet]
        [Route("get-all-orderDetails")]
        public async Task<ActionResult<IEnumerable<OrderDetails>>> GetAllOrderDetails([FromQuery] int orderId)
        {
            try
            {
                var orders = await _orderDetailsServices.GetOrderAllDetailsAsync();
                if (orders == null || orders.Count() == 0)
                {
                    _logger.LogInformation("No orders found.");
                    return NotFound(new { message = "No orders found." });
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all orderDetails.");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet]
        [Route("get-user-Order-details")]
        public async Task<ActionResult<IEnumerable<OrderDetails>>> getAllOrderByUserId([FromQuery] int orderID)
        {
            try
            {
                var orders = await _orderDetailsServices.GetOrderDetailsByOrderIdAsync(orderID);
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
