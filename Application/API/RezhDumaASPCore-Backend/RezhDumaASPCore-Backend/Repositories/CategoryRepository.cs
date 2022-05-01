using RezhDumaASPCore_Backend.Model;

namespace RezhDumaASPCore_Backend.Repositories
{
    public class CategoryRepository : GeneralRepository<Category>
    {
        public CategoryRepository(UserContext db) : base(db)
        {
        }
    }
}
