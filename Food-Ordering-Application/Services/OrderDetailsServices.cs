using Food_Ordering_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Food_Ordering_Application.Services
{
    public class OrderDetailsServices:IOrderDetailsServices
    {
        private readonly FlashFoodsContext _context;
        private readonly ILogger<OrderDetailsServices> _logger;
        public OrderDetailsServices(FlashFoodsContext context, ILogger<OrderDetailsServices> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IEnumerable<OrderDetails>> GetOrderAllDetailsAsync()
        {
            var ordrDetails=await _context.OrderDetails.ToListAsync();
            return ordrDetails;
        }
        public async Task<IEnumerable<OrderDetails>> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            var Orderdetails_of_orderId=await _context.OrderDetails.Where(od=>od.OrderId==orderId).ToListAsync();
            return Orderdetails_of_orderId;
        }
       
        public async Task<string> RemoveOrderDetails(int orderDetailsId)
        {
            var OrderDetailsToRemove=await _context.OrderDetails.FirstOrDefaultAsync(od=>od.OrderDetailsId==orderDetailsId);
            if (OrderDetailsToRemove == null)
            {
                return "Order details not found with the provided orderDetailsid";
            }
            _context.OrderDetails.Remove(OrderDetailsToRemove);
           await  _context.SaveChangesAsync();
            return "OrderDetails Removed successfully";
        }
    }
}
