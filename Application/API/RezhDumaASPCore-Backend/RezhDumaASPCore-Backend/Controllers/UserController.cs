using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RezhDumaASPCore_Backend.Model;
using RezhDumaASPCore_Backend.Repositories;

namespace RezhDumaASPCore_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : AbstractController<User, UserRepository>
    {
        public UserController(UserRepository repository) : base(repository)
        {
        }

        [HttpGet("deputies")]
        public async Task<ActionResult<IEnumerable<User>>> GetByRole()
        {
            return await repository.GetByRole(Role.Deputy);
        }

        [HttpGet("deputies/filters/")]
        public async Task<ActionResult<IEnumerable<User>>> Get(string category = null, string district = null)
        {
            if (category == null && district == null)
                return BadRequest();
            return await repository.GetDeputyByCategoryAndDistrict(category, district);
        }
    }
}
