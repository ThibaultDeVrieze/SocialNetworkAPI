using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models.IRepositories
{
    public interface ILocationRepository
    {
        Location GetBy(int locationID);
        IEnumerable<Location> GetAll();
        void Add(Location loc);
        void Delete(Location loc);
        void SaveChanges();
    }
}
