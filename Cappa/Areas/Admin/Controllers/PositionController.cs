using Cappa.Areas.Admin.ViewModels;
using Cappa.DAL;
using Cappa.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cappa.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PositionController : Controller
    {
        private readonly AppDbContext _context;
        public PositionController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Position> positions = await _context.Positions.ToListAsync();
            return View(positions);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreatePositionVm positionVM)
        {
            if (!ModelState.IsValid) return View();

            bool result = _context.Positions.Any(a => a.Name.Trim() == positionVM.Name.Trim());
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda position movcuddur");
                return View();
            }

            Position position = new Position
            {
                Name = positionVM.Name
            };

            await _context.Positions.AddAsync(position);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();

            Position position = await _context.Positions.FirstOrDefaultAsync(d => d.Id == id);
            if (position is null) return NotFound();

            UpdatePositionVm positionVM = new UpdatePositionVm
            {
                Name = position.Name,
            };

            return View(positionVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdatePositionVm positionVM)
        {
            if (!ModelState.IsValid) return View(positionVM);
            Position existed = await _context.Positions.FirstOrDefaultAsync(p => p.Id == id);

            if (existed is null) return NotFound();

            bool result = _context.Positions.Any(p => p.Name == positionVM.Name && p.Id != id);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda position artiq movcuddur");
                return View();
            }
            existed.Name = positionVM.Name;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            Position existed = await _context.Positions.FirstOrDefaultAsync(p => p.Id == id);

            if (existed is null) return NotFound();

            _context.Positions.Remove(existed);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0) return BadRequest();

            Position position = await _context.Positions.FirstOrDefaultAsync(d => d.Id == id);
            if (position == null) return NotFound();

            return View(position);
        }
    }
}
