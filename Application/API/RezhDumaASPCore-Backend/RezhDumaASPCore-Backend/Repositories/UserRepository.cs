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
            if (categoryId != null) db.PullEntity<Category>(categoryId);
            if (districtId != null) db.PullEntity<District>(districtId);
            var deputies = await db.Set<User>()
                .Where(u => u.Role == Role.Deputy)
                .Where(d => (categoryId == null || d.Category.Id.Equals(categoryId)) &&
                            (districtId == null || d.District.Id.Equals(districtId)))
                .ToListAsync();
            deputies.ForEach(d => PullCategoryAndDistrict(categoryId, districtId, d));
            return deputies;
        }

        public async override Task<User> Get(string id)
        {
            var user = await db.Set<User>().FindAsync(id);
            PullCategoryAndDistrict(user.CategoryId, user.DistrictId, user);
            return user;
        }

        private void PullCategoryAndDistrict(string categoryId, string districtId, User user)
        {
            if (categoryId == null) db.PullEntity<Category>(user.CategoryId);
            if (districtId == null) db.PullEntity<District>(user.DistrictId);
        }
    }
}
