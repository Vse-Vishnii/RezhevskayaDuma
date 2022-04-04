using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RezhDumaASPCore_Backend.Model;

namespace RezhDumaASPCore_Backend.Controllers
{
    public abstract class AbstractController<T> : Controller
    where T:DbEntity
    {
        protected readonly ILogger<ApplicationController> logger;
        protected readonly UserContext db;

        public AbstractController(ILogger<ApplicationController> logger, UserContext db)
        {
            this.logger = logger;
            this.db = db;
        }

        [HttpGet]
        public abstract Task<ActionResult<IEnumerable<T>>> Get();

        [HttpGet("{id}")]
        public abstract Task<ActionResult<T>> Get(string id);

        [HttpPost]
        public virtual async Task<ActionResult<T>> Post(T entity)
        {
            if (entity == null)
                return BadRequest();
            db.Add(entity);
            await db.SaveChangesAsync();
            return Ok(entity);
        }

        [HttpPut]
        public virtual async Task<ActionResult<T>> Put(T application)
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
        public virtual async Task<ActionResult<T>> Delete(string id)
        {
            var application = db.Applications.FirstOrDefault(x => x.Id == id);
            if (application == null)
            {
                return NotFound();
            }
            db.Remove(application);
            return Ok(application);
        }
    }
}
