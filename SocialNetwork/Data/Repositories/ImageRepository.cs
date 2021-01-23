using Microsoft.EntityFrameworkCore;
using SocialNetwork.Models;
using SocialNetwork.Models.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Data.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Image> _images;

        public ImageRepository(ApplicationDbContext context)
        {
            _context = context;
            _images = context.Images;
        }
        public void Add(Image img)
        {
            _images.Add(img);
        }

        public void Delete(Image img)
        {
            _images.Remove(img);
        }

        public IEnumerable<Image> GetAll()
        {
            return _images.ToList();
        }

        public Image GetBy(int imageID)
        {
            return _images.SingleOrDefault(i => i.ImageID == imageID);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
