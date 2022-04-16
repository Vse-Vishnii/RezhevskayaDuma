using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RezhDumaASPCore_Backend.Model;
using RezhDumaASPCore_Backend.Repositories;

namespace RezhDumaASPCore_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : AbstractController<Category,CategoryRepository>
    {
        public CategoryController(ILogger<ApplicationController> logger, CategoryRepository repository) : base(logger, repository)
        {
        }
    }
}
