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
        public void Update(User user)
        {
            _users.Update(user);
        }

        public IEnumerable<User> GetAll()
        {
            return _users
                .Include(u=>u.Location)
                .ToList();
        }

        public User GetBy(int userID)
        {
            return _users
                .Include(u => u.Location)
                .SingleOrDefault(u => u.UserID == userID);
        }
        public User GetByEmail(string email)
        {
            return _users
                .Include(u => u.Location)
                .SingleOrDefault(u => u.Email == email);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
