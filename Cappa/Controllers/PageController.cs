using Microsoft.AspNetCore.Mvc;

namespace Cappa.Controllers
{
    public class PageController : Controller
    {
        public IActionResult Employee()
        {
            return View();
        }
        public IActionResult Service()
        {
            return View();
        }
        public IActionResult Errorpage()
        {
            return View();
        }
    }
}
