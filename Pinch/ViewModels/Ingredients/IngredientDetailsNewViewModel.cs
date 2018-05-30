using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pinch.Models;

namespace Pinch.ViewModels.Ingredients
{
    public class IngredientDetailsNewViewModel
    {
        public int RecipeId { get; set; }

        public Ingredient Ingredient { get; set; }
    }
}