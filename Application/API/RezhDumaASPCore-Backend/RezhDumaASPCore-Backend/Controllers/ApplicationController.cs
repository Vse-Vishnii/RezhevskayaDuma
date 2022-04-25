using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RezhDumaASPCore_Backend.Model;
using RezhDumaASPCore_Backend.Repositories;

namespace RezhDumaASPCore_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationController : AbstractController<Application, ApplicationRepository>
    {
        public ApplicationController(ILogger<ApplicationController> logger, ApplicationRepository repository) : base(logger, repository)
        {
        }

        [HttpGet("deputy/{id}")]
        public async Task<ActionResult<IEnumerable<Application>>> Get(string id)
        {
            return await repository.GetByDeputy(id);
        }

        [HttpGet("{status}")]
        public async Task<ActionResult<IEnumerable<Application>>> Get(Status status)
        {
            return await repository.GetByStatus(status);
        }

        [HttpGet("deputy/{id}/{status}")]
        public async Task<ActionResult<IEnumerable<Application>>> Get(string id, Status status)
        {
            return await repository.GetByDeputyStatus(id, status);
        }
    }
}
