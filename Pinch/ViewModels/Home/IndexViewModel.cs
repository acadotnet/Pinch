using Pinch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pinch.ViewModels.Home
{
    public class IndexViewModel
    {
        public List<Recipe> FavouriteRecipes { get; set; }

        public List<Recipe> Weekdaymeals { get; set; }

        public List<Ingredient> PopularIngredients { get; set; }
    }
}