using Food_Ordering_Application.DTO_s;
using Food_Ordering_Application.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Food_Ordering_Application.Services
{
    public class OrderServices:IOrderRepo
    {
        private readonly FlashFoodsContext _context;
        private readonly NotificationService _notificationService;
        public OrderServices(FlashFoodsContext context, NotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }
        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            var rdrs=await _context.Orders.ToListAsync();
            return rdrs;
        }
        public async Task<IEnumerable<Order>> GetOrdersByUserId(string userId)
        {
            var userOrders=await _context.Orders.Where(o=>o.UserId == userId).ToListAsync();
            return userOrders;
        }
        //Logic to place orders from cart
        public async Task<string> PlaceOrderAsync(string userId, string paymentMethod   )
        {
            var cartItems = await _context.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.MenuItem)
                .ToListAsync();

            if (!cartItems.Any())
            {
                return "Cart is empty.";
            }

            // Calculate total price
            decimal totalPrice = cartItems.Sum(ci => ci.MenuItem.Price * ci.Quantity);

            // Step 1: Create and save the Payment object without the TransactionId
            var payment = new Payment
            {
                Amount = totalPrice,
                UserId = userId,
                PaymentDate = DateTime.UtcNow,
                PaymentMethod = "CreditCard", // Example: set your payment method
                status = "Pending" // Initial status
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            // Get the PaymentId of the saved Payment object
            int paymentId = payment.PaymentId;

            // Step 2: Retrieve the restaurantId from the first cart item (assuming all items are from the same restaurant)
            int restaurantId = 0;
            if (cartItems.Any())
            {
                var menuItem = await _context.MenuItems
                    .Where(mi => mi.MenuItemId == cartItems.First().MenuItemId)
                    .FirstOrDefaultAsync();

                if (menuItem != null)
                {
                    restaurantId = menuItem.RestaurantId;
                }
            }

            // Step 3: Update the Payment object with the TransactionId
            // Assuming you have received the transaction ID from the payment gateway
            string transactionId = "1234567890"; // Example transaction ID

            // Retrieve the payment record
            var savedPayment = await _context.Payments.FirstOrDefaultAsync(p => p.PaymentId == paymentId);
            if (savedPayment != null)
            {
                savedPayment.TransactionId = transactionId; // Update the TransactionId

                _context.Payments.Update(savedPayment);
                await _context.SaveChangesAsync();
            }

            // Step 4: Create and save the Order object
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                TotalPrice = totalPrice,
                PaymentStatus = "Pending",
                DeliveryStatus = "Pending",
                ViewedByRestaurant = false,
                PaymentId = paymentId, // Set the PaymentId
                RestaurantId = restaurantId // Set the RestaurantId
            };

            // Save the Order object
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Step 5: Create and save OrderDetails for each item in the cart
            foreach (var cartItem in cartItems)
            {
                var orderDetails = new OrderDetails
                {
                    OrderId = order.OrderId, // Set the OrderId
                    menuItemId = cartItem.MenuItemId,
                    quantity = cartItem.Quantity,
                    price = cartItem.MenuItem.Price
                };

                _context.OrderDetails.Add(orderDetails);
            }

            await _context.SaveChangesAsync();
            // Remove items from the cart
            _context.CartItems.RemoveRange(cartItems);

            await _context.SaveChangesAsync();

            // Notify restaurant owner (simplified example)
            var restaurant = await _context.Restaurants
            .Include(r => r.Owner) // Eagerly load the Owner navigation property
            .FirstOrDefaultAsync(r => r.RestaurantId == order.RestaurantId);
            if (restaurant != null && !string.IsNullOrEmpty(restaurant.Owner.Email)) 
            {
                await _notificationService.SendEmail(
                    restaurant.Owner.Email,
                    "New Order Recieved",
                    $"You have recieved a new order with order Id {order.OrderId}"
                    );
            }

            return "Order placed successfully and email has been sent to restauramt ";
        }

        
    }
}
