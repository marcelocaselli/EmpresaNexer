using Microsoft.AspNetCore.Mvc;

namespace EmpresaNexer.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        [Route("")]
        public IActionResult Get()
        {
            return Ok();
        }

    }
}
