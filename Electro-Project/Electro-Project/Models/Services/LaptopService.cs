using Electro_Project.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Electro_Project.Models.Services
{
    public class LaptopService : ILaptopService
    {
        public ShopContext context { get; set; }

        public LaptopService(ShopContext _context)
        {
            context = _context;
        }

        public void Add(Laptop _laptop)
        {
            context.Add(_laptop);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Laptop cinema = context.Laptops.FirstOrDefault(x => x.Id == id);
            context.Remove(cinema);
            context.SaveChanges();
        }

        public IEnumerable<Laptop> GetAll()
        {

            return context.Laptops.Include(l => l.Manufacturer)
                .Include(l => l.Media)
                    .ToList();
        }

        public Laptop GetById(int id)
        {
            Laptop? laptop = context.Laptops
                .Include(l => l.Manufacturer)
                .Include(l => l.Media)
                .Include(l => l.Reviews)
                .ThenInclude(r => r.User)
                .FirstOrDefault(m => m.Id == id);

            return laptop;
        }

        public Laptop Update(int id, Laptop _laptop)
        {
            context.Update(_laptop);
            context.SaveChanges();

            return _laptop;
        }
    }
}
