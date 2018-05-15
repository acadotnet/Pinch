using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pinch.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsFavorite { get; set; }

        public string ImageLink { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }

        public ICollection<Instruction> Instructions { get; set; }
    }
}