using Cappa.DAL;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
var app = builder.Build();

app.UseRouting();
app.UseStaticFiles();

app.MapControllerRoute(
    "default",
    "{area:exists}/{controller=home}/{action=index}/{id?}"
    );
app.MapControllerRoute(
    "default",
    "{controller=home}/{action=index}/{id?}"
    );

app.Run();
