namespace Electro_Project.Models.Services
{
    public interface IReviewService
    {

        IEnumerable<Review> GetAll();

        Review GetById(int id);

        void Add(Review _review);

        Review Update(int id, Review _review);

        void Delete(int id);
    }
}
