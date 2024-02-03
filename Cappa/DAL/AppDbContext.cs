using Cappa.Models;
using Microsoft.EntityFrameworkCore;

namespace Cappa.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Menu> Menus { get; set; }
    }
}
