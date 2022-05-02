using System.Linq;
using System.Threading.Tasks;
using RezhDumaASPCore_Backend.Model;

namespace RezhDumaASPCore_Backend.Repositories
{
    public class AnswerRepository : GeneralRepository<Answer>
    {
        public AnswerRepository(UserContext db) : base(db)
        {
        }

        public async override Task<Answer> Add(Answer entity)
        {
            var app = db.PullEntity(entity.Application, entity.ApplicationId);
            db.PullEntity(app.Applicant, app.ApplicantId);
            var deputyApplication = db.Set<DeputyApplication>()
                .FirstOrDefault(da => da.ApplicationId.Equals(app.Id));
            db.PullEntity(app.Deputy, deputyApplication.DeputyId);
            return await base.Add(entity);
        }
    }
}
