using KuchniaApp.Data;
using KuchniaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KuchniaApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? categoryId, string? search)
        {
            var query = _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.AppUser)
                .AsQueryable();

            if (categoryId.HasValue)
                query = query.Where(r => r.CategoryId == categoryId.Value);

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(r => r.Title.Contains(search) || r.Description!.Contains(search));

            ViewBag.Categories = new SelectList(
                await _context.Categories.ToListAsync(), "Id", "Name", categoryId);
            ViewBag.CurrentCategory = categoryId;
            ViewBag.CurrentSearch = search;

            return View(await query.OrderByDescending(r => r.CreatedAt).ToListAsync());
        }
    }
}