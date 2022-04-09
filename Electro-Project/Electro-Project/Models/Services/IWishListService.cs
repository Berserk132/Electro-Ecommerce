namespace Electro_Project.Models.Services
{
    public interface IWishListService
    {
        public void AddToWishList(int PID, String UID);
        public void RemoveFromWishList(int PID, String UID);
        public IEnumerable<WishList_Product> GetByUserId(string id);
    }
}
