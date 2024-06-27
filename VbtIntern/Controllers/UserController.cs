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
            var response = _userService.GetAllUser();
            return response;
        }

    }
}
