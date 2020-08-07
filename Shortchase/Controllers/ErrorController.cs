using Microsoft.AspNetCore.Mvc;
using Shortchase.Helpers;

namespace Shortchase.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(RequestFeedback request)
        {
            ViewData["Message"] = request.Text;
            ViewData["Title"] = request.Title;
            return View();
        }
    }
}