using Electro_Project.Models.Context;

namespace Electro_Project.Models.Services
{
    public class WishListService : IWishListService
    {
        public ShopContext context { get; set; }

        public WishListService(ShopContext _context)
        {
            context = _context;
        }


        public void AddToWishList(int PID, string UID)
        {
            Product ProdID = context.Products.Find(PID);
            context.WishLists_Products.Add(new WishList_Product() { PID = PID, UserID = UID });
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {

            }

        }
        public void RemoveFromWishList(int PID, String UID)
        {
            WishList_Product prod = context.WishLists_Products.First(P => P.PID == PID && P.UserID == UID);
            context.WishLists_Products.Remove(prod);
            context.SaveChanges();
        }
        public IEnumerable<WishList_Product> GetByUserId(string id)
        {
            IEnumerable<WishList_Product> wishList = context.WishLists_Products
                .Where(m => m.UserID == id);
            
            return wishList;
        }
    }
}
