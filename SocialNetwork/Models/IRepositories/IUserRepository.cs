using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Models.IRepositories
{
    public interface IUserRepository
    {
        User GetBy(int userID);
        User GetByEmail(string email);
        IEnumerable<User> GetAll();
        void Add(User user);
        void Delete(User user);
        void Update(User user);
        void SaveChanges();
    }
}
