﻿using Microsoft.AspNetCore.Mvc;

namespace Cappa.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
