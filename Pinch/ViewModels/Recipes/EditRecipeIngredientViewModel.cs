using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pinch.Models;

namespace Pinch.ViewModels.Recipes
{
    public class EditRecipeIngredientViewModel
    {
        public Recipe Recipe { get; set; }

        public string SelectedIngredientName { get; set; }

        public List<Ingredient> Ingredients { get; set; }
    }
}