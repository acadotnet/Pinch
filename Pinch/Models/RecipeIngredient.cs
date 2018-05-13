using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pinch.Models
{
    public class RecipeIngredient
    {
        public int Id { get; set; }

        [Required]
        public int Measurement { get; set; }

        [Required]
        public string UnitOfMeasurement { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

    }
}