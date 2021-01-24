using Microsoft.EntityFrameworkCore;
using SocialNetwork.Models;
using SocialNetwork.Models.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Event> _events;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
            _events = context.Events;
        }
        public void Add(Event ev)
        {
            _events.Add(ev);
        }

        public void Delete(Event ev)
        {
            _events.Remove(ev);
        }

        public IEnumerable<Event> GetAll()
        {
            return _events.Include(e => e.Location)
                .Include(e => e.UsersGoing)
                .Include(e=>e.Messages)
                .Include(e => e.Founder).ToList();
        }

        public Event GetBy(int eventID)
        {
            return _events.Include(e => e.Location)
                .Include(e => e.UsersGoing)
                .Include(e => e.Messages)
                .Include(e => e.Founder).SingleOrDefault(e => e.EventID == eventID);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
