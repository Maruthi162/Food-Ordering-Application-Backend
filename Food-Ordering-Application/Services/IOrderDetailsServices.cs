using Food_Ordering_Application.Models;

namespace Food_Ordering_Application.Services
{
    public interface IOrderDetailsServices
    {
        public Task<IEnumerable<OrderDetails>> GetOrderAllDetailsAsync();
        public Task<IEnumerable<OrderDetails>> GetOrderDetailsByOrderIdAsync(int orderId);
        
        public Task<string> RemoveOrderDetails(int orderDetailsId);

    }
}
