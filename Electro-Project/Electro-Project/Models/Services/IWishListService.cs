namespace Electro_Project.Models.Services
{
    public interface IWishListService
    {

        IEnumerable<WishList> GetAll();

        WishList GetById(int id);

        void Add(WishList _wishList);

        WishList Update(int id, WishList _wishList);

        void Delete(int id);
    }
}
