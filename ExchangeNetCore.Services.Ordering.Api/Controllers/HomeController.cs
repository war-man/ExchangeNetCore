using Microsoft.AspNetCore.Mvc;

namespace ExchangeNetCore.Services.Ordering.Api.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Ordering Service is runing...");
    }
}