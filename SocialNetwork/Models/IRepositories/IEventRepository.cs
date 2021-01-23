using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models.IRepositories
{
    public interface IEventRepository
    {
        Event GetBy(int eventID);
        IEnumerable<Event> GetAll();
        void Add(Event ev);
        void Delete(Event ev);
        void SaveChanges();
    }
}
