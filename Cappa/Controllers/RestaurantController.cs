using Microsoft.AspNetCore.Mvc;

namespace Cappa.Controllers
{
    public class RestaurantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
