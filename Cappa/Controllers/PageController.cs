using Cappa.DAL;
using Cappa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cappa.Controllers
{
    public class PageController : Controller
    {
        private readonly AppDbContext _context;

        public PageController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Employee()
        {
            List<Employee> employees = await _context.Employees.Include(e=>e.Position).ToListAsync();
            return View(employees);
        }
        public IActionResult Service()
        {
            return View();
        }
        public IActionResult Errorpage()
        {
            return View();
        }
    }
}
