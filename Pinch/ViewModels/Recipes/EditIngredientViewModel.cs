using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pinch.ViewModels.Recipes
{
    public class EditIngredientViewModel
    {
        public string IngredientName { get; set; }

        public int RecipeId { get; set; }

        public int Measurement { get; set; }

        public string UnitOfMeasurement { get; set; }


    }
}