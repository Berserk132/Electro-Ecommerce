using Electro_Project.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Electro_Project.Models.Services
{
    public class MobileService : IMobileService
    {
        public ShopContext context { get; set; }

        public MobileService(ShopContext _context)
        {
            context = _context;
        }

        public void Add(Mobile _mobile)
        {
            context.Add(_mobile);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Mobile mobile = context.Mobiles.FirstOrDefault(x => x.Id == id);
            context.Remove(mobile);
            context.SaveChanges();
        }

        public IEnumerable<Mobile> GetAll()
        {

            return context.Mobiles.Include(l => l.Manufacturer)
                .Include(l => l.Media)
                    .ToList();
        }

        public Mobile GetById(int id)
        {
            Mobile? mobile = context.Mobiles
                .Include(l => l.Manufacturer)
                .Include(l => l.Media)
                .Include(l => l.Reviews)
                .ThenInclude(r => r.User)
                .FirstOrDefault(m => m.Id == id);

            return mobile;
        }

        public Mobile Update(int id, Mobile _mobile)
        {
            context.Update(_mobile);
            context.SaveChanges();

            return _mobile;
        }
    }
}
