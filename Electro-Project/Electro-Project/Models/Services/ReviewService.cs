using Electro_Project.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Electro_Project.Models.Services
{
    public class ReviewService : IReviewService
    {

        public ShopContext context { get; set; }

        public ReviewService(ShopContext _context)
        {
            context = _context;
        }

        public void Add(Review _review)
        {
            context.Add(_review);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Review review = context.Reviews.FirstOrDefault(x => x.Id == id);
            context.Remove(review);
            context.SaveChanges();
        }

        public IEnumerable<Review> GetAll()
        {
            return context.Reviews
                    .ToList();
        }

        public Review GetById(int id)
        {
            Review? review = context.Reviews
                .Include(r => r.User)
                .FirstOrDefault(m => m.Id == id);

            return review;
        }

        public Review Update(int id, Review _review)
        {
            context.Update(_review);
            context.SaveChanges();

            return _review;
        }
    }
}
