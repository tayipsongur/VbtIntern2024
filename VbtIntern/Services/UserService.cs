using Cache.RedisCache;
using Microsoft.EntityFrameworkCore;
using VbtIntern.Context;
using VbtIntern.Entities.Models;

namespace VbtIntern.Services
{
    public class UserService : IUserService
    {
        private VbtContext _vbtContext;
        private readonly IRedisCacheService _redisCacheService;
        public UserService(IServiceProvider serviceProvider)
        {
            _vbtContext = serviceProvider.GetRequiredService<VbtContext>();
            _redisCacheService = serviceProvider.GetRequiredService<IRedisCacheService>();
        }
        public bool DeleteEntity(int id)
        {
            var user = _vbtContext.Users.FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                user.IsDeleted = true;
                var status = _vbtContext.SaveChanges();

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
            var response = _vbtContext.Users.Where(x => x.IsDeleted == false).AsNoTracking().ToList();
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
                var status = _vbtContext.SaveChanges();

                if (status > 0)
                {
                    //User'ı redis'e kaydet
                    RedisSetName(user);
                    //_redisCacheService.Set("User", user);
                }
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

        public List<User> InsertUsers(List<User> users)
        {
            if (users != null)
            {

                foreach (var user in users)
                {
                    _vbtContext.Users.Add(user);
                }

                var status = _vbtContext.SaveChanges();

                if (status > 0)
                {
                    //User'ı redis'e kaydet
                    _redisCacheService.Set("UserList", users, DateTime.Now.AddMinutes(60));
                    return users;
                }
            }

            return new List<User>();
        }

        //REDİS SET
        private void RedisSetName(User user)
        {
            if (user is not null)
            {
                _redisCacheService.Set($"{user.Name}:Name", user.Name);
                _redisCacheService.Set($"{user.Name}:Surname", user.Surname);
                _redisCacheService.Set($"{user.Name}:Age", user.Age);
            }
        }
        public string GetRedisUser(string key)
        {
            string response = string.Empty;

            try
            {
                if (key is not null)
                {
                    response = _redisCacheService.Get<string>(key);
                }
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
            return response;
        }

        public bool RedisRemove(string key)
        {
            var isRemoved = _redisCacheService.Remove(key);

            if (isRemoved)
            {
                return true;
            }

            return default;
        }

        public void RedisSet(string key, string data)
        {
            if (key is not null && data is not null)
            {
                _redisCacheService.Set(key, data);
            }
        }

    }
}

