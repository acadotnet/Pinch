using Pinch.Data;
using Pinch.Models;
using Pinch.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Pinch.Services
{
    public class EFRecipeServices : IRecipeService
    {
        private readonly PinchContext _context;

        public EFRecipeServices(PinchContext context)
        {
            _context = context;
        }

        public IEnumerable<Recipe> GetRecipes()
        {
            return _context.Recipes.ToList();
        }

        public IEnumerable<Recipe> RecipeSearchByName(string name)
        {
            return _context.Recipes.Where(r => r.Name.Contains(name)).ToList();
        }

        public IEnumerable<Ingredient> RecipeSearchByIngredientName(string IngredientName)
        {
            return _context.Ingredients.Include(i => i.RecipeIngredients.Select(ri => ri.Recipe))
                                                           .Where(i => i.Name.Contains(IngredientName));
        }

        public Recipe GetRecipeById(int id)
        {
            return _context.Recipes
                .Include(r => r.RecipeIngredients.Select(ri => ri.Ingredient))
                .Include(r => r.Instructions)
                .FirstOrDefault(r => r.Id == id);
        }

        public void DeleteRecipeById(int id)
        {
            _context.Recipes.Remove(GetRecipeById(id));
            _context.SaveChanges();
        }

        public RecipeIngredient GetRecipeIngredientsByIngredientId(int ingredientId)
        {
            return _context.RecipeIngredients.FirstOrDefault(ri => ri.Id == ingredientId);
        }

        public void DeleteRecipeIngredient(int ingredientId)
        {
            _context.RecipeIngredients.Remove(GetRecipeIngredientsByIngredientId(ingredientId));
            _context.SaveChanges();
        }

        public Instruction CreateRecipeInstructions(int id, int sequence, string name)
        {
            var recipeInstruction = new Instruction
            {
                RecipeId = id,
                SequenceOrder = sequence,
                Name = name
            };

            _context.Instructions.Add(recipeInstruction);
            _context.SaveChanges();

            return recipeInstruction;
        }

        public Recipe CreateRecipe(string name, string desc, bool isfav, string image)
        {
            var recipe = new Recipe
            {
                Name = name,
                Description = desc,
                IsFavorite = isfav,
                ImageLink = image
            };

            _context.Recipes.Add(recipe);
            _context.SaveChanges();

            return recipe;
        }

        public void EditRecipe(int id, string name, string desc, bool isfav, string image)
        {
            var recipeToEdit = GetRecipeById(id);

            recipeToEdit.Name = name;
            recipeToEdit.Description = desc;
            recipeToEdit.IsFavorite = isfav;
            recipeToEdit.ImageLink = image;

            _context.SaveChanges();
        }

        public Ingredient GetIngredientByName(string name)
        {
            return _context.Ingredients.FirstOrDefault(i => i.Name == name);
        }

        public Ingredient AddIngredient(string name)
        {
            var ingredient = new Ingredient
            {
                Name = name
            };

            _context.Ingredients.Add(ingredient);
            _context.SaveChanges();

            return ingredient;
        }

        public RecipeIngredient AddRecipeIngredient(int id, int measurement, string measureUnit, int ingredientId)
        {
            var recipeIngredientToAdd = new RecipeIngredient
            {
                RecipeId = id,
                Measurement = measurement,
                UnitOfMeasurement = measureUnit,
                IngredientId = ingredientId
            };

            _context.RecipeIngredients.Add(recipeIngredientToAdd);
            _context.SaveChanges();

            return recipeIngredientToAdd;
        }
    }
}