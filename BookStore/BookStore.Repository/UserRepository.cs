using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class UserRepository
    {
        BookStoreContext _context = new BookStoreContext();

        public List<User> GetUsers()
        {
           return  _context.Users.ToList();

        }

        public User Login(LoginModel model)
        {
            return _context.Users.FirstOrDefault(c=> c.Email.Equals(model.Email.ToLower()) && c.Password.Equals(model.Password));


        }
        public ListResponse<Role> Roles()
        {

            var query = _context.Roles.AsQueryable();
            int totalReocrds = query.Count();
            IEnumerable<Role> categories = query;

            return new ListResponse<Role>()
            {
                Results = categories,
                TotalRecords = totalReocrds,
            };
        }

        public User Register(RegisterModel model)
        {
            User user = new User()
            {
                Email = model.Email,
                Password = model.Password,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Roleid = model.Roleid,

            };
            var entry = _context.Users.Add(user);
            _context.SaveChanges();
            return entry.Entity;
        }
    }
}
