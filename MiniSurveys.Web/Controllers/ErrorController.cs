using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;
using MiniSurveys.Web.Models;
using System.Diagnostics;

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
