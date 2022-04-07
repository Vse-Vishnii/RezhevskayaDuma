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
        protected DbSet<T> entities;

        public AbstractController(ILogger<ApplicationController> logger, UserContext db)
        {
            this.logger = logger;
            this.db = db;
        }

        [HttpGet]
        public async virtual Task<ActionResult<IEnumerable<T>>> Get()
        {
            return await entities.ToListAsync();
        }

        [HttpGet("{id}")]
        public async virtual Task<ActionResult<T>> Get(string id)
        {
            return await entities.FirstOrDefaultAsync(dbEntity => dbEntity.Id.Equals(id));
        }

        [HttpPost]
        public virtual async Task<ActionResult<T>> Post(T entity)
        {
            if (entity == null)
                return BadRequest();
            db.Add(entity);
            await db.SaveChangesAsync();
            return Ok(entity);
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult<T>> Put(string id, T newEntity)
        {
            var application = entities.FirstOrDefault(app => app.Id.Equals(id));
            newEntity.GetType().GetProperties()
                .Where(p=>p.GetValue(newEntity)!=null)
                .ToList()
                .ForEach(p=>application.GetType()
                    .GetProperty(p.Name)
                    .SetValue(application,p.GetValue(newEntity)));
            if (application == null)
            {
                return BadRequest();
            }
            db.Update(application);
            await db.SaveChangesAsync();
            return Ok(application);
        }
        
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<T>> Delete(string id)
        {
            var application = entities.FirstOrDefault(x => x.Id == id);
            if (application == null)
            {
                return NotFound();
            }
            db.Remove(application);
            await db.SaveChangesAsync();
            return Ok(application);
        }
    }
}
