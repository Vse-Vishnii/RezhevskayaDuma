using RezhDumaASPCore_Backend.Model;

namespace RezhDumaASPCore_Backend.Repositories
{
    public class UserRepository : GeneralRepository<User>
    {
        public UserRepository(UserContext db) : base(db)
        {
        }
    }
}
