using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RezhDumaASPCore_Backend.Model;
using RezhDumaASPCore_Backend.Repositories;

namespace RezhDumaASPCore_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DistrictController : AbstractController<District,DistrictRepository>
    {
        public DistrictController(ILogger<ApplicationController> logger, DistrictRepository repository) : base(logger, repository)
        {
        }
    }
}
