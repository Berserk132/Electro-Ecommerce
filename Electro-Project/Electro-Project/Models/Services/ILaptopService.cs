namespace Electro_Project.Models.Services
{
    public interface ILaptopService
    {

        IEnumerable<Laptop> GetAll();

        Laptop GetById(int id);

        void Add(Laptop _laptop);

        Laptop Update(int id, Laptop _laptop);

        void Delete(int id);
    }
}
