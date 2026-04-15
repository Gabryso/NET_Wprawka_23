using KuchniaApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KuchniaApp.ViewModels
{
    public class RecipeFormViewModel
    {
        public Recipe Recipe { get; set; } = new();
        public List<SelectListItem> Categories { get; set; } = new();
        public List<IngredientRowViewModel> AvailableIngredients { get; set; } = new();
        public List<int> SelectedIngredientIds { get; set; } = new();
        public Dictionary<int, string> Quantities { get; set; } = new();
    }

    public class IngredientRowViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? DefaultUnit { get; set; }
    }
}