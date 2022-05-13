using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RezhDumaASPCore_Backend.Model;
using RezhDumaASPCore_Backend.Repositories;

namespace RezhDumaASPCore_Backend.Controllers
{
    [EnableCors(origins:"*", headers: "*", methods: "*")]
    public abstract class AbstractController<TEntity,TRepository> : Controller
    where TEntity:DbEntity
    where TRepository:IRepository<TEntity>
    {
        protected readonly TRepository repository;

        public AbstractController(TRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async virtual Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            return await repository.GetAll();
        }

        [HttpGet("{id}")]
        public async virtual Task<ActionResult<TEntity>> Get(string id)
        {
            return await repository.Get(id);
        }

        [HttpPost]
        public virtual async Task<ActionResult<TEntity>> Post(TEntity entity)
        {
            await repository.Add(entity);
            return Ok(entity);
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult<TEntity>> Put(string id, TEntity newEntity)
        {
            if (id!=newEntity.Id)
            {
                return BadRequest();
            }

            await repository.Update(newEntity);
            return Ok(newEntity);
        }
        
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<TEntity>> Delete(string id)
        {
            var entity = repository.Delete(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }
    }
}
