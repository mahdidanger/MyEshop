using MyEshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEshop.Data.Repositories
{
    public interface IUserRepository
    {
        bool IsExistUserByEmail(string email);
        void AddUser(Users user);
        Users GetUserForLogin(string email, string password);

    }

    public class UserRepository: IUserRepository
    {
        private MyEshopContext _context;
        public UserRepository(MyEshopContext context)
        {
            _context = context;
        }

        public bool IsExistUserByEmail(string email)
        {
            return _context.Users.Any(e => e.Email == email);
        }

        public void AddUser(Users user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }


        Users IUserRepository.GetUserForLogin(string email, string password)
        {
            return _context.Users
                .SingleOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}
