using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RezhDumaASPCore_Backend.Model;

namespace RezhDumaASPCore_Backend.Repositories
{
    public abstract class GeneralRepository<TEntity> : IRepository<TEntity>
    where TEntity:DbEntity
    {
        protected readonly UserContext db;

        public GeneralRepository(UserContext db)
        {
            this.db = db;
        }

        public async virtual Task<List<TEntity>> GetAll()
        {
            return await db.Set<TEntity>().ToListAsync();
        }

        public async virtual Task<TEntity> Get(string id)
        {
            return await db.Set<TEntity>().FindAsync(id);
        }

        public async virtual Task<TEntity> Add(TEntity entity)
        {
            AddEntity(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public async virtual Task<TEntity> Update(string id, TEntity newEntity)
        {
            var entity = db.Set<TEntity>().Find(id);
            newEntity.GetType()
                .GetProperties()
                .Where(p => p.GetValue(newEntity) != null)
                .ToList()
                .ForEach(p => entity.GetType().GetProperty(p.Name).SetValue(entity, p.GetValue(newEntity)));
            db.Update(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public async virtual Task<TEntity> Delete(string id)
        {
            var entity = db.Set<TEntity>().Find(id);
            if (entity == null)
            {
                return entity;
            }
            db.Remove(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        protected void AddEntity(DbEntity entity)
        {
            db.ChangeTracker.TrackGraph(entity, node =>
                node.Entry.State = !node.Entry.IsKeySet ? EntityState.Added : EntityState.Unchanged);
        }
    }
}
