using Microsoft.EntityFrameworkCore;
using SocialNetwork.Models;
using SocialNetwork.Models.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Data.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Location> _locations;

        public LocationRepository(ApplicationDbContext context)
        {
            _context = context;
            _locations = context.Locations;
        }

        public void Add(Location loc)
        {
            _locations.Add(loc);
        }

        public void Delete(Location loc)
        {
            _locations.Remove(loc);
        }

        public IEnumerable<Location> GetAll()
        {
            return _locations.ToList();
        }

        public Location GetBy(int locationID)
        {
            return _locations.SingleOrDefault(l => l.LocationID == locationID);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
