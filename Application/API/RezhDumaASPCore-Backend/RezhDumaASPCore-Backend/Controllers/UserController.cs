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
    }
}
