using Pinch.Data;
using Pinch.Models;
using Pinch.Services.Interfaces;
using Pinch.ViewModels.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pinch.Services
{
    public class EfIngredientServices : IIngredientService
    {
        private readonly PinchContext _context;

        public EfIngredientServices(PinchContext context)
        {
            _context = context;
        }

        public IEnumerable<Ingredient> Get()
        {
            return _context.Ingredients.ToList();
        }

        public Ingredient GetIngredientById(int ingredientId)
        {
            return _context.Ingredients.FirstOrDefault(i => i.Id == ingredientId);
        }

        public IEnumerable<Recipe> GetRecipiesByIngredientId(int ingredientId)
        {
            return _context.RecipeIngredients.Where(ri => ri.IngredientId == ingredientId).Select(ri => ri.Recipe).ToList();
        }
    }
}