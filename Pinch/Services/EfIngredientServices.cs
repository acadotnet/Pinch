using Pinch.Data;
using Pinch.Models;
using Pinch.Services.Interfaces;
using Pinch.ViewModels.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

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

        public IEnumerable<Recipe> GetFewerIngredientsRecipes()
        {
            var recipes = new List<Recipe>();

            foreach (var recipe in _context.RecipeIngredients.GroupBy(r => r.RecipeId)
                                             .Select(g => new
                                             {
                                                 RecipeId = g.Key,
                                                 Count = g.Count()
                                             })
                                             .Where(r => r.Count < 5)
                                             .OrderBy(r => r.Count)
                                             .ToList())
            {
                recipes.Add(_context.Recipes.FirstOrDefault(r => r.Id == recipe.RecipeId));
            }

            return recipes;
        }

        public IEnumerable<Ingredient> GetPopularIngredients()
        {
            var ingredients = new List<Ingredient>();

            foreach (var ingredient in _context.RecipeIngredients.GroupBy(r => r.IngredientId)
                                             .Select(g => new
                                             {
                                                 IngredientId = g.Key,
                                                 Count = g.Count()
                                             })
                                             .Where(r => r.Count > 3 && r.Count < 7)
                                             .OrderByDescending(r => r.Count)
                                             .ToList())
            {
                ingredients.Add(_context.Ingredients.FirstOrDefault(r => r.Id == ingredient.IngredientId));
            }

            return ingredients;
        }

        public Ingredient EditIngredient(Ingredient ingredient)
        {
            var ingredient1 = _context.Ingredients.FirstOrDefault(i => i.Id == ingredient.Id);

            ingredient1.Name = ingredient.Name;
            ingredient1.IngredientImage = ingredient.IngredientImage;
            ingredient1.IsMainIngredient = ingredient.IsMainIngredient;

            _context.SaveChanges();
            
            return ingredient;
        }
    }
}