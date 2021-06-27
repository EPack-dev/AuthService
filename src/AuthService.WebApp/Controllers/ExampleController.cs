using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.WebApp.Controllers
{
    [ApiController]
    [Route("api/example")]
    public class ExampleController : ControllerBase
    {
        [HttpGet("no-auth")]
        public ActionResult GetNoAuth()
        {
            return Ok();
        }

        [HttpGet("auth")]
        [Authorize]
        public ActionResult GetAuth()
        {
            return Ok();
        }

        [HttpGet("auth-regular")]
        [Authorize(Roles = "Regular")]
        public ActionResult GetAuthUser()
        {
            return Ok();
        }

        [HttpGet("auth-admin")]
        [Authorize(Roles = "Admin")]
        public ActionResult GetAuthAdmin()
        {
            return Ok();
        }
    }
}
