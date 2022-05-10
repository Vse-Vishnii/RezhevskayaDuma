﻿using System.Collections.Generic;
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

        public async Task<List<User>> GetByRole(Role role)
        {
            return db.Set<User>().Where(u => u.Role == role).ToList();
        }

        public async Task<List<User>> GetDeputyByCategoryAndDistrict(string categoryId, string districtId)
        {
            db.PullEntity<Category>(categoryId);
            db.PullEntity<District>(districtId);
            return await db.Set<User>().Where(d => d.Category.Id.Equals(categoryId) && d.District.Id.Equals(districtId)).ToListAsync();
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
