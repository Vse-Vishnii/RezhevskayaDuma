using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RezhDumaASPCore_Backend.Model;

namespace RezhDumaASPCore_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly ILogger<ApplicationController> logger;
        private readonly UserContext db;

        public ApplicationController(ILogger<ApplicationController> logger, UserContext db)
        {
            this.logger = logger;
            this.db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Application>>> Get()
        {
            return await db.Applications.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Application>> Get(string id)
        {
            return await db.Applications.FirstOrDefaultAsync(a => a.Id.Equals(id));
        }

        [HttpPost]
        public async Task<ActionResult<Application>> Post(Application application)
        {
            if (application == null)
                return BadRequest();
            db.Applications.Add(application);
            await db.SaveChangesAsync();
            return Ok(application);
        }

        [HttpPut]
        public async Task<ActionResult<Application>> Put(Application application)
        {
            if (application == null)
            {
                return BadRequest();
            }
            if (!db.Applications.Any(x => x.Id == application.Id))
            {
                return NotFound();
            }

            db.Update(application);
            await db.SaveChangesAsync();
            return Ok(application);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(string id)
        {
            var application = db.Applications.FirstOrDefault(x => x.Id == id);
            if (application == null)
            {
                return NotFound();
            }
            db.Applications.Remove(application);
            await db.SaveChangesAsync();
            return Ok(application);
        }
    }
}
