using Electro_Project.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Electro_Project.Models.Services
{
    public class MediaService : IMediaService
    {
        public ShopContext context { get; set; }

        public MediaService(ShopContext _context)
        {
            context = _context;
        }

        public void Add(Media _media)
        {
            context.Add(_media);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Media media = context.Medias.FirstOrDefault(x => x.Id == id);
            context.Remove(media);
            context.SaveChanges();
        }

        public IEnumerable<Media> GetAll()
        {

            return context.Medias
                    .ToList();
        }

        public Media GetById(int id)
        {
            Media? media = context.Medias
                .FirstOrDefault(m => m.Id == id);

            return media;
        }

        public Media Update(int id, Media _media)
        {
            context.Update(_media);
            context.SaveChanges();

            return _media;
        }
    }
}
