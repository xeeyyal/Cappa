using Microsoft.AspNetCore.Mvc;

namespace Cappa.Controllers
{
    public class RoomController : Controller
    {
        public IActionResult Rooms()
        {
            return View();
        }
        public IActionResult Detail()
        {
            return View();
        }
    }
}
