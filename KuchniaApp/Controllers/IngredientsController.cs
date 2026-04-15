using KuchniaApp.Data;
using KuchniaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KuchniaApp.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly AppDbContext _context;
        public IngredientsController(AppDbContext context) { _context = context; }

        public async Task<IActionResult> Index()
            => View(await _context.Ingredients.ToListAsync());

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ingredient ingredient)
        {
            if (!ModelState.IsValid) return View(ingredient);
            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var ing = await _context.Ingredients.FindAsync(id);
            if (ing == null) return NotFound();
            return View(ing);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ingredient ingredient)
        {
            if (id != ingredient.Id) return NotFound();
            if (!ModelState.IsValid) return View(ingredient);
            _context.Update(ingredient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var ing = await _context.Ingredients.FirstOrDefaultAsync(i => i.Id == id);
            if (ing == null) return NotFound();
            return View(ing);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ing = await _context.Ingredients.FindAsync(id);
            if (ing != null) _context.Ingredients.Remove(ing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}