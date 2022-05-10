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

        [HttpGet("role={role}")]
        public async Task<ActionResult<IEnumerable<User>>> GetByRole(Role role)
        {
            return await repository.GetByRole(role);
        }

        [HttpGet("deputies/category={category}/district={district}")]
        public async Task<ActionResult<IEnumerable<User>>> Get(string category, string district)
        {
            return await repository.GetDeputyByCategoryAndDistrict(category, district);
        }

        [HttpGet("deputies/category={category}")]
        public async Task<ActionResult<IEnumerable<User>>> GetByCategory(string category)
        {
            return await repository.GetDeputyByCategory(category);
        }

        [HttpGet("deputies/district={district}")]
        public async Task<ActionResult<IEnumerable<User>>> GetByDistrict(string district)
        {
            return await repository.GetDeputyByDistrict(district);
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
