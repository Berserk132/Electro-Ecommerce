namespace Electro_Project.Models.Services
{
    public interface IProductService
    {

        IEnumerable<Product> GetAll();

        Product GetById(int id);

        void Add(Product _product);

        Product Update(int id, Product _product);

        void Delete(int id);
    }
}
