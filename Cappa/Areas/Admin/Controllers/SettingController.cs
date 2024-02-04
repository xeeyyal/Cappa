using Microsoft.AspNetCore.Mvc;

namespace Cappa.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
