using Microsoft.AspNetCore.Mvc;

namespace MiniSurveys.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("[controller]/{code}")]
        public IActionResult Index(int code)
        {
            return View(code);
        }
    }
}
