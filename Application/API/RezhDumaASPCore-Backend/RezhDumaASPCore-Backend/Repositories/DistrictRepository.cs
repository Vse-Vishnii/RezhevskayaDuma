using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RezhDumaASPCore_Backend.Model;

namespace RezhDumaASPCore_Backend.Repositories
{
    public class DistrictRepository : GeneralRepository<District>
    {
        public DistrictRepository(UserContext db) : base(db)
        {
        }

        public override Task<District> Add(District entity)
        {
            if (db.Set<District>().FirstOrDefault(d => d.DeputyId.Equals(entity.DeputyId)) != null)
                throw new Exception("К депутату уже прикреплён район");
            return base.Add(entity);
        }
    }
}
