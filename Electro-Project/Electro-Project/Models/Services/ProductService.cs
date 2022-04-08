using Electro_Project.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Electro_Project.Models.Services
{
    public class ProductService : IProductService
    {
        public ShopContext context { get; set; }

        public ProductService(ShopContext _context)
        {
            context = _context;
        }

        public void Add(Product _product)
        {
            context.Add(_product);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Product product = context.Products.FirstOrDefault(x => x.Id == id);
            context.Remove(product);
            context.SaveChanges();
        }

        public IEnumerable<Product> GetAll()
        {

            return context.Products
                .Include(l => l.Manufacturer)
                .Include(l => l.Media)
                .Include(l => l.Reviews)
                .ThenInclude(r => r.User)
                .ToList();
        }

        public Product GetById(int id)
        {
            Product? product = context.Products
                .Include(l => l.Manufacturer)
                .Include(l => l.Media)
                .Include(l => l.Reviews)
                .ThenInclude(r => r.User)
                .FirstOrDefault(m => m.Id == id);

            return product;
        }

        public Product Update(int id, Product _product)
        {
            context.Update(_product);
            context.SaveChanges();

            return _product;
        }
    }
}
