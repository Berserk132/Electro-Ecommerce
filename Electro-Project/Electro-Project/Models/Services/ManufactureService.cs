using Electro_Project.Models.Context;

namespace Electro_Project.Models.Services
{
    public class ManufactureService : IManufactureService
    {

        public ShopContext context { get; set; }

        public ManufactureService(ShopContext _context)
        {
            context = _context;
        }

        public void Add(Manufacturer _laptop)
        {
            context.Add(_laptop);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Manufacturer? manufacture = context.Manufacturer.FirstOrDefault(x => x.Id == id);
            context.Remove(manufacture);
            context.SaveChanges();
        }

        public IEnumerable<Manufacturer> GetAll()
        {

            return context.Manufacturer
                    .ToList();
        }

        public Manufacturer GetById(int id)
        {
            Manufacturer? manufacture = context.Manufacturer
                .FirstOrDefault(m => m.Id == id);

            return manufacture;
        }

        public Manufacturer Update(int id, Manufacturer _manufacture)
        {
            context.Update(_manufacture);
            context.SaveChanges();

            return _manufacture;
        }
    }
}
