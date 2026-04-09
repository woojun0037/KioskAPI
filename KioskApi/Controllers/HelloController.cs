using Microsoft.AspNetCore.Mvc;

namespace KioskApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class HelloController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("API 연결 성공");
        }
    }
}
