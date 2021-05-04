using Microsoft.AspNetCore.Mvc;

namespace ExchangeNetCore.ApiGateways.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("ApiGateway is running...");
    }
}