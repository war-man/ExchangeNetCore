using Microsoft.AspNetCore.Mvc;

namespace ExchangeNetCore.Services.Signalr.Api.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("SignalR Service is running...");
    }
}