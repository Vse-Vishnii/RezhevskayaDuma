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
            //CreateData();
        }

        private void CreateData()
        {
            var user = new User
            {
                Firstname = "Алексей",
                Role = Role.Applicant
            };
            db.Add(user);
            var deputy = new User
            {
                Firstname = "Депутат",
                Role = Role.Deputy
            };
            db.Add(deputy);
            var application = new Application
            {
                Applicant = user,
                Name = "Test",
                Description = "It's a test application",
                Status = Status.Sent
            };
            db.Add(application);
            db.SaveChanges();
        }

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<Application>>> Get()
        {
            return await db.Applications.ToListAsync();
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<Application>> Get(string id)
        {
            return await db.Applications.FirstOrDefaultAsync(a => a.Id.Equals(id));
        }

        [HttpPost]
        public override async Task<ActionResult<Application>> Post(Application application)
        {
            if (application == null)
                return BadRequest();
            AddDistrict(application);
            AddCategory(application);
            var deputy = application.Deputy;
            if (deputy != null)
            {
                db.Add(new DeputyApplication(application, deputy));
            }
            db.Add(application);
            await db.SaveChangesAsync();
            return Ok(application);
        }

        private async void AddDistrict(Application application)
        {
            var districts = application.Districts;
            if (districts != null)
                foreach (var d in districts)
                {
                    var da = new DistrictApplication(application, d);
                    db.Add(da);
                    await db.SaveChangesAsync();
                }
        }

        private async void AddCategory(Application application)
        {
            var categories = application.Categories;
            if (categories != null)
                foreach (var c in categories)
                {
                    var ca = new CategoryApplication(application, c);
                    db.Add(ca);
                    await db.SaveChangesAsync();
                }
        }
    }
}
