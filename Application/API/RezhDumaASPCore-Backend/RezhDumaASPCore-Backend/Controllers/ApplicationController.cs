using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RezhDumaASPCore_Backend.Model;

namespace RezhDumaASPCore_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationController : AbstractController<Application>
    {
        public ApplicationController(ILogger<ApplicationController> logger, UserContext db) : base(logger,db)
        {
            entities = db.Applications;
            //Default.CreateData(db);
            //починить вывод заявлений
            //поработать с Find
            //протестировать добавление
            //https://docs.microsoft.com/en-us/ef/core/change-tracking/identity-resolution
        }

        public override Task<ActionResult<IEnumerable<Application>>> Get()
        {
            entities.ForEachAsync(app => app = SetForeignKeys(app));
            return base.Get();
        }

        public override Task<ActionResult<Application>> Get(string id)
        {
            var app = entities.Find(id);
            app = SetForeignKeys(app);
            return base.Get(id);
        }

        [HttpPost]
        public override async Task<ActionResult<Application>> Post(Application application)
        {
            if (application == null)
                return BadRequest();
            SetDeputyApplication(application);
            db.ChangeTracker.TrackGraph(application, node =>
                node.Entry.State = !node.Entry.IsKeySet ? EntityState.Added : EntityState.Unchanged);
            await db.SaveChangesAsync();
            return Ok(application);
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
