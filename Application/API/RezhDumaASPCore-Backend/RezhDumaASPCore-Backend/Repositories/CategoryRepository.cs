using System;
using System.Linq;
using System.Threading.Tasks;
using RezhDumaASPCore_Backend.Model;

namespace RezhDumaASPCore_Backend.Repositories
{
    public class CategoryRepository : GeneralRepository<Category>
    {
        public CategoryRepository(UserContext db) : base(db)
        {
        }

        public override Task<Category> Add(Category entity)
        {
            if (db.Set<Category>().FirstOrDefault(d => d.DeputyId.Equals(entity.DeputyId)) != null)
                throw new Exception("К депутату уже прикреплена категория");
            return base.Add(entity);
        }
    }
}
