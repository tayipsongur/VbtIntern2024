using VbtIntern.Entities.Models;

namespace VbtIntern.Services
{
    public interface IUserService
    {
        List<User> GetAllUser();
        User InsertEntity(User user);
        User UpdateEntity(User user);
        bool DeleteEntity(int id);
    }
}
