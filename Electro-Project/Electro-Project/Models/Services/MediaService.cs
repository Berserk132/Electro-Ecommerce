using Electro_Project.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Electro_Project.Models.Services
{
    public class MediaService : IMediaService
    {
        IWebHostEnvironment hostingEnvironment;


        public ShopContext context { get; set; }

        public MediaService(ShopContext _context, IWebHostEnvironment _hostingEnvironment)
        {
            context = _context;
            hostingEnvironment = _hostingEnvironment;
        }

        public void Add(Media _media, IFormFile file)
        {
            context.Add(_media);
            this.SaveToFile(file);

            context.SaveChanges();
        }

        public async void SaveToFile(IFormFile file)
        {
            #region ImageToFile
            string uploads = Path.Combine(hostingEnvironment.WebRootPath, "img");

            if (file.Length > 0)
            {
                string filePath = Path.Combine(uploads, file.FileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }

            #endregion
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
