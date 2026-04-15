using KuchniaApp.Data;
using KuchniaApp.Models;
using KuchniaApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KuchniaApp.Controllers
{
    public class RecipesController : Controller
    {
        private readonly AppDbContext _context;

        public RecipesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var recipes = await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.AppUser)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
            return View(recipes);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var recipe = await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.AppUser)
                .Include(r => r.RecipeIngredients!)
                    .ThenInclude(ri => ri.Ingredient)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null) return NotFound();
            return View(recipe);
        }

        public async Task<IActionResult> Create()
        {
            var vm = await BuildFormViewModel(new Recipe());
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            Recipe recipe,
            List<int> selectedIngredientIds,
            Dictionary<int, string> quantities)
        {
            if (ModelState.IsValid)
            {
                recipe.CreatedAt = DateTime.Now;
                _context.Recipes.Add(recipe);
                await _context.SaveChangesAsync();

                foreach (var ingId in selectedIngredientIds)
                {
                    quantities.TryGetValue(ingId, out var qty);
                    _context.RecipeIngredients.Add(new RecipeIngredient
                    {
                        RecipeId = recipe.Id,
                        IngredientId = ingId,
                        Quantity = qty ?? "?"
                    });
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var vm = await BuildFormViewModel(recipe);
            return View(vm);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var recipe = await _context.Recipes
                .Include(r => r.RecipeIngredients)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null) return NotFound();

            var vm = await BuildFormViewModel(recipe);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            Recipe recipe,
            List<int> selectedIngredientIds,
            Dictionary<int, string> quantities)
        {
            if (id != recipe.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var old = _context.RecipeIngredients.Where(ri => ri.RecipeId == id);
                _context.RecipeIngredients.RemoveRange(old);

                _context.Update(recipe);
                await _context.SaveChangesAsync();

                foreach (var ingId in selectedIngredientIds)
                {
                    quantities.TryGetValue(ingId, out var qty);
                    _context.RecipeIngredients.Add(new RecipeIngredient
                    {
                        RecipeId = id,
                        IngredientId = ingId,
                        Quantity = qty ?? "?"
                    });
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var vm = await BuildFormViewModel(recipe);
            return View(vm);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var recipe = await _context.Recipes
                .Include(r => r.Category)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (recipe == null) return NotFound();
            return View(recipe);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null) _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<RecipeFormViewModel> BuildFormViewModel(Recipe recipe)
        {
            var existingIds = recipe.RecipeIngredients?
                .Select(ri => ri.IngredientId).ToList() ?? new List<int>();

            var existingQty = recipe.RecipeIngredients?
                .ToDictionary(ri => ri.IngredientId, ri => ri.Quantity)
                ?? new Dictionary<int, string>();

            return new RecipeFormViewModel
            {
                Recipe = recipe,
                Categories = new SelectList(
                    await _context.Categories.ToListAsync(), "Id", "Name", recipe.CategoryId)
                    .ToList(),
                AvailableIngredients = await _context.Ingredients
                    .Select(i => new IngredientRowViewModel
                    { Id = i.Id, Name = i.Name, DefaultUnit = i.DefaultUnit })
                    .ToListAsync(),
                SelectedIngredientIds = existingIds,
                Quantities = existingQty
            };
        }
    }
}