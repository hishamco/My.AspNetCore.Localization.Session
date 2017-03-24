using Microsoft.AspNetCore.Mvc;

namespace LocalizationSample.Controllers
{   
    public class HomeController : Controller
    {
        [Route("/")]
        [MiddlewareFilter(typeof(LocalizationPipeline))]
        public IActionResult Index() => View();
    }
}
