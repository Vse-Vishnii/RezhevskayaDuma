using System.Linq;
using System.Threading.Tasks;
using RezhDumaASPCore_Backend.Model;
using RezhDumaASPCore_Backend.Services;

namespace RezhDumaASPCore_Backend.Repositories
{
    public class AnswerRepository : GeneralRepository<Answer>
    {
        private readonly IEmailService emailService;

        public AnswerRepository(IEmailService emailService, UserContext db) : base(db)
        {
            this.emailService = emailService;
        }

        public async override Task<Answer> Add(Answer entity)
        {
            db.Add(entity);
            var app = db.Set<Application>().Find(entity.ApplicationId);
            var user = db.Set<User>().FirstOrDefault(u => u.Id.Equals(app.ApplicantId));
            await emailService.SendEmailAsync(user.Email, entity.Name , entity.Description);
            await db.SaveChangesAsync();
            return entity;
        }
    }
}
