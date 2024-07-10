using VbtIntern.Entities.Models;

namespace VbtIntern.Services
{
    public interface IUserService
    {
        List<User> GetAllUser();
        User InsertEntity(User user);
        User UpdateEntity(User user);
        bool DeleteEntity(int id);
        List<User> InsertUsers(List<User> users);
        //REDİS METHODS
        string GetRedisUser(string key);

        bool RedisRemove(string key);
        void RedisSet(string key, string data);

    }
}
