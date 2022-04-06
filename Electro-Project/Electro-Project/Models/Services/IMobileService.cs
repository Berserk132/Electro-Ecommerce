namespace Electro_Project.Models.Services
{
    public interface IMobileService
    {

        IEnumerable<Mobile> GetAll();

        Mobile GetById(int id);

        void Add(Mobile _mobile);

        Mobile Update(int id, Mobile _mobile);

        void Delete(int id);
    }
}
