using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RezhDumaASPCore_Backend.Model;
using RezhDumaASPCore_Backend.Repositories;

namespace RezhDumaASPCore_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnswerController : AbstractController<Answer,AnswerRepository>
    {
        public AnswerController(AnswerRepository repository) : base(repository)
        {
        }
    }
}
