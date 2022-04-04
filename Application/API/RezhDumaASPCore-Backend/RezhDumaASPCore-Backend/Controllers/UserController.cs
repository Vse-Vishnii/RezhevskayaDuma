using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RezhDumaASPCore_Backend.Model;

namespace RezhDumaASPCore_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : AbstractController<User>
    {
        public UserController(ILogger<ApplicationController> logger, UserContext db) : base(logger, db)
        {
        }

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await db.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<User>> Get(string id)
        {
            return await db.Users.FirstOrDefaultAsync(user => user.Id.Equals(id));
        }
    }
}
