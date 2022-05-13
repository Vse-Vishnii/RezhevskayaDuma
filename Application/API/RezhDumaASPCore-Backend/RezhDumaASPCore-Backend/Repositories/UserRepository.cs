using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RezhDumaASPCore_Backend.Model;

namespace RezhDumaASPCore_Backend.Repositories
{
    public class UserRepository : GeneralRepository<User>
    {
        public UserRepository(UserContext db) : base(db)
        {
        }

        public async Task<List<User>> GetDeputyByCategoryAndDistrict(string categoryId, string districtId)
        {
            if (categoryId != null)
                db.PullEntity<Category>(categoryId);
            if (districtId != null)
                db.PullEntity<District>(districtId);
            return await db.Set<User>().Where(u => u.Role == Role.Deputy)
                .Where(d => (categoryId == null || d.Category.Id.Equals(categoryId)) && (districtId == null || d.District.Id.Equals(districtId))).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<User>>> GetDeputyByCategory(string categoryId)
        {
            db.PullEntity<Category>(categoryId);
            return await db.Set<User>().Where(d => d.Category.Id.Equals(categoryId)).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<User>>> GetDeputyByDistrict(string districtId)
        {
            db.PullEntity<District>(districtId);
            return await db.Set<User>().Where(d => d.District.Id.Equals(districtId)).ToListAsync();
        }
    }
}
