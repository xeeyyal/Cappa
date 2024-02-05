using Cappa.Areas.Admin.ViewModels;
using Cappa.DAL;
using Cappa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cappa.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;

        public SettingController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Setting> settings = await _context.Settings.ToListAsync();
            return View(settings);
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Setting? setting = await _context.Settings.FirstOrDefaultAsync(s => s.Id == id);
            if (setting == null) return NotFound();

            UpdateSettingVm update = new UpdateSettingVm
            {
                Key = setting.Key,
                Value = setting.Value
            };
            return View(update);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateSettingVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            Setting? existed = await _context.Settings.FirstOrDefaultAsync(s => s.Id == id);
            if (existed == null) return NotFound();

            existed.Value = vm.Value;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
