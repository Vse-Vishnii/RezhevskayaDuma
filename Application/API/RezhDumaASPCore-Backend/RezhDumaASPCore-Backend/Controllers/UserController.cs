using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RezhDumaASPCore_Backend.Model;
using RezhDumaASPCore_Backend.Model.Authentication;
using RezhDumaASPCore_Backend.Repositories;
using RezhDumaASPCore_Backend.Services;

namespace RezhDumaASPCore_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : AbstractController<User, UserRepository>
    {
        private readonly IUserService userService;
        public UserController(UserRepository repository, IUserService userService) : base(repository)
        {
            this.userService = userService;
        }

        [HttpGet("deputies/filters/")]
        public async Task<ActionResult<IEnumerable<User>>> Get(string category = null, string district = null)
        {
            return await repository.GetDeputyByCategoryAndDistrict(category, district);
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
    }
}
