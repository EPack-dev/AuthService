using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuthService.WebApi.Controllers
{
    [Route("example")]
    public class ExampleController : ControllerBase
    {
        public ExampleController(ILogger<ExampleController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("Ok");
        }

        private readonly ILogger<ExampleController> _logger;
    }
}
