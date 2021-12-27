using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi6.Data;
using WebApi6.Repository;

namespace WebApi6.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository _repo;
        public UsersController(IUserRepository userRepository)
        {
            _repo = userRepository;

        }
        [AllowAnonymous]
        [HttpPost("ValidateUser")]
        public bool ValidateUser([FromBody] User user)
        {
            var bs = Convert.FromBase64String(user.Username);
            var u = Encoding.UTF8.GetString(bs);
            var bs2 = Convert.FromBase64String(user.Password);
            var p = Encoding.UTF8.GetString(bs2);


            var objUser = _repo.ValidateUser(u, p);

            if (objUser == null)
                return false;
            else
                return true;

        }
    }
}
