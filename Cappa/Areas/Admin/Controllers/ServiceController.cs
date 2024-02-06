using Cappa.Areas.Admin.ViewModels;
using Cappa.DAL;
using Cappa.Models;
using Cappa.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cappa.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ServiceController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Service> services = await _context.Services.ToListAsync();
            return View(services);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateServiceVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            bool result = await _context.Services.AnyAsync(e => e.Title.ToLower().Trim() == vm.Title.ToLower().Trim());
            if (result)
            {
                ModelState.AddModelError("Title", "Title already is exists");
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
            Service service = new Service
            {
                Title = vm.Title,
                Subtitle = vm.Subtitle,
                Description = vm.Description,
                Price = vm.Price,
                Image = await vm.Photo.CreateFileAsync(_env.WebRootPath, "assets", "img")
            };
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Service? service = await _context.Services.FirstOrDefaultAsync(s => s.Id == id);
            if (service == null) return NotFound();

            UpdateServiceVm update = new UpdateServiceVm
            {
                Title = service.Title,
                Subtitle = service.Subtitle,
                Description = service.Description,
                Price = service.Price,
                Image = service.Image
            };
            return View(update);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateServiceVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            Service? existed = await _context.Services.FirstOrDefaultAsync(s => s.Id == id);
            if (existed == null) return NotFound();

            bool result = await _context.Services.AnyAsync(s => s.Title.ToLower().Trim() == vm.Title.ToLower().Trim() && s.Id != id);
            if (result)
            {
                ModelState.AddModelError("Title", "Title already is exists");
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
                string newImage = await vm.Photo.CreateFileAsync(_env.WebRootPath, "assets", "img");
                existed.Image.DeleteFile(_env.WebRootPath, "assets", "img");
                existed.Image = newImage;
            }
            existed.Title = vm.Title;
            existed.Subtitle = vm.Subtitle;
            existed.Description = vm.Description;
            existed.Price = vm.Price;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Service? service = await _context.Services.FirstOrDefaultAsync(s => s.Id == id);
            if (service == null) return NotFound();

            service.Image.DeleteFile(_env.WebRootPath, "assets", "img");
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0) return BadRequest();
            Service service = await _context.Services.FirstOrDefaultAsync(d => d.Id == id);
            if (service == null) return NotFound();
            return View(service);
        }
    }
}
