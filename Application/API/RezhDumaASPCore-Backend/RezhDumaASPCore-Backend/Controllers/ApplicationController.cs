using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("deputy/{id}")]
        public async Task<ActionResult<IEnumerable<Application>>> Get(string id)
        {
            return await repository.GetByDeputy(id);
        }

        [HttpGet("status={status}")]
        public async Task<ActionResult<IEnumerable<Application>>> Get(Status status)
        {
            return await repository.GetByStatus(status);
        }

        [HttpGet("deputy/{id}/status={status}")]
        public async Task<ActionResult<IEnumerable<Application>>> Get(string id, Status status)
        {
            return await repository.GetByDeputyStatus(id, status);
        }

        [HttpGet("name={name}")]
        public async Task<ActionResult<IEnumerable<Application>>> GetByName(string name)
        {
            return await repository.GetByName(name);
        }

        public async override Task<ActionResult<Application>> Post(Application entity)
        {
            await repository.Add(entity);
            //messageService.Send(entity.Applicant, entity.Deputy,  entity);
            return Ok(entity);
        }

        [HttpPost("parameters")]
        public async Task<ActionResult<Application>> Post(Application entity, [FromQuery] string[] categoryIds = null,
            [FromQuery] string[] districtIds = null, string deputyId = null)
        {
            await repository.Add(entity, categoryIds, districtIds, deputyId);
            //messageService.Send(entity.Applicant, entity.Deputy,  entity);
            return Ok(entity);
        }

        [HttpPut("{id}/{accepted}")]
        public async Task<ActionResult<Application>> AcceptApplication(string id, bool accepted, Answer answer = null)
        {
            var app = await repository.Get(id);
            app.Status = accepted ? Status.InProcess : Status.Refused;
            await repository.Update(app);
            if (!accepted)
                messageService.Send(app.Deputy, app.Applicant, answer);
            return Ok(app);
        }
    }
}
