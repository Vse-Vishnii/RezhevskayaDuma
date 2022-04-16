using RezhDumaASPCore_Backend.Model;

namespace RezhDumaASPCore_Backend.Repositories
{
    public class AnswerRepository : GeneralRepository<Answer>
    {
        public AnswerRepository(UserContext db) : base(db)
        {
        }
    }
}
