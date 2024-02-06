using Cappa.Areas.Admin.ViewModels;
using Cappa.DAL;
using Cappa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cappa.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuController : Controller
    {
        private readonly AppDbContext _context;
        public MenuController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Menu> menus = await _context.Menus.ToListAsync();
            return View(menus);
        }
        public  IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateMenuVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            bool result = _context.Menus.Any(m => m.Title.Trim() == vm.Title.Trim());
            if (result)
            {
                ModelState.AddModelError("Title", "Bu menu artiq movcuddur");
                return View();
            }

            Menu menu = new Menu
            {
                Title = vm.Title,
                Description = vm.Description,
                Price = vm.Price,
            };

            await _context.Menus.AddAsync(menu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();

            Menu? menu = await _context.Menus.FirstOrDefaultAsync(m => m.Id == id);
            if (menu is null) return NotFound();

            UpdateMenuVm update = new UpdateMenuVm
            {
                Title = menu.Title,
                Description = menu.Description,
                Price = menu.Price,
            };

            return View(update);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateMenuVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            Menu? existed = await _context.Menus.FirstOrDefaultAsync(m => m.Id == id);

            if (existed is null) return NotFound();

            bool result = _context.Menus.Any(p => p.Title == vm.Title && p.Id != id);
            if (result)
            {
                ModelState.AddModelError("Title", "Bu adda menu artiq movcuddur");
                return View();
            }
            existed.Title = vm.Title;
            existed.Description = vm.Description;
            existed.Price = vm.Price;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            Menu? existed = await _context.Menus.FirstOrDefaultAsync(m => m.Id == id);

            if (existed is null) return NotFound();

            _context.Menus.Remove(existed);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0) return BadRequest();
            Menu position = await _context.Menus.FirstOrDefaultAsync(d => d.Id == id);
            if (position == null) return NotFound();
            return View(position);
        }
    }
}
