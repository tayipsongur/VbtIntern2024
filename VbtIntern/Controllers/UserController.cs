using Microsoft.AspNetCore.Mvc;
using VbtIntern.Entities.Models;
using VbtIntern.Services;

namespace VbtIntern.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IServiceProvider serviceProvider)
        {
            _userService = serviceProvider.GetRequiredService<IUserService>();
        }

        [HttpGet(nameof(GetAllUser))]
        public List<User> GetAllUser()
        {
            //elinize sağlık
            var response = _userService.GetAllUser();

            return response;
        }

        [HttpPost(nameof(InsertEntity))]
        public User InsertEntity(User user)
        {
            var response = _userService.InsertEntity(user);
            if (response != null)
            {
                return response;
            }
            return response;
        }

        [HttpPost(nameof(InsertUsers))]
        public List<User> InsertUsers(List<User> users)
        {
            var response = _userService.InsertUsers(users);
            if (response != null)
            {
                return response;
            }
            return response;
        }


        //REDİS İŞLEMLERİ

        [HttpGet(nameof(GetRedisUser))]
        public string GetRedisUser(string key)
        {
            var response = _userService.GetRedisUser(key);

            if (response is not null)
            {
                return response;
            }
            return string.Empty;
        }

        [HttpPost(nameof(RedisSet))]
        public void RedisSet(string key, string data)
        {
            _userService.RedisSet(key, data);
        }

        [HttpPost(nameof(RedisRemove))]
        public bool RedisRemove(string key)
        {
            var response = _userService.RedisRemove(key);
            return response;
        }
    }
}
