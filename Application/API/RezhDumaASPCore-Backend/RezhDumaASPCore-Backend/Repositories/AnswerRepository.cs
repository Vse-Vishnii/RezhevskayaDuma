using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            var app = db.PullEntity<Application>(entity.ApplicationId);
            entity.Application = app;
            db.PullEntity<User>(app.ApplicantId);
            var deputyApplication = db.Set<DeputyApplication>()
                .FirstOrDefault(da => da.ApplicationId.Equals(app.Id));
            entity.Application.Deputy = db.PullEntity<User>(deputyApplication.DeputyId);
            entity.Created = DateTime.Now;
            return await base.Add(entity);
        }

        public async override Task<Answer> Get(string id)
        {
            return await db.Set<Answer>().FirstOrDefaultAsync(ans => ans.ApplicationId.Equals(id));
        }
    }
}
