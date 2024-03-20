using Microsoft.AspNetCore.Mvc;

namespace ProxyPeeker.Controllers;

[ApiController]
public class AppController : ControllerBase
{
    [HttpGet]
    [Route("")]
    public IActionResult IsHealthy()
    {
        return Ok(
            new
            {
                Healthy = true
            }
        );
    }
}
