using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pinch.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string IngredientImage { get; set; }

        public bool IsMainIngredient { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }

    }
}