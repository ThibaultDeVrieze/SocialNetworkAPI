using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models.IRepositories
{
    public interface IImageRepository
    {
        Image GetBy(int imageID);
        IEnumerable<Image> GetAll();
        void Add(Image img);
        void Delete(Image img);
        void SaveChanges();
    }
}
