using Cappa.DAL;
using Cappa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cappa.Controllers
{
    public class RoomController : Controller
    {
        private readonly AppDbContext _context;

        public RoomController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Rooms()
        {
            List<Room> rooms = await _context.Rooms.ToListAsync();
            return View(rooms);
        }
        public IActionResult Detail()
        {
            return View();
        }
    }
}
