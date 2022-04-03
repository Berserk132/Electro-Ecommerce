using Electro_Project.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Electro_Project.Models.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly ShopContext context;
        public OrdersService(ShopContext _context)
        {
            context = _context;
        }

        public List<Order> GetOrdersByUserIdAndRole(string userId, string userRole)
        {
            var orders = context.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Product).Include(n => n.User).ToList();

            if (userRole != "Admin")
            {
                orders = orders.Where(n => n.UserId == userId).ToList();
            }

            return orders;
        }

        public void StoreOrder(List<ShoppingCartItem> items, string userId, string userEmailAddress, Address address)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress,
                ShippingAddress = address,
            };
            context.Orders.Add(order);
            context.SaveChanges();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    ProductId = item.Product.Id,
                    OrderId = order.Id,
                    Price = (double) item.Product.Price
                };
                context.OrderItems.Add(orderItem);
            }
            context.SaveChanges();
        }
    }
}
