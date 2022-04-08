using Electro_Project.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Electro_Project.Models.Services
{
    public class WishListService : IWishListService
    {

        public ShopContext context { get; set; }

        public WishListService(ShopContext _context)
        {
            context = _context;
        }

        public void Add(WishList _wishList)
        {
            context.Add(_wishList);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            WishList wishList = context.WishLists.FirstOrDefault(x => x.Id == id);
            context.Remove(wishList);
            context.SaveChanges();
        }

        public IEnumerable<WishList> GetAll()
        {
            return context.WishLists
                    .Include(W => W.User)
                    .Include(W => W.Products)
                    .ToList();
        }

        public WishList GetById(int id)
        {
            WishList? wishList = context.WishLists
                .Include(W => W.User)
                .Include(W => W.Products)
                .FirstOrDefault(m => m.Id == id);

            return wishList;
        }

        public WishList Update(int id, WishList _wishList)
        {
            context.Update(_wishList);
            context.SaveChanges();

            return _wishList;
        }
    }
}
