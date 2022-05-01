using RezhDumaASPCore_Backend.Model;

namespace RezhDumaASPCore_Backend.Repositories
{
    public class DistrictRepository : GeneralRepository<District>
    {
        public DistrictRepository(UserContext db) : base(db)
        {
        }
    }
}
