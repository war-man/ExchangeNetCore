using Microsoft.AspNetCore.Mvc;

namespace ExchangeNetCore.Monolith.Api.Controllers
{
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Index()
        {
            return "Api is running...";
        }
    }
}