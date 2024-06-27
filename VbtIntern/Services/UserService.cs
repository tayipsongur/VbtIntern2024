using Microsoft.EntityFrameworkCore;
using VbtIntern.Context;
using VbtIntern.Entities.Models;

namespace VbtIntern.Services
{
    public class UserService : IUserService
    {
        private VbtContext _vbtContext;
        public UserService(VbtContext vbtContext)
        {
                _vbtContext = vbtContext;
        }
        public bool DeleteEntity(int id)
        {
            var user = _vbtContext.Users.FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                user.IsDeleted = true;
                var status =  _vbtContext.SaveChanges();

                if (status < 0)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }

        }

        public List<User> GetAllUser()
        {
            var response = _vbtContext.Users.Where(x=> x.IsDeleted == false).AsNoTracking().ToList();
            return response;
        }

        public User InsertEntity(User user)
        {
            if (user is null)
            {
                return new User();
            }

            try
            {
                _vbtContext.Users.Add(user);
                _vbtContext.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public User UpdateEntity(User user)
        {
            var updatedUser = _vbtContext.Users.FirstOrDefault(x => x.Id == user.Id);

            if (user == null)
            {
                return new User();
            }
            else
            {
                updatedUser.Name = user.Name;
                updatedUser.Surname = user.Surname;
                updatedUser.Age = user.Age;

                _vbtContext.Update(updatedUser);
                var status = _vbtContext.SaveChanges();

                if (status < 0)
                {
                    return null;
                }
            }

            return updatedUser;
        }
    }
}
