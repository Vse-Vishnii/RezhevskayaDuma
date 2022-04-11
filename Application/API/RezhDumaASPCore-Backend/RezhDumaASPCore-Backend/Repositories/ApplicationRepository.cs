using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RezhDumaASPCore_Backend.Model;

namespace RezhDumaASPCore_Backend.Repositories
{
    public class ApplicationRepository : GeneralRepository<Application>
    {
        public ApplicationRepository(UserContext db) : base(db)
        {
        }

        public async override Task<List<Application>> GetAll()
        {
            db.Set<Application>().ForEachAsync(app => app = SetForeignKeys(app));
            return await base.GetAll();
        }

        public async override Task<Application> Get(string id)
        {
            var app = SetForeignKeys(db.Set<Application>().Find(id));
            return await base.Get(id);
        }

        public async override Task<Application> Add(Application entity)
        {
            SetDeputyApplication(entity);
            AddEntity(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        private Application SetForeignKeys(Application app)
        {
            app.Categories = new List<Category>(db.CategoryApplications
                .Where(ca => ca.ApplicationId.Equals(app.Id))
                .Select(ca => ca.Category));
            app.Districts = new List<District>(db.DistrictApplications
                .Where(da => da.ApplicationId.Equals(app.Id))
                .Select(da => da.District));
            app.Deputy = db.DeputyApplications.Where(da => da.ApplicationId.Equals(app.Id))
                .Select(da => da.Deputy)
                .FirstOrDefault();
            return app;
        }

        private void SetDeputyApplication(Application application)
        {
            if (application.Categories != null && application.Categories.Count == 1)
                application.Deputy = db.Users.Find(application.Categories[0].DeputyId);
            if (application.Districts != null && application.Districts.Count == 1)
                application.Deputy = db.Users.Find(application.Districts[0].DeputyId);
            var deputy = application.Deputy;
            if (deputy != null)
            {
                db.ChangeTracker.TrackGraph(new DeputyApplication(application, deputy), node =>
                    node.Entry.State = !node.Entry.IsKeySet ? EntityState.Added : EntityState.Unchanged);
            }
        }
    }
}
