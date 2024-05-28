using Food_Ordering_Application.Models;

namespace Food_Ordering_Application.Services
{
    public interface IOrderRepo
    {
        public Task<IEnumerable<Order>> GetAllOrders();
        public Task<IEnumerable<Order>> GetOrdersByUserId(string userId);
        public Task<string> PlaceOrderAsync(string userId, string paymentMethod);
       
    }
}
