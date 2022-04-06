namespace Electro_Project.Models.Services
{
    public interface IMediaService
    {

        IEnumerable<Media> GetAll();

        Media GetById(int id);

        void Add(Media _media);

        Media Update(int id, Media _media);

        void Delete(int id);
    }
}
