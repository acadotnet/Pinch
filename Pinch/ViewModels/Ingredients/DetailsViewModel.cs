using Pinch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pinch.ViewModels.Ingredients
{
    public class DetailsViewModel
    {
        public Ingredient Ingredient { get; set; }

        public List<Recipe> Recipes { get; set; }
    }
}