using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RezhDumaASPCore_Backend.Model;

namespace RezhDumaASPCore_Backend.Controllers
{
    public abstract class AbstractController : Controller
    {
        protected readonly ILogger<ApplicationController> logger;
        protected readonly UserContext db;

        public AbstractController(ILogger<ApplicationController> logger, UserContext db)
        {
            this.logger = logger;
            this.db = db;
        }
    }
}
