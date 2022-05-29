using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RezhDumaASPCore_Backend.Model;

namespace RezhDumaASPCore_Backend.Repositories
{
    public class UserRepository : GeneralRepository<User>
    {
        private readonly IPasswordHasher<User> passwordHasher;

        public UserRepository(UserContext db, IPasswordHasher<User> passwordHasher) : base(db)
        {
            this.passwordHasher = passwordHasher;
        }

        public async Task<List<User>> GetDeputyByCategoryAndDistrict(string[] categories, string[] districts)
        {
            var queryable = await db.Set<User>()
                .Where(u => u.Role == Role.Deputy).ToListAsync();
            var deputies = queryable
                .Where(d => CheckFilters(categories, districts, d)).ToList();
            deputies.ForEach(d => PullCategoryAndDistrict(d));
            return deputies;
        }

        public async override Task<User> Get(string id)
        {
            var user = await db.Set<User>().FindAsync(id);
            PullCategoryAndDistrict(user);
            return user;
        }

        public override Task<User> Add(User entity)
        {
            if (db.Set<User>().FirstOrDefault(u => u.Email.Equals(entity.Email)) != null)
                throw new Exception("Данная почта уже зарегистрирована");
            entity.Password = passwordHasher.HashPassword(entity, entity.Password);
            return base.Add(entity);
        }

        private void PullCategoryAndDistrict(User user)
        {
            db.PullEntity<Category>(user.CategoryId);
            db.PullEntity<District>(user.DistrictId);
        }

        private static bool CheckFilters(string[] categories, string[] districts, User user)
        {
            return CheckSpecialFilter(categories, user.CategoryId) || CheckSpecialFilter(districts, user.DistrictId);
        }

        private static bool CheckSpecialFilter(string[] entities, string id)
        {
            if (entities.Length == 0) return true;
            if (id == null) return false;
            foreach (var e in entities)
                if (id.Equals(e))
                    return true;
            return false;
        }

        private void PullIds<T>(string[] entities) where T : DbEntity
        {
            entities.ToList().ForEach(c => db.PullEntity<T>(c));
        }
    }
}
