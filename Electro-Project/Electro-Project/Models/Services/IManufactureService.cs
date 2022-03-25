namespace Electro_Project.Models.Services
{
    public interface IManufactureService
    {

        IEnumerable<Manufacturer> GetAll();

        Manufacturer GetById(int id);

        void Add(Manufacturer _manufacture);

        Manufacturer Update(int id, Manufacturer _manufacture);

        void Delete(int id);
    }
}
