using Cappa.Areas.Admin.ViewModels;
using Cappa.DAL;
using Cappa.Models;
using Cappa.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cappa.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoomController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public RoomController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Room> rooms = await _context.Rooms.ToListAsync();
            return View(rooms);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoomVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            bool result = await _context.Rooms.AnyAsync(r => r.Name.ToLower().Trim() == vm.Name.ToLower().Trim());
            if (result)
            {
                ModelState.AddModelError("Name", "Name already is exists");
                return View(vm);
            }
            if (!vm.Photo.ValidateType())
            {
                ModelState.AddModelError("Photo", "Image type is not valid");
                return View(vm);
            }
            if (!vm.Photo.ValidateSize(10))
            {
                ModelState.AddModelError("Photo", "Image size is not valid");
                return View(vm);
            }
            Room room = new Room
            {
                Name = vm.Name,
                Price = vm.Price,
                Description = vm.Description,
                Image = await vm.Photo.CreateFileAsync(_env.WebRootPath, "assets", "img", "rooms")
            };
            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Room? room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id);
            if (room == null) return NotFound();

            UpdateRoomVm update = new UpdateRoomVm
            {
                Name = room.Name,
                Price = room.Price,
                Description = room.Description,
                Image = room.Image
            };
            return View(update);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateRoomVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            Room? existed = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id);
            if (existed == null) return NotFound();

            bool result = await _context.Rooms.AnyAsync(r => r.Name.ToLower().Trim() == vm.Name.ToLower().Trim() && r.Id != id);
            if (result)
            {
                ModelState.AddModelError("Name", "Name already is exists");
                return View(vm);
            }
            if (vm.Photo is not null)
            {
                if (!vm.Photo.ValidateType())
                {
                    ModelState.AddModelError("Photo", "Image type is not valid");
                    return View(vm);
                }
                if (!vm.Photo.ValidateSize(10))
                {
                    ModelState.AddModelError("Photo", "Image size is not valid");
                    return View(vm);
                }
                string newImage = await vm.Photo.CreateFileAsync(_env.WebRootPath, "assets", "img", "rooms");
                existed.Image.DeleteFile(_env.WebRootPath, "assets", "img", "rooms");
                existed.Image = newImage;
            }
            existed.Name = vm.Name;
            existed.Price = vm.Price;
            existed.Description = vm.Description;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Room? room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id);
            if (room == null) return NotFound();

            room.Image.DeleteFile(_env.WebRootPath, "assets", "img", "rooms");
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0) return BadRequest();
            Room? room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id);
            if (room == null) return NotFound();
            return View(room);
        }
    }
}
