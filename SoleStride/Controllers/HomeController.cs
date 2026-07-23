using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoleStride.Models;
using System.Diagnostics;

namespace SoleStride.Controllers
{
    public class HomeController : Controller
    {
        private readonly SoleStrideDbContext _context;

        public HomeController(SoleStrideDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var shoes = await _context.Shoes
                .Include(s => s.Category)
                .OrderBy(s => s.ShoesName)
                .ToListAsync();

            return View(shoes);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
