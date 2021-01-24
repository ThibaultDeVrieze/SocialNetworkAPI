using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models.IRepositories
{
    public interface IMessageRepository
    {
        Message GetBy(int messageID);
        IEnumerable<Message> GetAll();
        void Add(Message message);
        void Delete(Message message);
        void Update(Message message);
        void SaveChanges();
    }
}
