using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RezhDumaASPCore_Backend.Model;

namespace RezhDumaASPCore_Backend.Repositories
{
    public class ApplicationRepository : GeneralRepository<Application>
    {
        public ApplicationRepository(UserContext db) : base(db)
        {
        }

        public async Task<List<Application>> GetByStatus(Status status)
        {
            return GetApplications(GetByStatus(status, db.Set<Application>()));
        }

        public async Task<List<Application>> GetByDeputyStatus(string id, Status? status)
        {
            var apps = GetByDeputy(id, db.Set<DeputyApplication>());
            if(status != null)
                apps = GetByStatus(status, apps);
            return GetApplications(apps);
        }

        public async override Task<List<Application>> GetAll()
        {
            db.Set<Application>().ForEachAsync(app => app = SetForeignKeys(app));
            return await base.GetAll();
        }

        public async override Task<Application> Get(string id)
        {
            SetForeignKeys(db.Set<Application>().Find(id));
            return await base.Get(id);
        }

        public async Task<ActionResult<IEnumerable<Application>>> GetWithFilter(string filter)
        {
            return await db.Set<Application>()
                .Where(app =>
                    filter == null || app.Name.ToLower().Contains(filter.ToLower()) ||
                                       app.Id.ToLower().Contains(filter.ToLower()))
                .ToListAsync();
        }

        public async override Task<Application> Add(Application entity)
        {
            SetDeputyApplication(entity);
            if(entity.ApplicantId != null)
                db.PullEntity<User>(entity.ApplicantId);
            else
                AddEntity(entity.Applicant);
            entity.Created = DateTime.Now;
            AddEntity(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public async Task<Application> Add(Application entity, string[] categoryIds, string[] districtIds, string deputyId)
        {
            if (categoryIds != null)
                entity.Categories = await db.Set<Category>().Where(cat => categoryIds.Contains(cat.Id)).ToListAsync();
            if (districtIds != null)
                entity.Districts = await db.Set<District>().Where(dis => districtIds.Contains(dis.Id)).ToListAsync();
            if (categoryIds != null)
                entity.Deputy = await db.Set<User>().FindAsync(deputyId);
            return await Add(entity);
        }

        private List<Application> GetApplications(List<Application> apps)
        {
            apps.ForEach(app => app = SetForeignKeys(app));
            return apps;
        }

        private Application SetForeignKeys(Application app)
        {
            db.PullCollection<Category,CategoryApplication>(app).ToList();
            db.PullCollection<District, DistrictApplication>(app).ToList(); 
            app.Deputy = db.DeputyApplications.Where(da => da.ApplicationId.Equals(app.Id))
                .Select(da => da.Deputy)
                .FirstOrDefault();
            return app;
        }

        private void SetDeputyApplication(Application application)
        {
            if (application.Categories != null && application.Categories.Count == 1)
                application.Deputy = db.PullEntity<User>(application.Categories[0].DeputyId);
            if (application.Districts != null && application.Districts.Count == 1)
                application.Deputy = db.PullEntity<User>(application.Districts[0].DeputyId);
            var deputy = application.Deputy;
            if (deputy != null)
            {
                AddEntity(new DeputyApplication(application, deputy));
            }
        }

        private List<Application> GetByDeputy(string id, IEnumerable<DeputyApplication> list)
        {
            var deputyApplications = list.Where(app => app.DeputyId.Equals(id)).ToList();
            deputyApplications.ForEach(da => db.PullEntity<Application>(da.ApplicationId));
            return deputyApplications.Select(app => app.Application).ToList();
        }

        private List<Application> GetByStatus(Status? status, IEnumerable<Application> list)
        {
            return list.Where(app => app.Status == status).ToList();
        }
    }
}
