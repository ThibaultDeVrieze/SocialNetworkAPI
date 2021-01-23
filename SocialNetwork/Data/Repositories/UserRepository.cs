using Microsoft.EntityFrameworkCore;
using SocialNetwork.Models;
using SocialNetwork.Models.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<User> _users;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            _users = context.Users;
        }
        public void Add(User user)
        {
            _users.Add(user);
        }

        public void Delete(User user)
        {
            _users.Remove(user);
        }

        public IEnumerable<User> GetAll()
        {
            return _users.Include(u=>u.Messages)
                .Include(u=>u.Events)
                .Include(u=>u.Location)
                .Include(u=>u.Images)
                .ToList();
        }

        public User GetBy(int userID)
        {
            return _users.Include(u => u.Messages)
                .Include(u => u.Events)
                .Include(u => u.Location)
                .Include(u => u.Images)
                .SingleOrDefault(u => u.UserID == userID);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
