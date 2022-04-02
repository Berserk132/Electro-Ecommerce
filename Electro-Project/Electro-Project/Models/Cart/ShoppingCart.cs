using Electro_Project.Models;
using Electro_Project.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Electro_Project.Models.Cart
{
    public class ShoppingCart
    {
        public ShopContext _context { get; set; }

        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        // ctor
        public ShoppingCart(ShopContext context)
        {
            _context = context;
        }


        // function to create the session
        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<ShopContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId, ShoppingCartItems = ShoppingCart.GetShoppingCartItems(cartId, context) };
        }


        // Add New Item in SHooping Cart
        public void AddItemToCart(Product product)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Product.Id == product.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Amount = 1
                };

                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }


        // Remove Item in SHooping Cart
        public void RemoveItemFromCart(Product product)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Product.Id == product.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _context.SaveChanges();
        }

        // Get All items
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.Product).ToList());
        }


        public static List<ShoppingCartItem> GetShoppingCartItems(string id, ShopContext _context)
        {
            List<ShoppingCartItem> ShoppingCartItems = _context.ShoppingCartItems
                .Where(n => n.ShoppingCartId == id)
                .Include(n => n.Product)
                .ThenInclude(p => p.Media)
                .ToList();
            
            return ShoppingCartItems ;
        }

        // Get Total of Shopping cart
        public double GetShoppingCartTotal() => _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => (double)n.Product.Price * n.Amount).Sum();


        // Delete all in Shooping Cart
        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
