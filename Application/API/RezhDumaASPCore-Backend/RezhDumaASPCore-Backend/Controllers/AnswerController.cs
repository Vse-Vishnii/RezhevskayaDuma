using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RezhDumaASPCore_Backend.Model;
using RezhDumaASPCore_Backend.Repositories;
using RezhDumaASPCore_Backend.Services;
using RezhDumaASPCore_Backend.Helpers;

namespace RezhDumaASPCore_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnswerController : AbstractController<Answer,AnswerRepository>
    {
        private readonly IMessageService messageService;
        public AnswerController(AnswerRepository repository, IMessageService messageService) : base(repository)
        {
            this.messageService = messageService;
        }

        [Authorize(new[]{Role.Deputy})]
        public override async Task<ActionResult<Answer>> Post(Answer entity)
        {
            await repository.Add(entity);
            messageService.Send(entity.Application.Deputy, entity.Application.Applicant, entity);
            return Ok(entity);
        }

        public async override Task<ActionResult<Answer>> Get(string id)
        {
            return await repository.Get(id);
        }
    }
}
