using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RezhDumaASPCore_Backend.Helpers;
using RezhDumaASPCore_Backend.Model;
using RezhDumaASPCore_Backend.Repositories;
using RezhDumaASPCore_Backend.Services;

namespace RezhDumaASPCore_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationController : AbstractController<Application, ApplicationRepository>
    {
        private readonly IMessageService messageService;

        public ApplicationController(ApplicationRepository repository, IMessageService messageService) : base(repository)
        {
            this.messageService = messageService;
        }

        [HttpGet("status={status}")]
        public async Task<ActionResult<IEnumerable<Application>>> Get(Status status)
        {
            return await repository.GetByStatus(status);
        }

        [HttpGet("deputy/{id}")]
        public async Task<ActionResult<IEnumerable<Application>>> Get(string id, Status? status = null)
        {
            return await repository.GetByDeputyStatus(id, status);
        }

        [HttpGet("filters")]
        public async Task<ActionResult<IEnumerable<Application>>> Get(string name = null, string id = null)
        {
            return await repository.Get(id, name);
        }

        [HttpPost("parameters")]
        public async Task<ActionResult<Application>> Post(Application entity, [FromQuery] string[] categoryIds = null,
            [FromQuery] string[] districtIds = null, string deputyId = null)
        {
            await repository.Add(entity, categoryIds, districtIds, deputyId);
            //messageService.Send(entity.Applicant, entity.Deputy,  entity);
            return Ok(entity);
        }

        [Authorize(new [] {Role.Special})]
        [HttpPut("{id}/{accepted}")]
        public async Task<ActionResult<Application>> AcceptApplication(string id, bool accepted, [FromBody] Answer answer = null)
        {
            var app = await repository.Get(id);
            app.Status = accepted ? Status.InProcess : Status.Refused;
            await repository.Update(id, app);
            if (!accepted)
                messageService.Send(app.Deputy, app.Applicant, null);
            return Ok(app);
        }
    }
}
