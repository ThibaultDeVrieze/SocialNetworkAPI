using Microsoft.EntityFrameworkCore;
using SocialNetwork.Models;
using SocialNetwork.Models.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Data.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Message> _messages;

        public MessageRepository(ApplicationDbContext context)
        {
            _context = context;
            _messages = context.Messages;
        }
        public void Add(Message message)
        {
            _messages.Add(message);
        }

        public void Delete(Message message)
        {
            _messages.Remove(message);
        }

        public IEnumerable<Message> GetAll()
        {
            return _messages.OrderBy(m=>m.DateTime).Reverse().ToList();
        }

        public Message GetBy(int messageID)
        {
            return _messages.SingleOrDefault(m => m.MessageID == messageID);
        }

        public void Update(Message mess)
        {
            _messages.Update(mess);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
